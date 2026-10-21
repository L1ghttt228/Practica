using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using LZCompressor.App.Services;
using LZCompressor.Core;

namespace LZCompressor.App;

public sealed partial class MainForm : Form
{
    private readonly CompressionService _compressionService = new();
    private readonly FileComparisonService _fileComparisonService = new();
    private readonly ReportService _reportService = new();
    private readonly List<CompressionResult> _compressionResults = new();
    private string _currentFilePath = string.Empty;
    private int _windowSize = 4096;
    private int _lookaheadSize = 32;
    private int _dictionarySize = 4096;
    private int _maxPhraseLength = 256;

    public MainForm()
    {
        InitializeComponent();
        SetAlgorithm(0);
    }

    private CompressionAlgorithm SelectedAlgorithm => _algorithm.SelectedIndex == 1
        ? CompressionAlgorithm.Lz78
        : CompressionAlgorithm.Lz77;

    private CompressionOptions CurrentOptions() => new(
        _windowSize, _lookaheadSize, _dictionarySize, _maxPhraseLength);

    private void OpenMenuItem_Click(object? sender, EventArgs e) => BrowseForOriginal();

    private void BrowseButton_Click(object? sender, EventArgs e) => BrowseForOriginal();

    private void BrowseForOriginal()
    {
        if (!string.IsNullOrEmpty(_currentFilePath) && File.Exists(_currentFilePath))
        {
            _openFileDialog.InitialDirectory = Path.GetDirectoryName(_currentFilePath) ?? string.Empty;
            _openFileDialog.FileName = Path.GetFileName(_currentFilePath);
        }
        else
        {
            _openFileDialog.FileName = string.Empty;
        }

        if (_openFileDialog.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            _currentFilePath = _openFileDialog.FileName;
            _filePath.Text = _currentFilePath;
            _originalPreview.Text = File.ReadAllText(_currentFilePath);
            SetStatus("Файл загружен: " + Path.GetFileName(_currentFilePath));
        }
        catch (Exception exception)
        {
            ShowFileError("Не удалось загрузить файл.", exception);
        }
    }

    private void Algorithm_SelectedIndexChanged(object? sender, EventArgs e) => UpdateAlgorithmMenuChecks();

    private void Lz77MenuItem_Click(object? sender, EventArgs e) => SetAlgorithm(0);

    private void Lz78MenuItem_Click(object? sender, EventArgs e) => SetAlgorithm(1);

    private void SetAlgorithm(int index)
    {
        _algorithm.SelectedIndex = index;
        UpdateAlgorithmMenuChecks();
    }

    private void UpdateAlgorithmMenuChecks()
    {
        bool isLz77 = _algorithm.SelectedIndex != 1;
        _lz77MenuItem.Checked = isLz77;
        _lz78MenuItem.Checked = !isLz77;
    }

    // Сохраняем сжатые данные в выбранную папку и добавляем результат в таблицу.
    private void CompressButton_Click(object? sender, EventArgs e) => CompressSelectedFile();

    private void CompressSelectedFile()
    {
        if (!RequireOriginal("Пожалуйста, выберите исходный файл для сжатия."))
            return;

        CompressionAlgorithm algorithm = SelectedAlgorithm;
        string algorithmName = algorithm == CompressionAlgorithm.Lz77 ? "LZ77" : "LZ78";
        string suffix = algorithm == CompressionAlgorithm.Lz77 ? ".lz77.bin" : ".lz78.bin";
        string suggested = FileNameService.GetUniqueFileName(_currentFilePath + suffix);
        _saveFileDialog.Title = "Сохранить сжатый файл как...";
        _saveFileDialog.Filter = "Сжатые файлы (*.bin)|*.bin|Все файлы (*.*)|*.*";
        _saveFileDialog.FileName = suggested;
        _saveFileDialog.InitialDirectory = Path.GetDirectoryName(_currentFilePath) ?? string.Empty;
        _saveFileDialog.DefaultExt = "bin";
        _saveFileDialog.AddExtension = false;
        _saveFileDialog.OverwritePrompt = true;
        if (_saveFileDialog.ShowDialog(this) != DialogResult.OK)
            return;

        string destination = FileNameService.GetUniqueFileName(_saveFileDialog.FileName);
        try
        {
            CompressionResult result = _compressionService.CompressFile(
                _currentFilePath, destination, algorithm, CurrentOptions());
            AddResult(result);
            _processedPreview.Text = PreviewBytes(File.ReadAllBytes(destination));
            SetStatus("Сжатие завершено: " + algorithmName);
        }
        catch (Exception exception)
        {
            ShowFileError("Не удалось сжать файл.", exception);
        }
    }

    // После распаковки сравниваем восстановленный файл с выбранным исходником.
    private void DecompressButton_Click(object? sender, EventArgs e) => DecompressSelectedFile();

    private void DecompressSelectedFile()
    {
        if (_decompressOpenFileDialog.ShowDialog(this) != DialogResult.OK)
            return;

        try
        {
            string compressedPath = _decompressOpenFileDialog.FileName;
            string originalExtension = _compressionService.GetOriginalExtension(compressedPath);
            string stem = Path.Combine(Path.GetDirectoryName(compressedPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(compressedPath));
            string suggested = FileNameService.GetUniqueFileName(stem + ".restored" + originalExtension);
            _saveFileDialog.Title = "Сохранить восстановленный файл как...";
            _saveFileDialog.Filter = $"Восстановленные файлы (*.restored{originalExtension})|*.restored{originalExtension}|Все файлы (*.*)|*.*";
            _saveFileDialog.FileName = suggested;
            _saveFileDialog.InitialDirectory = Path.GetDirectoryName(compressedPath) ?? string.Empty;
            _saveFileDialog.DefaultExt = originalExtension.TrimStart('.');
            _saveFileDialog.AddExtension = false;
            _saveFileDialog.OverwritePrompt = true;
            if (_saveFileDialog.ShowDialog(this) != DialogResult.OK)
                return;

            string restoredPath = FileNameService.GetUniqueFileName(_saveFileDialog.FileName);
            _compressionService.DecompressFile(compressedPath, restoredPath, SelectedAlgorithm);
            _originalPreview.Text = File.ReadAllText(restoredPath);
            if (!string.IsNullOrEmpty(_currentFilePath) && File.Exists(_currentFilePath))
            {
                bool identical = _fileComparisonService.FilesAreIdentical(_currentFilePath, restoredPath);
                SetStatus(identical
                    ? "Распаковка завершена. Данные идентичны."
                    : "Распаковка завершена. ВНИМАНИЕ: данные различаются!");
            }
            else
            {
                SetStatus("Распаковка завершена. Исходный файл для сравнения не найден.");
            }
        }
        catch (Exception exception)
        {
            ShowFileError("Не удалось распаковать файл.", exception);
        }
    }

    // Запускаем оба алгоритма с одинаковыми параметрами для сопоставления результатов.
    private void CompareButton_Click(object? sender, EventArgs e) => CompareAlgorithms();

    private void CompareAlgorithms()
    {
        if (!RequireOriginal("Пожалуйста, выберите исходный файл для проведения сравнения."))
            return;

        try
        {
            string directory = Path.GetDirectoryName(_currentFilePath) ?? string.Empty;
            string stem = Path.Combine(directory, Path.GetFileNameWithoutExtension(_currentFilePath));
            CompressionResult lz77 = _compressionService.CompressFile(
                _currentFilePath, stem + "_lz77.bin", CompressionAlgorithm.Lz77, CurrentOptions());
            AddResult(lz77);
            CompressionResult lz78 = _compressionService.CompressFile(
                _currentFilePath, stem + "_lz78.bin", CompressionAlgorithm.Lz78, CurrentOptions());
            AddResult(lz78);
            SetStatus("Сравнение завершено.");
        }
        catch (Exception exception)
        {
            ShowFileError("Не удалось выполнить сравнение алгоритмов.", exception);
        }
    }

    private bool RequireOriginal(string message)
    {
        if (string.IsNullOrEmpty(_currentFilePath))
            _currentFilePath = _filePath.Text;
        if (!string.IsNullOrEmpty(_currentFilePath) && File.Exists(_currentFilePath))
            return true;

        MessageBox.Show(this, message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
    }

    private void AddResult(CompressionResult result)
    {
        _compressionResults.Add(result);
        _results.Rows.Add(
            result.AlgorithmName,
            result.OriginalSize.ToString(CultureInfo.CurrentCulture),
            result.CompressedSize.ToString(CultureInfo.CurrentCulture),
            result.CompressionRatio.ToString("0.##", CultureInfo.CurrentCulture),
            result.ExecutionTimeMilliseconds.ToString(CultureInfo.CurrentCulture));
    }

    // Формируем отчет из накопленных результатов и сохраняем его в UTF-8.
    private void SaveReportMenuItem_Click(object? sender, EventArgs e) => SaveReport();

    private void SaveReport()
    {
        if (_compressionResults.Count == 0)
        {
            MessageBox.Show(this,
                "Нет данных для сохранения. Сначала выполните сжатие или сравнение.",
                "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (_saveReportDialog.ShowDialog(this) != DialogResult.OK)
            return;

        string reportPath = FileNameService.GetUniqueFileName(_saveReportDialog.FileName);
        try
        {
            string report = _reportService.CreateReport(
                _compressionResults, _windowSize, _lookaheadSize, _dictionarySize, _maxPhraseLength,
                DateTime.Now, CultureInfo.CurrentCulture);
            File.WriteAllText(reportPath, report, new UTF8Encoding(false));
            SetStatus("Отчет сохранен: " + Path.GetFileName(reportPath));
        }
        catch (Exception exception)
        {
            ShowFileError("Не удалось сохранить отчет.", exception);
        }
    }

    // Применяем новые параметры только после подтверждения пользователем.
    private void ParametersMenuItem_Click(object? sender, EventArgs e) => EditParameters();

    private void EditParameters()
    {
        using var dialog = new SettingsForm(_windowSize, _lookaheadSize, _dictionarySize, _maxPhraseLength);
        if (dialog.ShowDialog(this) != DialogResult.OK)
            return;

        _windowSize = dialog.WindowSize;
        _lookaheadSize = dialog.LookaheadSize;
        _dictionarySize = dialog.DictionarySize;
        _maxPhraseLength = dialog.MaxPhraseLength;
        SetStatus("Параметры обновлены.");
    }

    private void AboutMenuItem_Click(object? sender, EventArgs e) => OpenAboutHelp();

    private void OpenAboutHelp()
    {
        try
        {
            string helpPath = Path.Combine(AppContext.BaseDirectory, "Lempel-Ziv.chm");
            if (!File.Exists(helpPath))
            {
                MessageBox.Show(this, "Файл справки Lempel-Ziv.chm не найден рядом с приложением.",
                    "Справка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(helpPath)
            {
                UseShellExecute = true
            });
        }
        catch (Exception exception)
        {
            ShowFileError("Не удалось открыть справку.", exception);
        }
    }

    private void ExitMenuItem_Click(object? sender, EventArgs e) => Close();

    private static string PreviewBytes(byte[] data)
    {
        int length = Math.Min(data.Length, 10000);
        char[] characters = new char[length];
        for (int i = 0; i < length; i++)
        {
            byte value = data[i];
            characters[i] = value is >= 32 and <= 126 or >= 128 ? (char)value : '.';
        }
        return new string(characters);
    }

    private void ShowFileError(string message, Exception exception)
    {
        MessageBox.Show(this, message + Environment.NewLine + exception.Message,
            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        SetStatus("Операция не выполнена.");
    }

    private void SetStatus(string text) => _statusText.Text = text;
}
