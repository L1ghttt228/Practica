using System.Windows.Forms;

namespace LZCompressor.App;

public sealed partial class SettingsForm : Form
{
    public int WindowSize => (int)_windowSize.Value;
    public int LookaheadSize => (int)_lookaheadSize.Value;
    public int DictionarySize => (int)_dictionarySize.Value;
    public int MaxPhraseLength => (int)_maxPhraseLength.Value;

    public SettingsForm()
    {
        InitializeComponent();
    }

    // Заполняем поля текущими значениями, чтобы пользователь мог их изменить.
    public SettingsForm(int windowSize, int lookaheadSize, int dictionarySize, int maxPhraseLength)
        : this()
    {
        _windowSize.Value = windowSize;
        _lookaheadSize.Value = lookaheadSize;
        _dictionarySize.Value = dictionarySize;
        _maxPhraseLength.Value = maxPhraseLength;
    }
}
