namespace LZCompressor.Core.Algorithms;

/// <summary>Кодирует повторяющиеся последовательности ссылками на фразы словаря.</summary>
public sealed class Lz78Algorithm : ILzAlgorithm
{
    public CompressionAlgorithm Algorithm => CompressionAlgorithm.Lz78;

    public (long StepCount, long CodeCount) Compress(byte[] input, Stream output, CompressionOptions options)
    {
        var dictionary = new Dictionary<byte[], ushort>(ByteArrayComparer.Instance);
        byte[] current = Array.Empty<byte>();
        ushort lastIndex = 0;
        long steps = 0;
        long codes = 0;
        Span<byte> record = stackalloc byte[4];

        for (int i = 0; i < input.Length; i++)
        {
            steps++;
            byte[] next = Append(current, input[i]);
            if (dictionary.TryGetValue(next, out ushort found) && next.Length <= options.MaxPhraseLength)
            {
                current = next;
                lastIndex = found;
            }
            else
            {
                WriteUInt16(record, 0, lastIndex);
                record[2] = input[i];
                record[3] = 0;
                output.Write(record);
                codes++;
                if (dictionary.Count < options.DictionarySize)
                {
                    ushort newIndex = checked((ushort)(dictionary.Count + 1));
                    dictionary.Add(next, newIndex);
                }
                current = Array.Empty<byte>();
                lastIndex = 0;
            }
        }

        if (current.Length > 0)
        {
            WriteUInt16(record, 0, lastIndex);
            record[2] = 0;
            record[3] = 1;
            output.Write(record);
            codes++;
        }
        return (steps, codes);
    }

    public byte[] Decompress(byte[] input, int recordStart)
    {
        var dictionary = new List<byte[]> { Array.Empty<byte>() };
        using var output = new MemoryStream();
        int position = recordStart;
        while (position < input.Length)
        {
            if (input.Length - position < 4)
                throw new InvalidDataException("Сжатый файл LZ78 обрезан: не хватает данных записи.");
            ushort prefixIndex = ReadUInt16(input, position);
            byte next = input[position + 2];
            byte isLast = input[position + 3];
            position += 4;
            if (isLast > 1)
                throw new InvalidDataException("Файл LZ78 содержит недопустимый признак последней записи.");
            if (prefixIndex >= dictionary.Count)
                throw new InvalidDataException("Запись LZ78 ссылается на отсутствующую фразу словаря.");

            byte[] phrase = isLast == 1 ? dictionary[prefixIndex] : Append(dictionary[prefixIndex], next);
            output.Write(phrase);
            // Сохраняем восстановленную фразу, чтобы использовать ее при декодировании следующих записей.
            dictionary.Add(phrase);
        }
        return output.ToArray();
    }

    private static byte[] Append(byte[] prefix, byte value)
    {
        byte[] result = new byte[prefix.Length + 1];
        prefix.CopyTo(result, 0);
        result[^1] = value;
        return result;
    }

    private static ushort ReadUInt16(byte[] source, int offset) =>
        (ushort)(source[offset] | (source[offset + 1] << 8));

    private static void WriteUInt16(Span<byte> destination, int offset, ushort value)
    {
        destination[offset] = (byte)value;
        destination[offset + 1] = (byte)(value >> 8);
    }

    private sealed class ByteArrayComparer : IEqualityComparer<byte[]>
    {
        public static readonly ByteArrayComparer Instance = new();
        public bool Equals(byte[]? x, byte[]? y) => ReferenceEquals(x, y) ||
            (x is not null && y is not null && x.AsSpan().SequenceEqual(y));
        public int GetHashCode(byte[] value)
        {
            var hash = new HashCode();
            foreach (byte item in value)
                hash.Add(item);
            return hash.ToHashCode();
        }
    }
}
