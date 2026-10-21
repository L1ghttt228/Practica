#nullable enable

namespace PasswordManager;

partial class EntriesForm
{
    private System.ComponentModel.IContainer? components = null;
    private Panel headerPanel = null!;
    private Label titleLabel = null!;
    private Label pathLabel = null!;
    private TextBox searchBox = null!;
    private ListView entriesList = null!;
    private ColumnHeader serviceColumn = null!;
    private ColumnHeader loginColumn = null!;
    private Button addButton = null!;
    private Button editButton = null!;
    private Button deleteButton = null!;
    private Button closeButton = null!;

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
        pathLabel = new Label();
        searchBox = new TextBox();
        entriesList = new ListView();
        serviceColumn = new ColumnHeader();
        loginColumn = new ColumnHeader();
        addButton = new Button();
        editButton = new Button();
        deleteButton = new Button();
        closeButton = new Button();
        headerPanel.SuspendLayout();
        SuspendLayout();
        headerPanel.BackColor = Color.FromArgb(22, 34, 55);
        headerPanel.Controls.Add(titleLabel);
        headerPanel.Controls.Add(pathLabel);
        headerPanel.Dock = DockStyle.Top;
        headerPanel.Height = 88;
        titleLabel.AutoSize = true;
        titleLabel.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
        titleLabel.ForeColor = Color.White;
        titleLabel.Location = new Point(24, 14);
        titleLabel.Text = "Ваши доступы";
        pathLabel.AutoSize = true;
        pathLabel.Font = new Font("Segoe UI", 9F);
        pathLabel.ForeColor = Color.FromArgb(177, 190, 210);
        pathLabel.Location = new Point(27, 51);
        pathLabel.Text = "Зашифрованная база KeePass";
        searchBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        searchBox.BackColor = Color.White;
        searchBox.BorderStyle = BorderStyle.FixedSingle;
        searchBox.Font = new Font("Segoe UI", 10F);
        searchBox.Location = new Point(25, 108);
        searchBox.PlaceholderText = "Поиск по сервису или логину";
        searchBox.Size = new Size(690, 30);
        searchBox.TextChanged += SearchBox_TextChanged;
        entriesList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        entriesList.BackColor = Color.White;
        entriesList.BorderStyle = BorderStyle.FixedSingle;
        entriesList.Columns.AddRange(new[] { serviceColumn, loginColumn });
        entriesList.Font = new Font("Segoe UI", 10F);
        entriesList.FullRowSelect = true;
        entriesList.HideSelection = false;
        entriesList.Location = new Point(25, 156);
        entriesList.MultiSelect = false;
        entriesList.Size = new Size(690, 350);
        entriesList.View = View.Details;
        entriesList.DoubleClick += EditButton_Click;
        serviceColumn.Text = "СЕРВИС";
        serviceColumn.Width = 335;
        loginColumn.Text = "ЛОГИН / ЭЛЕКТРОННАЯ ПОЧТА";
        loginColumn.Width = 330;
        addButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        addButton.BackColor = Color.FromArgb(49, 196, 141);
        addButton.FlatAppearance.BorderSize = 0;
        addButton.FlatStyle = FlatStyle.Flat;
        addButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        addButton.ForeColor = Color.White;
        addButton.Location = new Point(25, 526);
        addButton.Size = new Size(150, 40);
        addButton.Text = "+  Добавить запись";
        addButton.UseVisualStyleBackColor = false;
        addButton.Click += AddButton_Click;
        editButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        editButton.BackColor = Color.FromArgb(230, 238, 248);
        editButton.FlatAppearance.BorderSize = 0;
        editButton.FlatStyle = FlatStyle.Flat;
        editButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        editButton.ForeColor = Color.FromArgb(44, 86, 134);
        editButton.Location = new Point(185, 526);
        editButton.Size = new Size(145, 40);
        editButton.Text = "Изменить";
        editButton.UseVisualStyleBackColor = false;
        editButton.Click += EditButton_Click;
        deleteButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        deleteButton.BackColor = Color.FromArgb(250, 241, 241);
        deleteButton.FlatAppearance.BorderSize = 0;
        deleteButton.FlatStyle = FlatStyle.Flat;
        deleteButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        deleteButton.ForeColor = Color.FromArgb(180, 76, 76);
        deleteButton.Location = new Point(340, 526);
        deleteButton.Size = new Size(130, 40);
        deleteButton.Text = "Удалить";
        deleteButton.UseVisualStyleBackColor = false;
        deleteButton.Click += DeleteButton_Click;
        closeButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        closeButton.BackColor = Color.White;
        closeButton.FlatAppearance.BorderColor = Color.FromArgb(216, 223, 232);
        closeButton.FlatStyle = FlatStyle.Flat;
        closeButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        closeButton.ForeColor = Color.FromArgb(62, 77, 98);
        closeButton.Location = new Point(585, 526);
        closeButton.Size = new Size(130, 40);
        closeButton.Text = "Закрыть базу";
        closeButton.UseVisualStyleBackColor = false;
        closeButton.Click += CloseButton_Click;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(246, 248, 251);
        ClientSize = new Size(740, 590);
        Controls.Add(closeButton);
        Controls.Add(deleteButton);
        Controls.Add(editButton);
        Controls.Add(addButton);
        Controls.Add(entriesList);
        Controls.Add(searchBox);
        Controls.Add(headerPanel);
        Font = new Font("Segoe UI", 9F);
        MinimumSize = new Size(700, 560);
        Name = "EntriesForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Записи базы — менеджер паролей";
        headerPanel.ResumeLayout(false);
        headerPanel.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
