namespace LZCompressor.Core.Services;

/// <summary>Вычисляет общие метрики сжатия отдельно от кодирования формата.</summary>
internal sealed class CompressionStatisticsService
{
    public CompressionResult CreateResult(string algorithmName, long originalSize, long compressedSize,
        long executionTimeMilliseconds, long stepCount, long codeCount)
    {
        double ratio = originalSize == 0 ? 0 : (1d - (double)compressedSize / originalSize) * 100d;
        return new CompressionResult(algorithmName, originalSize, compressedSize, ratio,
            executionTimeMilliseconds, stepCount, codeCount);
    }
}
