#nullable enable

namespace PasswordManager;

partial class MasterPasswordDialog
{
    private System.ComponentModel.IContainer? components = null;
    private Label titleLabel = null!;
    private Label hintLabel = null!;
    private Label passwordLabel = null!;
    private TextBox passwordBox = null!;
    private Label confirmLabel = null!;
    private TextBox confirmBox = null!;
    private Button revealButton = null!;
    private Button confirmButton = null!;
    private Button cancelButton = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        titleLabel = new Label();
        hintLabel = new Label();
        passwordLabel = new Label();
        passwordBox = new TextBox();
        confirmLabel = new Label();
        confirmBox = new TextBox();
        revealButton = new Button();
        confirmButton = new Button();
        cancelButton = new Button();
        SuspendLayout();
        titleLabel.AutoSize = true;
        titleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        titleLabel.ForeColor = Color.FromArgb(30, 43, 68);
        titleLabel.Location = new Point(27, 22);
        titleLabel.Text = "Мастер-пароль";
        hintLabel.AutoSize = false;
        hintLabel.Font = new Font("Segoe UI", 9F);
        hintLabel.ForeColor = Color.FromArgb(103, 117, 138);
        hintLabel.Location = new Point(30, 64);
        hintLabel.Size = new Size(495, 42);
        hintLabel.Text = "Не теряйте пароль: он шифрует базу и не хранится приложением.";
        passwordLabel.AutoSize = true;
        passwordLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        passwordLabel.ForeColor = Color.FromArgb(75, 88, 111);
        passwordLabel.Location = new Point(30, 123);
        passwordLabel.Text = "МАСТЕР-ПАРОЛЬ";
        passwordBox.BorderStyle = BorderStyle.FixedSingle;
        passwordBox.Font = new Font("Segoe UI", 11F);
        passwordBox.Location = new Point(30, 148);
        passwordBox.PasswordChar = '●';
        passwordBox.Size = new Size(370, 32);
        confirmLabel.AutoSize = true;
        confirmLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        confirmLabel.ForeColor = Color.FromArgb(75, 88, 111);
        confirmLabel.Location = new Point(30, 204);
        confirmLabel.Text = "ПОВТОРИТЕ ПАРОЛЬ";
        confirmBox.BorderStyle = BorderStyle.FixedSingle;
        confirmBox.Font = new Font("Segoe UI", 11F);
        confirmBox.Location = new Point(30, 229);
        confirmBox.PasswordChar = '●';
        confirmBox.Size = new Size(495, 32);
        revealButton.BackColor = Color.FromArgb(235, 240, 251);
        revealButton.FlatAppearance.BorderSize = 0;
        revealButton.FlatStyle = FlatStyle.Flat;
        revealButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        revealButton.ForeColor = Color.FromArgb(45, 68, 125);
        revealButton.Location = new Point(410, 146);
        revealButton.Size = new Size(115, 36);
        revealButton.Text = "Показать";
        revealButton.UseVisualStyleBackColor = false;
        revealButton.Click += RevealButton_Click;
        confirmButton.BackColor = Color.FromArgb(64, 100, 213);
        confirmButton.FlatAppearance.BorderSize = 0;
        confirmButton.FlatStyle = FlatStyle.Flat;
        confirmButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        confirmButton.ForeColor = Color.White;
        confirmButton.Location = new Point(278, 322);
        confirmButton.Size = new Size(120, 38);
        confirmButton.Text = "Продолжить";
        confirmButton.UseVisualStyleBackColor = false;
        confirmButton.Click += ConfirmButton_Click;
        cancelButton.BackColor = Color.White;
        cancelButton.FlatAppearance.BorderColor = Color.FromArgb(216, 223, 232);
        cancelButton.FlatStyle = FlatStyle.Flat;
        cancelButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        cancelButton.ForeColor = Color.FromArgb(62, 77, 98);
        cancelButton.Location = new Point(410, 322);
        cancelButton.Size = new Size(115, 38);
        cancelButton.Text = "Отмена";
        cancelButton.UseVisualStyleBackColor = false;
        cancelButton.Click += CancelButton_Click;
        AcceptButton = confirmButton;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 251);
        CancelButton = cancelButton;
        ClientSize = new Size(555, 390);
        Controls.Add(cancelButton);
        Controls.Add(confirmButton);
        Controls.Add(revealButton);
        Controls.Add(confirmBox);
        Controls.Add(confirmLabel);
        Controls.Add(passwordBox);
        Controls.Add(passwordLabel);
        Controls.Add(hintLabel);
        Controls.Add(titleLabel);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "MasterPasswordDialog";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Мастер-пароль";
        ResumeLayout(false);
        PerformLayout();
    }
}
