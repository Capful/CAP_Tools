using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using static CAP_Tools.MainWindow;

namespace CAP_Tools.Pages.List.NcProgram
{
    /// <summary>
    /// Interaction logic for NCWcs.xaml
    /// </summary>
    public partial class NCWcs : System.Windows.Controls.UserControl
    {
        public NCWcs()
        {
            InitializeComponent();
        }
        private void Xz_Click(object sender, RoutedEventArgs e)
        {
            ///打开选择文件夹对话框
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Cap.NCFileRoute = dialog.FileName;
                string XZPath = Cap.NCFileRoute;
                ///判断选择的文件夹中是否含有后缀名为NC的文件
                if (System.IO.Directory.GetFiles(XZPath, "*.nc").Length > 0)
                {
                    FileRoute.Text = XZPath;
                    ///如果存在，将替换按钮显示
                    this.Th.IsEnabled = true;
                    this.Dk.IsEnabled = false;
                    ///读取选择的文件夹中NC文件
                    ///清空listView
                    listView.Items.Clear();
                    string FullName = null;
                    string FileName = null;
                    string Tools = null;
                    string WCS = null;
                    ///读取ini文件数据
                    string inifilePath = AppDomain.CurrentDomain.BaseDirectory + "NC Config\\" + Cap.IniFileName + ".ini";  //设置路径
                    IniFile iniFile = new IniFile(inifilePath);
                    if (File.Exists(inifilePath))
                    {
                        ///读取ini文件数据

                        WCS_Line.Text = iniFile.ReadIni("WCS_Config", "WCS_Line");
                        WCS_Start.Text = iniFile.ReadIni("WCS_Config", "WCS_Start");
                        WCS_End.Text = iniFile.ReadIni("WCS_Config", "WCS_End");

                        T_Line.Text = iniFile.ReadIni("T_Config", "T_Line");
                        T_Start.Text = iniFile.ReadIni("T_Config", "T_Start");
                        T_End.Text = iniFile.ReadIni("T_Config", "T_End");

                        //iniFile.writeIni("section1", "key1", "value11"); //写
                    }
                    else
                    {
                        ModernDialog.ShowMessage(Cap.IniFileName + " 配置文件不存在，请检查", "警告", MessageBoxButton.OK);
                    }
                    
                    DirectoryInfo d = new DirectoryInfo(XZPath);
                    FileInfo[] Files = d.GetFiles("*.nc");
                    List<string> lstr = new List<string>();
                    ///获取文件夹下文件名，将路径显示到listView
                    foreach (FileInfo file in Files)
                    {
                        ///获取文件夹下文件名
                        FullName = file.FullName;
                        FileName = file.Name;
                        ///获取文件夹下所有程序的坐标系
                        StreamReader Wcs_objReader = new StreamReader(FullName);
                        int j = 0;
                        while ((WCS = Wcs_objReader.ReadLine()) != null)
                        {
                            j++;
                            ///第二行
                            if (j == 2)
                            {
                                ///截取第二行字符中两个指定字符间的字符
                                int k = WCS.IndexOf(WCS_Start.Text);//找a的位置
                                int l = WCS.IndexOf(WCS_End.Text);//找b的位置
                                WCS = (WCS.Substring(k + 1)).Substring(0, l - k - 1);
                                WCS = WCS.Trim(); //去除首尾空格
                                break;
                            }
                        }
                        Wcs_objReader.Close();//关闭流
                        ///获取文件夹下所有程序的刀具尺寸
                        StreamReader Tools_objReader = new StreamReader(FullName);
                        int i = 0;
                        while ((Tools = Tools_objReader.ReadLine()) != null)
                        {
                            i++;
                            ///第七行
                            if (i == 7)
                            {
                                ///截取第七行字符中两个指定字符间的字符
                                int s = Tools.IndexOf(T_Start.Text);//找a的位置
                                int g = Tools.IndexOf(T_End.Text);//找b的位置
                                Tools = (Tools.Substring(s + 1)).Substring(0, g - s - 1);
                                Tools = Tools.Trim(); //去除首尾空格
                                break;
                            }
                        }
                        Tools_objReader.Close(); //关闭流
                        ///将文件名，坐标系，刀具名称添加到ListView
                        listView.Items.Add(new { A = FileName, B = WCS, C = Tools });
                        this.WCS.Text = WCS;

                    }
                    ///判断程序是否有坐标系
                    if (WCS.Contains("G5"))
                    {
                        ///包含坐标系
                    }
                    else
                    {
                        ModernDialog.ShowMessage("程序中不存在坐标系", "警告", MessageBoxButton.OK);
                        ///不存在
                        this.Th.IsEnabled = false;
                    }

                }
                else
                {
                    ///不存在
                    this.Th.IsEnabled = false;
                    ///清空ListBox
                    listView.Items.Clear();
                    ModernDialog.ShowMessage("您选择的文件夹不存在.NC文件程序", "警告", MessageBoxButton.OK);
                }
            }
            
        }


        private void Th_Click(object sender, RoutedEventArgs e)
        {
            ///批量替换文本中的值
            string FilePath = (Path.GetDirectoryName(FileRoute.Text)) + "\\程序串联";
            string WcsPath = WCS.Text; //文件中的坐标值
            string AWcsPath = AWCS.Text; //输入的坐标值
            string[] pathFile = Directory.GetFiles(FileRoute.Text);
            string Th = "";
            foreach (string str in pathFile)
            {
                FileStream fs = new FileStream(str, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs, Encoding.Default);
                Th = sr.ReadToEnd();
                ///查找文本中的WcsPath，替换为AWcsPath
                Th = Th.Replace(WcsPath, AWcsPath);
                sr.Close();
                fs.Close();
                FileStream fs2 = new FileStream(str, FileMode.Open, FileAccess.Write);
                ///Encoding.Default 参照电脑默认编码保存文件
                StreamWriter sw = new StreamWriter(fs2, Encoding.Default);
                sw.WriteLine(Th);
                sw.Close();
                fs2.Close();
            }
            this.Dk.IsEnabled = true;
            ModernDialog.ShowMessage("替换坐标成功                                ", "提示", MessageBoxButton.OK);
            //判断是否修改完坐标后打开文件所在目录
            if (true == this.op.IsChecked)
            {
                if (true == this.copy.IsChecked)
                {
                    string SPath1 = (System.IO.Path.GetDirectoryName(FileRoute.Text)) + "\\程序串联";
                    Process.Start("Explorer.exe", SPath1);
                }
                else
                {
                    string Path2 = FileRoute.Text;
                    Process.Start("Explorer.exe", Path2);
                }
            }
            ///重新检测坐标
            ///清空listView
            listView.Items.Clear();
            string XZPath = Cap.NCFileRoute;
            string FullName = null;
            string FileName = null;
            string Tools = null;
            string WCS_A = null;
            DirectoryInfo d = new DirectoryInfo(XZPath);
            FileInfo[] Files = d.GetFiles("*.nc");
            List<string> lstr = new List<string>();
            ///获取WCS坐标添加到listView
            foreach (FileInfo file in Files)
            {
                ///获取文件夹下文件名
                FullName = file.FullName;
                FileName = file.Name;
                ///获取文件夹下所有程序的坐标系
                StreamReader Wcs_objReader = new StreamReader(FullName);
                int j = 0;
                while ((WCS_A = Wcs_objReader.ReadLine()) != null)
                {
                    j++;
                    ///第二行
                    if (j == 2)
                    {
                        ///截取第二行字符中两个指定字符间的字符
                        int k = WCS_A.IndexOf("9");//找a的位置
                        int l = WCS_A.IndexOf("G8");//找b的位置
                        WCS_A = (WCS_A.Substring(k + 1)).Substring(0, l - k - 1);
                        WCS_A = WCS_A.Trim(); //去除首尾空格
                        break;
                    }
                }
                Wcs_objReader.Close();//关闭流
                                      ///获取文件夹下所有程序的刀具尺寸
                StreamReader Tools_objReader = new StreamReader(FullName);
                int i = 0;
                while ((Tools = Tools_objReader.ReadLine()) != null)
                {
                    i++;
                    ///第七行
                    if (i == 7)
                    {
                        ///截取第七行字符中两个指定字符间的字符
                        int s = Tools.IndexOf("(");//找a的位置
                        int g = Tools.IndexOf("-");//找b的位置
                        Tools = (Tools.Substring(s + 1)).Substring(0, g - s - 1);
                        Tools = Tools.Trim(); //去除首尾空格
                        break;
                    }
                }
                Tools_objReader.Close(); //关闭流
                                         ///将文件名，坐标系，刀具名称添加到ListView
                listView.Items.Add(new { A = FileName, B = WCS_A, C = Tools });
                ///重新检测坐标系添加到ListView
                WCS.Text = WCS_A;
                //判断是否复制串联好的文件到上级目录
                if (true == this.copy.IsChecked)
                {
                    //将刀具名称和坐标系加入到文件名中
                    string NewFile = FilePath + "\\" + "[" + Tools + "]" + "[" + WCS_A + "]-" + FileName;
                    Directory.CreateDirectory(FilePath);
                    File.Copy(FullName, NewFile, true);
                }
            }

        }
        private void Dk_Click(object sender, RoutedEventArgs e)
        {
            if (true == this.copy.IsChecked)
            {
                string SPath = Path.GetDirectoryName(FileRoute.Text) + "\\程序串联";
                if (Directory.Exists(SPath))
                {
                    ///如果存在
                    Process.Start("Explorer.exe", SPath);
                }
                else
                {
                    ///如果不存在
                    ModernDialog.ShowMessage("路径不存在，请重新勾选复制按钮进行替换后重试", "警告", MessageBoxButton.OK);
                }
                
            }
            else
            {
                Process.Start("Explorer.exe", FileRoute.Text);
            }
        }
        public static void CopyDirectory(string srcPath, string destPath)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(srcPath);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)     //判断是否文件夹
                    {
                        if (!Directory.Exists(destPath + "\\" + i.Name))
                        {
                            Directory.CreateDirectory(destPath + "\\" + i.Name);   //目标目录下不存在此文件夹即创建子文件夹
                        }
                        CopyDirectory(i.FullName, destPath + "\\" + i.Name);    //递归调用复制子文件夹
                    }
                    else
                    {
                        File.Copy(i.FullName, destPath + "\\" + i.Name, true);      //不是文件夹即复制文件，true表示可以覆盖同名文件
                    }
                }
            }
            catch (Exception )
            {
                throw;
            }
        }
    }
}