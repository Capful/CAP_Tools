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
            if (CheckNX12() == true)
            {
                RegistryKey driverKey = NXregistry();
                string NX12EXE = (String)driverKey.GetValue("Unigraphics V30.0");
                string NX12 = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NX12EXE)));
                this.NX12.IsEnabled = true;
                this.NX12.Content = NX12.ToString();
            }
            else
            {
                this.NX12.IsEnabled = false;
                this.NX12.Content = "未安装";
            }

            if (CheckNX11() == true)
            {
                RegistryKey driverKey = NXregistry();
                string NX11EXE = (String)driverKey.GetValue("Unigraphics V29.0");
                string NX11 = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NX11EXE)));
                this.NX11.IsEnabled = true;
                this.NX11.Content = NX11.ToString();
            }
            else
            {
                this.NX11.IsEnabled = false;
                this.NX11.Content = "未安装";
            }

            if (CheckNX10() == true)
            {
                RegistryKey driverKey = NXregistry();
                string NX10EXE = (String)driverKey.GetValue("Unigraphics V28.0");
                string NX10 = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NX10EXE)));
                this.NX10.IsEnabled = true;
                this.NX10.Content = NX10.ToString();
            }
            else
            {
                this.NX10.IsEnabled = false;
                this.NX10.Content = "未安装";
            }

        }

        private static RegistryKey NXregistry()
        {
            return Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Unigraphics Solutions\Installed Applications");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ///System.Diagnostics.Process.Start(@"D:\Program Files\Siemens\NX 12.0");
            ///获取NX安装路径
            RegistryKey driverKey = NXregistry();
            string NX10EXE = (String)driverKey.GetValue("Unigraphics V28.0");
            string NX10 = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NX10EXE)));
            ///
            ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
            System.Diagnostics.Process.Start(@NX10);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ///获取NX安装路径
            RegistryKey driverKey = NXregistry();
            string NX11EXE = (String)driverKey.GetValue("Unigraphics V29.0");
            string NX11 = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NX11EXE)));
            ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
            System.Diagnostics.Process.Start(@NX11);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ///获取NX安装路径
            RegistryKey driverKey = NXregistry();
            string NX12EXE = (String)driverKey.GetValue("Unigraphics V30.0");
            ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
            string NX12 = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NX12EXE)));
            System.Diagnostics.Process.Start(@NX12);
        }

        private bool CheckNX12()
        {
            RegistryKey driverKey = NXregistry();
            string NX12EXE = (String)driverKey.GetValue("Unigraphics V30.0");
            string NX12 = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NX12EXE)));
            if (NX12 != null)
            {
                return true;
            }
            return false;
        }

        private bool CheckNX11()
        {
            RegistryKey driverKey = NXregistry();
            string NX11EXE = (String)driverKey.GetValue("Unigraphics V29.0");
            string NX11 = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NX11EXE)));
            if (NX11 != null)
            {
                return true;
            }
            return false;
        }
        private bool CheckNX10()
        {
            RegistryKey driverKey = NXregistry();
            string NX10EXE = (String)driverKey.GetValue("Unigraphics V28.0");
            string NX10 = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NX10EXE)));
            if (NX10 != null)
            {
                return true;
            }
            return false;
        }
    }
}
