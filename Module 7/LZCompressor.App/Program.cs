using System;
using System.Windows.Forms;

namespace LZCompressor.App;

internal static class Program
{
    // Настраивает WinForms и запускает главное окно программы.
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
