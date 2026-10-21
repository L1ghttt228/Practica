namespace LZCompressor.Core.Algorithms;

/// <summary>Определяет операции кодирования и декодирования для алгоритма сжатия.</summary>
public interface ILzAlgorithm
{
    CompressionAlgorithm Algorithm { get; }
    (long StepCount, long CodeCount) Compress(byte[] input, Stream output, CompressionOptions options);
    byte[] Decompress(byte[] input, int recordStart);
}
