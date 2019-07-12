using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
using System;
using System.IO;
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

        private void NXSLT_Click(object sender, RoutedEventArgs e)
        {
            ///判断NX是否安装，如果有安装再判断详细版本
            ///判断注册表项是否存在
            RegistryKey NX = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Unigraphics Solutions\Installed Applications");
            if (NX == null)
            {
                ModernDialog.ShowMessage("您未安装NX软件", "警告", MessageBoxButton.OK);
            }
            else
            {
                if (CheckNX1872() == true)
                {
                    Ugshext("Unigraphics V32.0");
                }
                else
                {
                    if (CheckNX1847() == true)
                    {
                        Ugshext("Unigraphics V31.0");
                    }
                    else
                    {
                        if (CheckNX12() == true)
                        {
                            Ugshext("Unigraphics V30.0");
                        }
                        else
                        {
                            if (CheckNX11() == true)
                            {
                                Ugshext("Unigraphics V29.0");
                            }
                            else
                            {
                                if (CheckNX10() == true)
                                {
                                    Ugshext("Unigraphics V28.0");
                                }
                                else
                                {
                                    ModernDialog.ShowMessage("您未安装NX软件", "警告", MessageBoxButton.OK);
                                }
                            }
                        }
                    }
                }
                    
            }
            
        }

        private void CN_Click(object sender, RoutedEventArgs e)
        {
            ///判断NX是否安装，如果有安装再判断详细版本
            ///判断注册表项是否存在
            RegistryKey NX = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Unigraphics Solutions\Installed Applications");
            if (NX == null)
            {
                ModernDialog.ShowMessage("您未安装NX软件", "警告", MessageBoxButton.OK);
            }
            else
            {
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey rkvalue = rk.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager\Environment", true);
                //当如果表中存在该键值会直接覆盖之前的键值。
                rkvalue.SetValue("UGII_LANG", "simpl_chinese");
                ModernDialog.ShowMessage("更改成功，重启电脑生效", "提示", MessageBoxButton.OK);
            }
        }

        private void EN_Click(object sender, RoutedEventArgs e)
        {
            ///判断NX是否安装，如果有安装再判断详细版本
            ///判断注册表项是否存在
            RegistryKey NX = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Unigraphics Solutions\Installed Applications");
            if (NX == null)
            {
                ModernDialog.ShowMessage("您未安装NX软件", "警告", MessageBoxButton.OK);
            }
            else
            {
                RegistryKey rk = Registry.LocalMachine;
                RegistryKey rkvalue = rk.OpenSubKey(@"SYSTEM\CurrentControlSet\Control\Session Manager\Environment", true);
                //当如果表中存在该键值会直接覆盖之前的键值。
                rkvalue.SetValue("UGII_LANG", "english");
                ModernDialog.ShowMessage("更改成功，重启电脑生效", "提示", MessageBoxButton.OK);
            }
        }

        private void General_Miller_Click(object sender, RoutedEventArgs e)
        {
            ///判断NX是否安装，如果有安装再判断详细版本
            ///判断注册表项是否存在
            RegistryKey NX = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Unigraphics Solutions\Installed Applications");
            if (NX == null)
            {
                ModernDialog.ShowMessage("您未安装NX软件", "警告", MessageBoxButton.OK);
            }
            else
            {
                MessageBoxResult result = ModernDialog.ShowMessage("确定要安装后处理文件吗？我们将会备份原始文件。", "提示", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    string Post1 = AppDomain.CurrentDomain.BaseDirectory + @"CAP Post";
                    if (Directory.Exists(Post1))//判断是否存在
                    {
                        if (CheckNX10() == true)
                        {
                            Post("Unigraphics V28.0");
                        }
                        if (CheckNX11() == true)
                        {
                            Post("Unigraphics V29.0");
                        }
                        if (CheckNX12() == true)
                        {
                            Post("Unigraphics V30.0");
                        }
                        if (CheckNX1847() == true)
                        {
                            Post("Unigraphics V31.0");
                        }
                        if (CheckNX1872() == true)
                        {
                            Post("Unigraphics V32.0");
                        }
                        ModernDialog.ShowMessage("后处理文件安装完成，已将原始菜单文件后缀名改为.bak", "提示", MessageBoxButton.OK);
                    }
                    else
                    {
                        ModernDialog.ShowMessage("未找到后处理文件，请检查安装包是否完整。或者联系Capful", "警告", MessageBoxButton.OK);
                    }
                }
            }

        }

        /// <summary>
        /// 复制文件夹中的所有内容
        /// </summary>
        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
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

        /// <summary>
        ///  注册NX缩略图,注册DLL文件
        /// </summary>
        /// <param name="Versions">NX注册表版本,如"Unigraphics V28.0"</param>
        private void Ugshext(string Versions)
        {
            ///获取NX安装路径
            string NXEXE = GetNXEXE(Versions);
            ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
            string NXPath = (System.IO.Path.GetDirectoryName(NXEXE));
            string Ugshext = "\"" + NXPath + "\\" + "ugshext.dll" + "\"";
            ///注册DLL
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "Regsvr32.exe";
            p.StartInfo.Arguments = Ugshext;
            p.Start();
            ///ModernDialog.ShowMessage("修复NX11缩略图成功", "提示", MessageBoxButton.OK);
        }

        /// <summary>
        ///  安装NX后处理文件
        /// </summary>
        /// <param name="Versions">NX注册表版本,如"Unigraphics V28.0"</param>
        private void Post(string Versions)
        {
            string Post1 = AppDomain.CurrentDomain.BaseDirectory + @"CAP Post";
            ///获取NX安装路径
            string NXEXE = GetNXEXE(Versions);
            string NXPath = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(NXEXE)));
            string Post2 = NXPath + @"\MACH\resource\postprocessor";
            ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
            ///打开主目录
            if (File.Exists(Post2 + "\\template_post.dat"))
            {
                if (File.Exists(Post2 + "\\template_post.dat.bak"))
                {
                    ///存在
                }
                else
                {
                    File.Move(Post2 + "\\template_post.dat", Post2 + "\\template_post.dat.bak");
                }
            }
            Copy(Post1, Post2);
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
