using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CAP_Tools.Pages.List
{
    /// <summary>
    /// Interaction logic for Collection.xaml
    /// </summary>
    public partial class Collection : UserControl
    {
        public Collection()
        {
            InitializeComponent();
        }

        private static RegistryKey NXregistry()
        {
            return Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Unigraphics Solutions\Installed Applications");
        }

        private void ModernButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (CheckNX12() == true)
            {
                ///获取NX安装路径
                RegistryKey driverKey = NXregistry();
                string NX12EXE = (String)driverKey.GetValue("Unigraphics V30.0");
                ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
                string NX12 = (System.IO.Path.GetDirectoryName(@NX12EXE));
                string path1 = @NX12;
                string path2 = "ugshext.dll";
                string newPath = System.IO.Path.Combine(path1, path2); // newPath = "D:\temp\result.txt";
                string newPath1 = "\"" + newPath + "\""; //"\"" + path + "\""（path为原来的路径）
                string newPath2 = "\"" + newPath + "\""; //"\"" + path + "\""（path为原来的路径）
                ///注册DLL
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "Regsvr32.exe";
                p.StartInfo.Arguments = @newPath1;
                p.Start();
               /// ModernDialog.ShowMessage(newPath1, "提示", MessageBoxButton.OK);
            }
            else
            {
                if (CheckNX11() == true)
                {
                    ///获取NX安装路径
                    RegistryKey driverKey = NXregistry();
                    string NX11EXE = (String)driverKey.GetValue("Unigraphics V29.0");
                    ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
                    string NX11 = (System.IO.Path.GetDirectoryName(@NX11EXE));
                    string path1 = @NX11;
                    string path2 = "ugshext.dll";
                    string newPath = System.IO.Path.Combine(path1, path2); // newPath = "D:\temp\result.txt";
                    string newPath1 = "\"" + @newPath + "\""; //"\"" + path + "\""（path为原来的路径）
                    ///注册DLL
                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = "Regsvr32.exe";
                    p.StartInfo.Arguments =@newPath1;
                    p.Start();
                    ///ModernDialog.ShowMessage("修复NX11缩略图成功", "提示", MessageBoxButton.OK);
                }
                else
                {
                    if (CheckNX10() == true)
                    {
                        ///获取NX安装路径
                        RegistryKey driverKey = NXregistry();
                        string NX10EXE = (String)driverKey.GetValue("Unigraphics V28.0");
                        ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
                        string NX10 = (System.IO.Path.GetDirectoryName(@NX10EXE));
                        string path1 = @NX10;
                        string path2 = "ugshext.dll";
                        string newPath = System.IO.Path.Combine(path1, path2); // newPath = "D:\temp\result.txt";
                        string newPath1 = "\"" + @newPath + "\""; //"\"" + path + "\""（path为原来的路径）
                        ///注册DLL
                        System.Diagnostics.Process p = new System.Diagnostics.Process();
                        p.StartInfo.FileName = "Regsvr32.exe";
                        p.StartInfo.Arguments = @newPath1;
                        p.Start();
                        ///ModernDialog.ShowMessage("修复NX10缩略图成功", "提示", MessageBoxButton.OK);
                    }
                    else
                     if (CheckNX85() == true)
                    {
                        ///获取NX安装路径
                        RegistryKey driverKey = NXregistry();
                        string NX85EXE = (String)driverKey.GetValue("Unigraphics V26.5");
                        ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
                        string NX85 = (System.IO.Path.GetDirectoryName(@NX85EXE));
                        string path1 = @NX85;
                        string path2 = "ugshext.dll";
                        string newPath = System.IO.Path.Combine(path1, path2); // newPath = "D:\temp\result.txt";
                        string newPath1 = "\"" + @newPath + "\""; //"\"" + path + "\""（path为原来的路径）
                        ///注册DLL
                        System.Diagnostics.Process p = new System.Diagnostics.Process();
                        p.StartInfo.FileName = "Regsvr32.exe";
                        p.StartInfo.Arguments = @newPath1;
                        p.Start();
                        ///ModernDialog.ShowMessage("修复NX10缩略图成功", "提示", MessageBoxButton.OK);
                    }
                    {
                        ModernDialog.ShowMessage("您未安装NX软件", "警告", MessageBoxButton.OK);
                    }
                }
            }
            
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

        private bool CheckNX85()
        {
            RegistryKey driverKey = NXregistry();
            string NX85EXE = (String)driverKey.GetValue("Unigraphics V26.5");
            string NX85 = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@NX85EXE)));
            if (NX85 != null)
            {
                return true;
            }
            return false;
        }

        private void ModernButton_Click_2(object sender, RoutedEventArgs e)
        {
            RegistryKey rk = Registry.LocalMachine;
            RegistryKey rkvalue = rk.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager\Environment", true);
            //当如果表中存在该键值会直接覆盖之前的键值。
            rkvalue.SetValue("UGII_LANG", "simpl_chinese");
            ModernDialog.ShowMessage("更改成功，重启电脑生效", "提示", MessageBoxButton.OK);
        }

        private void ModernButton_Click_3(object sender, RoutedEventArgs e)
        {
            RegistryKey rk = Registry.LocalMachine;
            RegistryKey rkvalue = rk.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager\Environment", true);
            //当如果表中存在该键值会直接覆盖之前的键值。
            rkvalue.SetValue("UGII_LANG", "english");
            ModernDialog.ShowMessage("更改成功，重启电脑生效", "提示", MessageBoxButton.OK);
        }
    }
}
