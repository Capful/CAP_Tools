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

        public CheckInstalled()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ///System.Diagnostics.Process.Start(@"D:\Program Files\Siemens\NX 12.0");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void NewMethod(string result)
        {
            ///msgboxResult.Text = result.ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ///获取NX安装路径
            RegistryKey driverKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Unigraphics Solutions\Installed Applications");
            string NX12EXE = (String)driverKey.GetValue("Unigraphics V30.0");
            string NX12 = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NX12EXE)));
            ///
            ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
            System.Diagnostics.Process.Start(@NX12);
        }
    }
}
