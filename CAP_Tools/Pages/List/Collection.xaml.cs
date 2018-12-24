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

        private static RegistryKey NXregistry()
        {
            return Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Unigraphics Solutions\Installed Applications");
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
                        p.StartInfo.Arguments = @newPath1;
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
                        {
                            ModernDialog.ShowMessage("您未安装NX软件", "警告", MessageBoxButton.OK);
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
                string Post1 = AppDomain.CurrentDomain.BaseDirectory + @"Postprocessor\CAP Post";
                MessageBoxResult result = ModernDialog.ShowMessage("确定要安装后处理文件吗？我们将会备份原始文件。", "提示", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    if (Directory.Exists(Post1))//判断是否存在
                    {
                        if (CheckNX10() == true)
                        {
                            ///获取NX安装路径
                            RegistryKey driverKey = NXregistry();
                            string EXE = (String)driverKey.GetValue("Unigraphics V28.0");
                            string Home = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@EXE)));
                            string Post2 = Home + @"\MACH\resource\postprocessor";
                            ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
                            ///打开主目录
                            if (File.Exists(Post2 + "\\template_post.dat"))
                            {
                                if (File.Exists(Post2 + "\\template_post.dat.bak"))
                                {
                                }
                                else
                                {
                                    File.Move(Post2 + "\\template_post.dat", Post2 + "\\template_post.dat.bak");
                                }
                            }
                            Copy(Post1, Post2);
                        }
                        if (CheckNX11() == true)
                        {
                            ///获取NX安装路径
                            RegistryKey driverKey = NXregistry();
                            string EXE = (String)driverKey.GetValue("Unigraphics V29.0");
                            string Home = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@EXE)));
                            string Post2 = Home + @"\MACH\resource\postprocessor";
                            ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
                            ///打开主目录
                            if (File.Exists(Post2 + "\\template_post.dat"))
                            {
                                if (File.Exists(Post2 + "\\template_post.dat.bak"))
                                {
                                }
                                else
                                {
                                    File.Move(Post2 + "\\template_post.dat", Post2 + "\\template_post.dat.bak");
                                }
                            }
                            Copy(Post1, Post2);
                        }
                        if (CheckNX12() == true)
                        {
                            ///获取NX安装路径
                            RegistryKey driverKey = NXregistry();
                            string EXE = (String)driverKey.GetValue("Unigraphics V30.0");
                            string Home = (System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@EXE)));
                            string Post2 = Home + @"\MACH\resource\postprocessor";
                            ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
                            ///打开主目录
                            if (File.Exists(Post2 + "\\template_post.dat"))
                            {
                                if (File.Exists(Post2 + "\\template_post.dat.bak"))
                                {
                                }
                                else
                                {
                                    File.Move(Post2 + "\\template_post.dat", Post2 + "\\template_post.dat.bak");
                                }
                            }
                            Copy(Post1, Post2);
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
    }
}
