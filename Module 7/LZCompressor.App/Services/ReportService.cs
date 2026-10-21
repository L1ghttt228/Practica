using System.Globalization;
using System.Text;
using LZCompressor.Core;

namespace LZCompressor.App.Services;

/// <summary>Формирует текстовый отчет по результатам работы приложения.</summary>
internal sealed class ReportService
{
    public string CreateReport(
        IReadOnlyCollection<CompressionResult> results,
        int windowSize,
        int lookaheadSize,
        int dictionarySize,
        int maxPhraseLength,
        DateTime reportDate,
        CultureInfo culture)
    {
        var report = new StringBuilder();
        report.AppendLine("ОТЧЕТ О СЖАТИИ ДАННЫХ");
        report.AppendLine("Дата: " + reportDate.ToString(culture));
        report.AppendLine($"Параметры LZ77: Окно={windowSize}, Буфер={lookaheadSize}");
        report.AppendLine($"Параметры LZ78: Словарь={dictionarySize}, Макс.фраза={maxPhraseLength}");
        report.AppendLine("----------------------------------------");
        report.AppendLine("Алгоритм\tИсходный (байт)\tСжатый (байт)\tСжатие (%)\tВремя (мс)\t");
        foreach (CompressionResult result in results)
        {
            report.Append(result.AlgorithmName).Append('\t')
                .Append(result.OriginalSize.ToString(culture)).Append('\t')
                .Append(result.CompressedSize.ToString(culture)).Append('\t')
                .Append(result.CompressionRatio.ToString("0.##", culture)).Append('\t')
                .Append(result.ExecutionTimeMilliseconds.ToString(culture)).AppendLine("\t");
        }
        return report.ToString();
    }
}
