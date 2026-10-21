using System.IO;

namespace LZCompressor.App.Services;

/// <summary>Подбирает свободное имя файла, не перезаписывая существующие данные.</summary>
internal static class FileNameService
{
    public static string GetUniqueFileName(string path)
    {
        if (!File.Exists(path))
            return path;

        string directory = Path.GetDirectoryName(path) ?? string.Empty;
        string name = Path.GetFileNameWithoutExtension(path);
        string extension = Path.GetExtension(path);
        int counter = 1;
        string candidate;
        do
        {
            candidate = Path.Combine(directory, $"{name} ({counter}){extension}");
            counter++;
        } while (File.Exists(candidate));
        return candidate;
    }
}
