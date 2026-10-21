namespace LZCompressor.Core;

/// <summary>Сравнивает содержимое файлов потоково, не загружая оба файла целиком в память.</summary>
public sealed class FileComparisonService
{
    /// <summary>Возвращает true, только если длина и каждый байт двух файлов совпадают.</summary>
    public bool FilesAreIdentical(string first, string second)
    {
        ArgumentException.ThrowIfNullOrEmpty(first);
        ArgumentException.ThrowIfNullOrEmpty(second);
        using var left = new FileStream(first, FileMode.Open, FileAccess.Read, FileShare.Read);
        using var right = new FileStream(second, FileMode.Open, FileAccess.Read, FileShare.Read);
        if (left.Length != right.Length)
            return false;

        byte[] a = new byte[81920];
        byte[] b = new byte[81920];
        while (true)
        {
            int countA = left.Read(a, 0, a.Length);
            int countB = right.Read(b, 0, b.Length);
            if (countA != countB)
                return false;
            if (countA == 0)
                return true;
            if (!a.AsSpan(0, countA).SequenceEqual(b.AsSpan(0, countB)))
                return false;
        }
    }
}
