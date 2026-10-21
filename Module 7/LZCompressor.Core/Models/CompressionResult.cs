namespace LZCompressor.Core;

/// <summary>Содержит размеры файлов, время выполнения и показатели алгоритма.</summary>
public sealed record CompressionResult(
    string AlgorithmName,
    long OriginalSize,
    long CompressedSize,
    double CompressionRatio,
    long ExecutionTimeMilliseconds,
    long StepCount,
    long CodeCount);
