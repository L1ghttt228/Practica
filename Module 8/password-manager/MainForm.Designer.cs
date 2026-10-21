#nullable enable

namespace PasswordManager;

partial class MainForm
{
    private System.ComponentModel.IContainer? components = null;
    private Panel headerPanel = null!;
    private Label titleLabel = null!;
    private Label subtitleLabel = null!;
    private Label welcomeLabel = null!;
    private Label descriptionLabel = null!;
    private Panel actionsCard = null!;
    private Button createButton = null!;
    private Button openButton = null!;
    private Button deleteButton = null!;
    private Label footerLabel = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing) components?.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        headerPanel = new Panel();
        titleLabel = new Label();
        subtitleLabel = new Label();
        welcomeLabel = new Label();
        descriptionLabel = new Label();
        actionsCard = new Panel();
        createButton = new Button();
        openButton = new Button();
        deleteButton = new Button();
        footerLabel = new Label();
        headerPanel.SuspendLayout();
        actionsCard.SuspendLayout();
        SuspendLayout();
        headerPanel.BackColor = Color.FromArgb(25, 37, 62);
        headerPanel.Controls.Add(titleLabel);
        headerPanel.Controls.Add(subtitleLabel);
        headerPanel.Dock = DockStyle.Top;
        headerPanel.Height = 100;
        titleLabel.AutoSize = true;
        titleLabel.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
        titleLabel.ForeColor = Color.White;
        titleLabel.Location = new Point(32, 17);
        titleLabel.Text = "Менеджер паролей";
        subtitleLabel.AutoSize = true;
        subtitleLabel.Font = new Font("Segoe UI", 9.5F);
        subtitleLabel.ForeColor = Color.FromArgb(177, 190, 215);
        subtitleLabel.Location = new Point(35, 62);
        subtitleLabel.Text = "Формат KeePass · защита мастер-паролем";
        welcomeLabel.AutoSize = true;
        welcomeLabel.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        welcomeLabel.ForeColor = Color.FromArgb(30, 43, 68);
        welcomeLabel.Location = new Point(52, 145);
        welcomeLabel.Text = "Ваши пароли";
        descriptionLabel.AutoSize = true;
        descriptionLabel.Font = new Font("Segoe UI", 10.5F);
        descriptionLabel.ForeColor = Color.FromArgb(103, 117, 138);
        descriptionLabel.Location = new Point(55, 191);
        descriptionLabel.Text = "Создайте новую базу или откройте уже существующий файл .kdbx.";
        actionsCard.BackColor = Color.White;
        actionsCard.Controls.Add(createButton);
        actionsCard.Controls.Add(openButton);
        actionsCard.Controls.Add(deleteButton);
        actionsCard.Location = new Point(52, 252);
        actionsCard.Size = new Size(545, 232);
        createButton.BackColor = Color.FromArgb(64, 100, 213);
        createButton.FlatAppearance.BorderSize = 0;
        createButton.FlatStyle = FlatStyle.Flat;
        createButton.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        createButton.ForeColor = Color.White;
        createButton.Location = new Point(25, 24);
        createButton.Size = new Size(495, 50);
        createButton.Text = "+   Создать базу паролей";
        createButton.UseVisualStyleBackColor = false;
        createButton.Click += CreateButton_Click;
        openButton.BackColor = Color.FromArgb(235, 240, 251);
        openButton.FlatAppearance.BorderSize = 0;
        openButton.FlatStyle = FlatStyle.Flat;
        openButton.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
        openButton.ForeColor = Color.FromArgb(45, 68, 125);
        openButton.Location = new Point(25, 91);
        openButton.Size = new Size(495, 49);
        openButton.Text = "Открыть существующую базу";
        openButton.UseVisualStyleBackColor = false;
        openButton.Click += OpenButton_Click;
        deleteButton.BackColor = Color.FromArgb(250, 242, 243);
        deleteButton.FlatAppearance.BorderSize = 0;
        deleteButton.FlatStyle = FlatStyle.Flat;
        deleteButton.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        deleteButton.ForeColor = Color.FromArgb(166, 70, 82);
        deleteButton.Location = new Point(25, 158);
        deleteButton.Size = new Size(495, 44);
        deleteButton.Text = "Удалить файл базы…";
        deleteButton.UseVisualStyleBackColor = false;
        deleteButton.Click += DeleteButton_Click;
        footerLabel.AutoSize = true;
        footerLabel.Font = new Font("Segoe UI", 9F);
        footerLabel.ForeColor = Color.FromArgb(103, 117, 138);
        footerLabel.Location = new Point(53, 513);
        footerLabel.Text = "Не забудьте мастер-пароль: восстановить его через приложение нельзя.";
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 251);
        ClientSize = new Size(650, 570);
        Controls.Add(footerLabel);
        Controls.Add(actionsCard);
        Controls.Add(descriptionLabel);
        Controls.Add(welcomeLabel);
        Controls.Add(headerPanel);
        Font = new Font("Segoe UI", 9F);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Менеджер паролей";
        headerPanel.ResumeLayout(false);
        headerPanel.PerformLayout();
        actionsCard.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }
}
