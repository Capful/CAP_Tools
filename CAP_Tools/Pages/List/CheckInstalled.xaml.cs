using System;
using System.IO;
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
            ///判断NX是否安装，如果有安装再判断详细版本
            ///判断注册表项是否存在
            RegistryKey NX = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Unigraphics Solutions\Installed Applications");
            if (NX == null)
            {
                NX1872.IsEnabled = false;
                NX1872.Content = "未安装";
                NX1847.IsEnabled = false;
                NX1847.Content = "未安装";
                NX12.IsEnabled = false;
                NX12.Content = "未安装";
                NX11.IsEnabled = false;
                NX11.Content = "未安装";
                NX10.IsEnabled = false;
                NX10.Content = "未安装";
            }
            else
            {
                if (CheckNX1872() == true)
                {
                    string NXEXE = GetNXEXE("Unigraphics V32.0");
                    string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(NXEXE)));
                    NX1872.IsEnabled = true;
                    NX1872.Content = NXPath.ToString();
                }
                else
                {
                    NX1872.IsEnabled = false;
                    NX1872.Content = "未安装";
                }
                if (CheckNX1847() == true)
                {
                    string NXEXE = GetNXEXE("Unigraphics V31.0");
                    string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(NXEXE)));
                    NX1847.IsEnabled = true;
                    NX1847.Content = NXPath.ToString();
                }
                else
                {
                    NX1847.IsEnabled = false;
                    NX1847.Content = "未安装";
                }
                if (CheckNX12() == true)
                {
                    string NXEXE = GetNXEXE("Unigraphics V30.0");
                    string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(NXEXE)));
                    NX12.IsEnabled = true;
                    NX12.Content = NXPath.ToString();
                }
                else
                {
                    NX12.IsEnabled = false;
                    NX12.Content = "未安装";
                }

                if (CheckNX11() == true)
                {
                    string NXEXE = GetNXEXE("Unigraphics V29.0");
                    string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(NXEXE)));
                    NX11.IsEnabled = true;
                    NX11.Content = NXPath.ToString();
                }
                else
                {
                    NX11.IsEnabled = false;
                    NX11.Content = "未安装";
                }

                if (CheckNX10() == true)
                {
                    string NXEXE = GetNXEXE("Unigraphics V28.0");
                    string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(NXEXE)));
                    NX10.IsEnabled = true;
                    NX10.Content = NXPath.ToString();
                }
                else
                {
                    NX10.IsEnabled = false;
                    NX10.Content = "未安装";
                }
            }
            
        }

        private void NX10_Click(object sender, RoutedEventArgs e)
        {
            ///System.Diagnostics.Process.Start(@"D:\Program Files\Siemens\NX 12.0");
            ///获取NX安装路径
            string NXEXE = GetNXEXE("Unigraphics V28.0");
            System.Diagnostics.Process.Start(NXEXE);
        }

        private void NX11_Click(object sender, RoutedEventArgs e)
        {
            ///获取NX安装路径
            string NXEXE = GetNXEXE("Unigraphics V29.0");
            System.Diagnostics.Process.Start(NXEXE);
        }

        private void NX12_Click(object sender, RoutedEventArgs e)
        {
            ///获取NX安装路径
            string NXEXE = GetNXEXE("Unigraphics V30.0");
            System.Diagnostics.Process.Start(NXEXE);
        }

        private void NX1847_Click(object sender, RoutedEventArgs e)
        {
            ///获取NX安装路径
            string NXEXE = GetNXEXE("Unigraphics V31.0");
            System.Diagnostics.Process.Start(NXEXE);
        }

        private void NX1872_Click(object sender, RoutedEventArgs e)
        {
            ///获取NX安装路径
            string NXEXE = GetNXEXE("Unigraphics V32.0");
            System.Diagnostics.Process.Start(NXEXE);
        }

        private bool CheckNX1872()
        {
            string NXEXE = GetNXEXE("Unigraphics V32.0");
            string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(NXEXE)));
            if (NXPath != null)
            {
                if (File.Exists(NXEXE))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckNX1847()
        {
            string NXEXE = GetNXEXE("Unigraphics V31.0");
            string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(NXEXE)));
            if (NXPath != null)
            {
                if (File.Exists(NXEXE))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckNX12()
        {
            string NXEXE = GetNXEXE("Unigraphics V30.0");
            string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(NXEXE)));
            if (NXPath != null)
            {
                if (File.Exists(NXEXE))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckNX11()
        {
            string NXEXE = GetNXEXE("Unigraphics V29.0");
            string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(NXEXE)));
            if (NXPath != null)
            {
                if (File.Exists(NXEXE))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckNX10()
        {
            string NXEXE = GetNXEXE("Unigraphics V28.0");
            string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(NXEXE)));
            if (NXPath != null)
            {
                if (File.Exists(NXEXE))
                {
                    return true;
                }
            }
            return false;
        }

        private string GetNXEXE(string Versions)
        {
            ///获取NX安装路径
            RegistryKey driverKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Unigraphics Solutions\Installed Applications");
            ///指定对应版本
            string EXE = (String)driverKey.GetValue(Versions);
            return EXE;
        }
    }
}
