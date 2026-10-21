using System.Diagnostics;
using LZCompressor.Core.Algorithms;
using LZCompressor.Core.IO;
using LZCompressor.Core.Services;

namespace LZCompressor.Core;

/// <summary>Выбирает алгоритм и координирует сжатие или распаковку файла.</summary>
public sealed class CompressionService
{
    private readonly IReadOnlyDictionary<CompressionAlgorithm, ILzAlgorithm> _algorithms;
    private readonly CompressionStatisticsService _statistics;

    /// <summary>Создает сервис, позволяя при необходимости передать реализации алгоритмов.</summary>
    public CompressionService(ILzAlgorithm? lz77 = null, ILzAlgorithm? lz78 = null)
    {
        ILzAlgorithm algorithm77 = lz77 ?? new Lz77Algorithm();
        ILzAlgorithm algorithm78 = lz78 ?? new Lz78Algorithm();
        if (algorithm77.Algorithm != CompressionAlgorithm.Lz77 || algorithm78.Algorithm != CompressionAlgorithm.Lz78)
            throw new ArgumentException("Внедренная реализация алгоритма не соответствует выбранному типу LZ77/LZ78.");
        _algorithms = new Dictionary<CompressionAlgorithm, ILzAlgorithm>
        {
            [CompressionAlgorithm.Lz77] = algorithm77,
            [CompressionAlgorithm.Lz78] = algorithm78
        };
        _statistics = new CompressionStatisticsService();
    }

    /// <summary>Сжимает файл выбранным алгоритмом и возвращает результаты измерений.</summary>
    public CompressionResult CompressFile(string sourcePath, string destinationPath,
        CompressionAlgorithm algorithm, CompressionOptions options)
    {
        ArgumentException.ThrowIfNullOrEmpty(sourcePath);
        ArgumentException.ThrowIfNullOrEmpty(destinationPath);
        ArgumentNullException.ThrowIfNull(options);
        ValidateAlgorithm(algorithm);
        ValidateOptions(options);

        byte[] input = File.ReadAllBytes(sourcePath);
        using var output = new MemoryStream();
        LegacyCompressedFileFormat.WriteHeader(output, Path.GetExtension(sourcePath));

        var timer = Stopwatch.StartNew();
        (long steps, long codes) = _algorithms[algorithm].Compress(input, output, options);
        timer.Stop();

        byte[] compressed = output.ToArray();
        File.WriteAllBytes(destinationPath, compressed);
        return _statistics.CreateResult(AlgorithmName(algorithm), input.LongLength, compressed.LongLength,
            timer.ElapsedMilliseconds, steps, codes);
    }

    /// <summary>Восстанавливает файл с помощью выбранного алгоритма.</summary>
    public void DecompressFile(string sourcePath, string destinationPath, CompressionAlgorithm algorithm)
    {
        ArgumentException.ThrowIfNullOrEmpty(sourcePath);
        ArgumentException.ThrowIfNullOrEmpty(destinationPath);
        ValidateAlgorithm(algorithm);
        byte[] input = File.ReadAllBytes(sourcePath);
        int recordStart = LegacyCompressedFileFormat.ReadHeaderEnd(input);
        byte[] result = _algorithms[algorithm].Decompress(input, recordStart);
        File.WriteAllBytes(destinationPath, result);
    }

    /// <summary>Читает расширение исходного файла из заголовка сжатого файла.</summary>
    public string GetOriginalExtension(string compressedFile)
    {
        ArgumentException.ThrowIfNullOrEmpty(compressedFile);
        return LegacyCompressedFileFormat.ReadOriginalExtension(compressedFile);
    }

    private static void ValidateAlgorithm(CompressionAlgorithm algorithm)
    {
        if (algorithm is not CompressionAlgorithm.Lz77 and not CompressionAlgorithm.Lz78)
            throw new ArgumentOutOfRangeException(nameof(algorithm), algorithm, "Выбран неизвестный алгоритм сжатия.");
    }

    private static void ValidateOptions(CompressionOptions options)
    {
        if (options.WindowSize is < 0 or > ushort.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(options), "Размер окна должен укладываться в диапазон поля смещения LZ77.");
        if (options.LookaheadSize is < 0 or > ushort.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(options), "Размер буфера должен укладываться в диапазон поля длины LZ77.");
        if (options.DictionarySize is < 0 or > ushort.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(options), "Размер словаря должен укладываться в диапазон индекса LZ78.");
        if (options.MaxPhraseLength < 0)
            throw new ArgumentOutOfRangeException(nameof(options), "Максимальная длина фразы не может быть отрицательной.");
    }

    private static string AlgorithmName(CompressionAlgorithm algorithm) => algorithm switch
    {
        CompressionAlgorithm.Lz77 => "LZ77",
        CompressionAlgorithm.Lz78 => "LZ78",
        _ => throw new ArgumentOutOfRangeException(nameof(algorithm), algorithm, "Выбран неизвестный алгоритм сжатия.")
    };
}
