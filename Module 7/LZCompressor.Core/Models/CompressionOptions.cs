namespace LZCompressor.Core;

/// <summary>Задает размеры окна поиска и словаря для алгоритмов сжатия.</summary>
public sealed record CompressionOptions(
    int WindowSize = 4096,
    int LookaheadSize = 32,
    int DictionarySize = 4096,
    int MaxPhraseLength = 256);
