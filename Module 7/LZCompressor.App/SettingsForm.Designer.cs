#nullable enable

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace LZCompressor.App;

partial class SettingsForm
{
    private IContainer? components = null;
    private GroupBox _lz77Group = null!;
    private GroupBox _lz78Group = null!;
    private Label _windowSizeLabel = null!;
    private Label _lookaheadSizeLabel = null!;
    private Label _dictionarySizeLabel = null!;
    private Label _maxPhraseLengthLabel = null!;
    private NumericUpDown _windowSize = null!;
    private NumericUpDown _lookaheadSize = null!;
    private NumericUpDown _dictionarySize = null!;
    private NumericUpDown _maxPhraseLength = null!;
    private Button _okButton = null!;
    private Button _cancelButton = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components is not null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new Container();
        _lz77Group = new GroupBox();
        _lz78Group = new GroupBox();
        _windowSizeLabel = new Label();
        _lookaheadSizeLabel = new Label();
        _dictionarySizeLabel = new Label();
        _maxPhraseLengthLabel = new Label();
        _windowSize = new NumericUpDown();
        _lookaheadSize = new NumericUpDown();
        _dictionarySize = new NumericUpDown();
        _maxPhraseLength = new NumericUpDown();
        _okButton = new Button();
        _cancelButton = new Button();
        _lz77Group.SuspendLayout();
        _lz78Group.SuspendLayout();
        ((ISupportInitialize)_windowSize).BeginInit();
        ((ISupportInitialize)_lookaheadSize).BeginInit();
        ((ISupportInitialize)_dictionarySize).BeginInit();
        ((ISupportInitialize)_maxPhraseLength).BeginInit();
        SuspendLayout();

        // Параметры скользящего окна LZ77.
        _lz77Group.Controls.Add(_windowSizeLabel);
        _lz77Group.Controls.Add(_windowSize);
        _lz77Group.Controls.Add(_lookaheadSizeLabel);
        _lz77Group.Controls.Add(_lookaheadSize);
        _lz77Group.Location = new Point(8, 8);
        _lz77Group.Name = "_lz77Group";
        _lz77Group.Size = new Size(374, 90);
        _lz77Group.TabIndex = 0;
        _lz77Group.TabStop = false;
        _lz77Group.Text = "Параметры LZ77";
        _windowSizeLabel.AutoSize = true;
        _windowSizeLabel.Location = new Point(16, 26);
        _windowSizeLabel.Name = "_windowSizeLabel";
        _windowSizeLabel.Size = new Size(183, 15);
        _windowSizeLabel.TabIndex = 0;
        _windowSizeLabel.Text = "Размер окна (256–32768 байт):";
        _windowSize.Location = new Point(245, 22);
        _windowSize.Maximum = 32768;
        _windowSize.Minimum = 256;
        _windowSize.Name = "_windowSize";
        _windowSize.Size = new Size(110, 23);
        _windowSize.TabIndex = 1;
        _windowSize.Value = 4096;
        _lookaheadSizeLabel.AutoSize = true;
        _lookaheadSizeLabel.Location = new Point(16, 57);
        _lookaheadSizeLabel.Name = "_lookaheadSizeLabel";
        _lookaheadSizeLabel.Size = new Size(203, 15);
        _lookaheadSizeLabel.TabIndex = 2;
        _lookaheadSizeLabel.Text = "Размер буфера (16–256 байт):";
        _lookaheadSize.Location = new Point(245, 53);
        _lookaheadSize.Maximum = 256;
        _lookaheadSize.Minimum = 16;
        _lookaheadSize.Name = "_lookaheadSize";
        _lookaheadSize.Size = new Size(110, 23);
        _lookaheadSize.TabIndex = 3;
        _lookaheadSize.Value = 32;

        // Параметры словаря фраз LZ78.
        _lz78Group.Controls.Add(_dictionarySizeLabel);
        _lz78Group.Controls.Add(_dictionarySize);
        _lz78Group.Controls.Add(_maxPhraseLengthLabel);
        _lz78Group.Controls.Add(_maxPhraseLength);
        _lz78Group.Location = new Point(8, 108);
        _lz78Group.Name = "_lz78Group";
        _lz78Group.Size = new Size(374, 100);
        _lz78Group.TabIndex = 4;
        _lz78Group.TabStop = false;
        _lz78Group.Text = "Параметры LZ78";
        _dictionarySizeLabel.AutoSize = true;
        _dictionarySizeLabel.Location = new Point(16, 28);
        _dictionarySizeLabel.Name = "_dictionarySizeLabel";
        _dictionarySizeLabel.Size = new Size(112, 15);
        _dictionarySizeLabel.TabIndex = 0;
        _dictionarySizeLabel.Text = "Размер словаря:";
        _dictionarySize.Location = new Point(245, 24);
        _dictionarySize.Maximum = 65535;
        _dictionarySize.Minimum = 256;
        _dictionarySize.Name = "_dictionarySize";
        _dictionarySize.Size = new Size(110, 23);
        _dictionarySize.TabIndex = 1;
        _dictionarySize.Value = 4096;
        _maxPhraseLengthLabel.AutoSize = true;
        _maxPhraseLengthLabel.Location = new Point(16, 62);
        _maxPhraseLengthLabel.Name = "_maxPhraseLengthLabel";
        _maxPhraseLengthLabel.Size = new Size(156, 15);
        _maxPhraseLengthLabel.TabIndex = 2;
        _maxPhraseLengthLabel.Text = "Макс. длина фразы (байт):";
        _maxPhraseLength.Location = new Point(245, 58);
        _maxPhraseLength.Maximum = 4096;
        _maxPhraseLength.Minimum = 32;
        _maxPhraseLength.Name = "_maxPhraseLength";
        _maxPhraseLength.Size = new Size(110, 23);
        _maxPhraseLength.TabIndex = 3;
        _maxPhraseLength.Value = 256;

        // Кнопки подтверждения и отмены параметров.
        _okButton.DialogResult = DialogResult.OK;
        _okButton.Location = new Point(200, 240);
        _okButton.Name = "_okButton";
        _okButton.Size = new Size(82, 27);
        _okButton.TabIndex = 5;
        _okButton.Text = "ОК";
        _okButton.UseVisualStyleBackColor = true;
        _cancelButton.DialogResult = DialogResult.Cancel;
        _cancelButton.Location = new Point(290, 240);
        _cancelButton.Name = "_cancelButton";
        _cancelButton.Size = new Size(92, 27);
        _cancelButton.TabIndex = 6;
        _cancelButton.Text = "Отмена";
        _cancelButton.UseVisualStyleBackColor = true;

        // Свойства модального окна настроек.
        AcceptButton = _okButton;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = _cancelButton;
        ClientSize = new Size(390, 280);
        Controls.Add(_cancelButton);
        Controls.Add(_okButton);
        Controls.Add(_lz78Group);
        Controls.Add(_lz77Group);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "SettingsForm";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Параметры алгоритмов";
        _lz77Group.ResumeLayout(false);
        _lz77Group.PerformLayout();
        _lz78Group.ResumeLayout(false);
        _lz78Group.PerformLayout();
        ((ISupportInitialize)_windowSize).EndInit();
        ((ISupportInitialize)_lookaheadSize).EndInit();
        ((ISupportInitialize)_dictionarySize).EndInit();
        ((ISupportInitialize)_maxPhraseLength).EndInit();
        ResumeLayout(false);
    }
}
