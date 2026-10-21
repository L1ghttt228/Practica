using System.Text;

namespace LZCompressor.Core.IO;

/// <summary>Записывает расширение исходного файла в заголовок и читает его при распаковке.</summary>
internal static class LegacyCompressedFileFormat
{
    private static readonly UTF8Encoding StrictUtf8 = new(false, true);

    /// <summary>Записывает длину расширения одним байтом, затем его UTF-8-байты.</summary>
    public static void WriteHeader(Stream destination, string originalExtension)
    {
        byte[] extension = Encoding.UTF8.GetBytes(originalExtension);
        if (extension.Length > byte.MaxValue)
            throw new ArgumentException("Длина расширения файла превышает допустимый размер заголовка.", "sourcePath");
        destination.WriteByte((byte)extension.Length);
        destination.Write(extension);
    }

    /// <summary>Проверяет заголовок и возвращает позицию начала сжатых данных.</summary>
    public static int ReadHeaderEnd(byte[] input)
    {
        if (input.Length == 0)
            throw new InvalidDataException("В сжатом файле отсутствует заголовок длины расширения.");
        int extensionLength = input[0];
        if (input.Length < extensionLength + 1)
            throw new InvalidDataException("Заголовок расширения сжатого файла поврежден или обрезан.");
        return extensionLength + 1;
    }

    /// <summary>Возвращает расширение исходного файла или .bin, если заголовок некорректен.</summary>
    public static string ReadOriginalExtension(string compressedFile)
    {
        if (!File.Exists(compressedFile))
            return ".bin";
        byte[] data = File.ReadAllBytes(compressedFile);
        if (data.Length == 0 || data.Length < 1 + data[0] || data[0] == 0)
            return ".bin";
        try
        {
            return StrictUtf8.GetString(data, 1, data[0]);
        }
        catch (DecoderFallbackException)
        {
            return ".bin";
        }
    }
}
