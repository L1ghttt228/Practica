namespace LZCompressor.Core.Algorithms;

/// <summary>Заменяет найденные повторы парой «смещение — длина» и следующим байтом.</summary>
public sealed class Lz77Algorithm : ILzAlgorithm
{
    public CompressionAlgorithm Algorithm => CompressionAlgorithm.Lz77;

    public (long StepCount, long CodeCount) Compress(byte[] input, Stream output, CompressionOptions options)
    {
        long steps = 0;
        long codes = 0;
        int position = 0;
        Span<byte> record = stackalloc byte[6];
        while (position < input.Length)
        {
            int bestLength = 0;
            int bestOffset = 0;
            for (int distance = 1; distance <= options.WindowSize; distance++)
            {
                int matchPosition = position - distance;
                if (matchPosition < 0)
                    break;

                int matchLength = 0;
                // Сравнение исходных байтов позволяет кодировать совпадения, длина которых
                // превышает расстояние до повторяемой последовательности.
                while (matchLength < options.LookaheadSize &&
                       position + matchLength < input.Length &&
                       input[matchPosition + matchLength] == input[position + matchLength])
                    matchLength++;

                if (matchLength > bestLength)
                {
                    bestLength = matchLength;
                    bestOffset = distance;
                }
            }

            ushort offset = checked((ushort)bestOffset);
            ushort length = checked((ushort)bestLength);
            // Нулевой следующий байт используется как признак последней записи.
            byte next = position + bestLength < input.Length ? input[position + bestLength] : (byte)0;
            WriteUInt16(record, 0, offset);
            WriteUInt16(record, 2, length);
            record[4] = next;
            record[5] = 0; // Последний байт записи не содержит данных и сохраняет ее размер.
            output.Write(record);
            codes++;
            steps++;
            position += bestLength + 1;
        }
        return (steps, codes);
    }

    public byte[] Decompress(byte[] input, int recordStart)
    {
        using var output = new MemoryStream();
        int position = recordStart;
        while (position < input.Length)
        {
            if (input.Length - position < 6)
                throw new InvalidDataException("Сжатый файл LZ77 обрезан: не хватает данных записи.");
            ushort offset = ReadUInt16(input, position);
            ushort length = ReadUInt16(input, position + 2);
            byte next = input[position + 4];
            position += 6; // Переходим к следующей записи фиксированной длины.
            if (offset > 0)
            {
                if (offset > output.Length)
                    throw new InvalidDataException("Запись LZ77 ссылается на позицию до начала выходных данных.");
                for (int i = 0; i < length; i++)
                {
                    long sourceIndex = output.Length - offset;
                    output.WriteByte(output.GetBuffer()[checked((int)sourceIndex)]);
                }
            }
            // Нулевой следующий байт пропускается только в последней записи.
            if (position < input.Length || next != 0)
                output.WriteByte(next);
        }
        return output.ToArray();
    }

    private static ushort ReadUInt16(byte[] source, int offset) =>
        (ushort)(source[offset] | (source[offset + 1] << 8));

    private static void WriteUInt16(Span<byte> destination, int offset, ushort value)
    {
        destination[offset] = (byte)value;
        destination[offset + 1] = (byte)(value >> 8);
    }
}
