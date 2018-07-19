using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace NX_Tool.Pages.List
{
    /// <summary>
    /// Interaction logic for CheckInstalled.xaml
    /// </summary>
    public partial class CheckInstalled : UserControl
    {
        private const string NX12 = @"D:\Program Files\Siemens\NX 12.0";

        public CheckInstalled()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@NX12);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RegistryKey driverKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Unigraphics Solutions\Installed Applications");
            string result = (String)driverKey.GetValue("Unigraphics V30.0");
            ///System.Diagnostics.Process.Start("explorer.exe", result);
            ///MessageBox.Show(result);
            NewMethod(result);
        }

        private void NewMethod(string result)
        {
            msgboxResult.Text = result.ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(ReadRegistry(@"SOFTWARE\WOW6432Node\Unigraphics Solutions\Installed Applications"));
        }

        private string ReadRegistry(string p)
        {
            RegistryKey rk = Registry.LocalMachine.OpenSubKey(p, true);
            if (rk == null) return "";
            return (string)rk.GetValue("Unigraphics V30.0");
        }
    }
}
