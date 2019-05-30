using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Forms;

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
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string m_Dir = dialog.FileName;
                ///判断选择的文件夹中是否含有后缀名为NC的文件
                if (System.IO.Directory.GetFiles(m_Dir, "*.nc").Length > 0)
                {
                    this.FileRoute.Text = m_Dir;
                    ///将选择的路径写入当前程序运行路径下的FileRoute.ini文件中
                    FileStream a = File.Create(AppDomain.CurrentDomain.BaseDirectory + "FileRoute.ini");
                    StreamWriter sw = new StreamWriter(a);
                    sw.WriteLine(m_Dir);
                    sw.Close();
                    a.Close();
                    ///如果存在，将替换按钮显示
                    this.Th.IsEnabled = true;
                    this.Dk.IsEnabled = false;
                    ///读取选择的文件夹中NC文件
                    ///清空listView
                    listView.Items.Clear();
                    string FilePath = null;
                    string FileName = null;
                    string Tools = null;
                    string WCS = null;
                    DirectoryInfo d = new DirectoryInfo(m_Dir);
                    FileInfo[] Files = d.GetFiles("*.nc");
                    List<string> lstr = new List<string>();
                    ///获取文件夹下文件名，将路径显示到listView
                    foreach (FileInfo file in Files)
                    {
                        ///获取文件夹下文件名
                        FilePath = file.FullName;
                        FileName = file.Name;
                        ///文件夹下所有文件的坐标系
                        StreamReader Wcs_objReader = new StreamReader(FilePath);
                        string WCS_A = string.Empty;
                        int j = 0;
                        while ((WCS = Wcs_objReader.ReadLine()) != null)
                        {
                            j++;
                            ///第二行
                            if (j == 2)
                            {
                                WCS_A = WCS;
                                ///截取第二行字符中两个指定字符间的字符
                                int k = WCS.IndexOf("9");//找a的位置
                                int l = WCS.IndexOf("G8");//找b的位置
                                WCS = (WCS.Substring(k + 1)).Substring(0, l - k - 1);
                                break;
                            }
                        }
                        Wcs_objReader.Close();//关闭流
                        ///文件夹下所有文件的刀具尺寸
                        StreamReader Tools_objReader = new StreamReader(FilePath);
                        string Tools_A = string.Empty;

                        int i = 0;
                        while ((Tools = Tools_objReader.ReadLine()) != null)
                        {
                            i++;
                            ///第七行
                            if (i == 7)
                            {
                                Tools_A = Tools;
                                ///截取第七行字符中两个指定字符间的字符
                                int s = Tools.IndexOf("(");//找a的位置
                                int g = Tools.IndexOf("-");//找b的位置
                                Tools = (Tools.Substring(s + 1)).Substring(0, g - s - 1);
                                break;
                            }
                        }
                        Tools_objReader.Close(); //关闭流
                        ///获取文件夹下文件名，将路径显示到listView
                        listView.Items.Add(new { A = FileName, B = WCS, C = Tools });

                    }

                    string WCSA = WCS;
                    ///去除字符首尾空格
                    WCSA = WCSA.Trim();
                    this.WCS.Text = WCSA;
                    ///判断程序是否有坐标系
                    if (WCSA.Contains("G5"))
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
            string G54 = AWCS.Text;
            ///判断程序是否有坐标系
            if (G54.Contains("G5"))
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
                //判断是否复制串联好的文件到上级目录
                if (true == this.copy.IsChecked)
                {
                    Directory.CreateDirectory((System.IO.Path.GetDirectoryName(FileRoute.Text)) + "\\程序串联");
                    CopyDirectory(FileRoute.Text, FilePath);
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
                string FilePath_A = null;
                string FileName = null;
                string Tools = null;
                string WCS2 = null;
                DirectoryInfo d = new DirectoryInfo(FileRoute.Text);
                FileInfo[] Files = d.GetFiles("*.nc");
                List<string> lstr = new List<string>();
                ///获取文件夹下文件名，将路径显示到listView
                foreach (FileInfo file in Files)
                {
                    ///获取文件夹下文件名
                    FilePath_A = file.FullName;
                    FileName = file.Name;
                    ///文件夹下所有文件的坐标系
                    StreamReader Wcs_objReader = new StreamReader(FilePath_A);
                    string WCS_A = string.Empty;
                    int j = 0;
                    while ((WCS2 = Wcs_objReader.ReadLine()) != null)
                    {
                        j++;
                        ///第二行
                        if (j == 2)
                        {
                            WCS_A = WCS2;
                            ///截取第七行字符中两个指定字符间的字符
                            int k = WCS2.IndexOf("9");//找a的位置
                            int l = WCS2.IndexOf("G8");//找b的位置
                            WCS2 = (WCS2.Substring(k + 1)).Substring(0, l - k - 1);
                            break;
                        }
                    }
                    Wcs_objReader.Close();//关闭流
                    ///文件夹下所有文件的刀具尺寸
                    StreamReader Tools_objReader = new StreamReader(FilePath_A);
                    string Tools_A = string.Empty;

                    int i = 0;
                    while ((Tools = Tools_objReader.ReadLine()) != null)
                    {
                        i++;
                        ///第七行
                        if (i == 7)
                        {
                            Tools_A = Tools;
                            ///截取第七行字符中两个指定字符间的字符
                            int s = Tools.IndexOf("(");//找a的位置
                            int g = Tools.IndexOf("-");//找b的位置
                            Tools = (Tools.Substring(s + 1)).Substring(0, g - s - 1);
                            break;
                        }
                    }
                    Tools_objReader.Close(); //关闭流
                                             ///获取文件夹下文件名，将路径显示到listView
                    listView.Items.Add(new { A = FileName, B = WCS2, C = Tools });
                }
                ///完成后检测文件夹中的值，并返回
                string WCSA = WCS2;
                ///去除字符首尾空格
                WCSA = WCSA.Trim();
                this.WCS.Text = WCSA;
            }
            else
            {
                ModernDialog.ShowMessage("输入错误！！！，请输入坐标系！或检查大小写！", "警告", MessageBoxButton.OK);
            }
            
        }
        private void Dk_Click(object sender, RoutedEventArgs e)
        {
            if (true == this.copy.IsChecked)
            {
                string Path = FileRoute.Text;
                string SPath = (System.IO.Path.GetDirectoryName(Path)) + "\\程序串联";
                Process.Start("Explorer.exe", SPath);
                this.Dk.IsEnabled = true;
            }
            else
            {
                string Path1 = FileRoute.Text;
                Process.Start("Explorer.exe", Path1);
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
            catch (Exception e)
            {
                throw;
            }
        }
    }
}