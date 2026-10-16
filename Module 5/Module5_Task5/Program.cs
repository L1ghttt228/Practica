using System;                 
using System.Windows.Forms;   

namespace Calculator
{
    
    internal static class Program
    {
        [STAThread] 
        static void Main() 
        {
            ApplicationConfiguration.Initialize(); 
            Application.Run(new Form1());          
        }
    }
}
