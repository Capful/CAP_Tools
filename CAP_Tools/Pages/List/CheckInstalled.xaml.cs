using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace CAP_Tools.Pages.List
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

            if (CheckNX85() == true)
            {
                RegistryKey driverKey = NXregistry();
                string NX85EXE = (String)driverKey.GetValue("Unigraphics V26.5");
                string NX85 = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NX85EXE)));
                this.NX85.IsEnabled = true;
                this.NX85.Content = NX85.ToString();
            }
            else
            {
                this.NX85.IsEnabled = false;
                this.NX85.Content = "未安装";
            }
        }

        private static RegistryKey NXregistry()
        {
            return Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Unigraphics Solutions\Installed Applications");
        }

        private void NX85_Click(object sender, RoutedEventArgs e)
        {
            ///System.Diagnostics.Process.Start(@"D:\Program Files\Siemens\NX 12.0");
            ///获取NX安装路径
            RegistryKey driverKey = NXregistry();
            string NXEXE = (String)driverKey.GetValue("Unigraphics V26.5");
            string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(NXEXE)));
            ///
            ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
            System.Diagnostics.Process.Start(@NXPath);
        }

        private void NX10_Click(object sender, RoutedEventArgs e)
        {
            ///System.Diagnostics.Process.Start(@"D:\Program Files\Siemens\NX 12.0");
            ///获取NX安装路径
            RegistryKey driverKey = NXregistry();
            string NXEXE = (String)driverKey.GetValue("Unigraphics V28.0");
            string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NXEXE)));
            ///
            ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
            System.Diagnostics.Process.Start(@NXPath);
        }

        private void NX11_Click(object sender, RoutedEventArgs e)
        {
            ///获取NX安装路径
            RegistryKey driverKey = NXregistry();
            string NXEXE = (String)driverKey.GetValue("Unigraphics V29.0");
            string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NXEXE)));
            ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
            System.Diagnostics.Process.Start(@NXPath);
        }

        private void NX12_Click(object sender, RoutedEventArgs e)
        {
            ///获取NX安装路径
            RegistryKey driverKey = NXregistry();
            string NXEXE = (String)driverKey.GetValue("Unigraphics V30.0");
            ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
            string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NXEXE)));
            System.Diagnostics.Process.Start(@NXPath);
        }

        private bool CheckNX12()
        {
            RegistryKey driverKey = NXregistry();
            string NXEXE = (String)driverKey.GetValue("Unigraphics V30.0");
            string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NXEXE)));
            if (NXPath != null)
            {
                return true;
            }
            return false;
        }

        private bool CheckNX11()
        {
            RegistryKey driverKey = NXregistry();
            string NXEXE = (String)driverKey.GetValue("Unigraphics V29.0");
            string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NXEXE)));
            if (NXPath != null)
            {
                return true;
            }
            return false;
        }

        private bool CheckNX10()
        {
            RegistryKey driverKey = NXregistry();
            string NXEXE = (String)driverKey.GetValue("Unigraphics V28.0");
            string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NXEXE)));
            if (NXPath != null)
            {
                return true;
            }
            return false;
        }

        private bool CheckNX85()
        {
            RegistryKey driverKey = NXregistry();
            string NXEXE = (String)driverKey.GetValue("Unigraphics V26.5");
            string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NXEXE)));
            if (NXPath != null)
            {
                return true;
            }
            return false;
        }
    }
}
