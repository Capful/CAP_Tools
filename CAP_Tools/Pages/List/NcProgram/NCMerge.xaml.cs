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
    /// Interaction logic for NCMerge.xaml
    /// </summary>
    public partial class NCMerge : System.Windows.Controls.UserControl
    {
        public NCMerge()
        {
            InitializeComponent();

            string inifilePath = AppDomain.CurrentDomain.BaseDirectory + "NC Config\\" + Cap.IniFileName + ".ini";  //设置路径
            if (File.Exists(inifilePath))
            {
                Cap.WCS_Line = ReadIni("WCS_Config", "WCS_Line");
                Cap.WCS_Start = ReadIni("WCS_Config", "WCS_Start");
                Cap.WCS_End = ReadIni("WCS_Config", "WCS_End");

                Cap.T_Line = ReadIni("T_Config", "T_Line");
                Cap.T_Start = ReadIni("T_Config", "T_Start");
                Cap.T_End = ReadIni("T_Config", "T_End");

            }
            else
            {
                ModernDialog.ShowMessage(Cap.IniFileName + " 配置文件不存在，请检查", "警告", MessageBoxButton.OK);
            }
            
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
                string m_Dir = dialog.FileName;
                ///判断选择的文件夹中是否含有后缀名为NC的文件
                if (System.IO.Directory.GetFiles(m_Dir, "*.nc").Length > 0)
                {
                    this.FileRoute.Text = m_Dir;
                    ///如果存在，将替换按钮显示
                    this.CL.IsEnabled = true;
                    this.Dk.IsEnabled = false;
                    this.JC.IsEnabled = false;
                    ///读取选择的文件夹中NC文件
                    ///清空listView
                    listView.Items.Clear();
                    string FilePath = null;
                    string FileName2 = null;
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
                        FileName2 = file.Name;
                        //删除文件名中的刀具名称和坐标
                        string FileNames = FileName2;
                        FileNames = FileNames.Replace("]-", "]");
                        int index = FileNames.LastIndexOf(']'); //截取最后出现']'后面的所有字符
                        FileNames = FileNames.Substring(index + 1); //结果
                        ///文件夹下所有文件的坐标系
                        StreamReader Wcs_objReader = new StreamReader(FilePath);
                        int j = 0;
                        while ((WCS = Wcs_objReader.ReadLine()) != null)
                        {
                            j++;
                            ///第二行
                            if (j == Int32.Parse(Cap.WCS_Line))
                            {
                                ///截取字符中两个指定字符间的字符
                                WCS = InterceptStr(WCS, Cap.WCS_Start, Cap.WCS_End);
                                WCS = WCS.Trim(); //去除首尾空格
                                break;
                            }
                        }
                        Wcs_objReader.Close();//关闭流

                        ///文件夹下所有文件的刀具尺寸
                        StreamReader Tools_objReader = new StreamReader(FilePath);
                        int i = 0;
                        while ((Tools = Tools_objReader.ReadLine()) != null)
                        {
                            i++;
                            ///第七行
                            if (i == Int32.Parse(Cap.T_Line))
                            {
                                ///截取字符中两个指定字符间的字符
                                Tools = InterceptStr(Tools, Cap.T_Start, Cap.T_End);
                                Tools = Tools.Trim(); //去除首尾空格
                                break;
                            }
                        }
                        Tools_objReader.Close(); //关闭流
                                                 ///获取文件夹下文件名，将路径显示到listView
                        listView.Items.Add(new { A = FileNames, B = WCS, C = Tools });
                    }
                }
                else
                {
                    ///不存在
                    this.CL.IsEnabled = false;
                    ///清空ListBox
                    listView.Items.Clear();
                    ModernDialog.ShowMessage("您选择的文件夹不存在.NC文件程序", "警告", MessageBoxButton.OK);
                }
            }
            
        }

        private void CL_Click(object sender, RoutedEventArgs e)
        {
            ///设置路径变量
            string Path = FileRoute.Text;                                             //获取打开文件夹对话框选择的路径
            string FolderName = Path.Substring(Path.LastIndexOf('\\') + 1);           //获取选择的路径名称，获取\后的一串字符
            string NewFile = Path + "\\" + FolderName + ".nc";                        //将文件夹名字命名为新文件的名字
            string TempFile = Path + "\\" + "Temp" + ".txt";                          //缓存文件
            string SelectFile = "/select," + NewFile;                                 //打开文件夹并选中文件
            if (File.Exists(NewFile))
            {
                File.Delete(NewFile);
            }
            ///合并文件夹内的文本，将值存在ALLText
            var builder = new StringBuilder();
            var files = System.IO.Directory.GetFiles(Path, "*.nc");
            foreach (var file in files)
            {
                builder.Append(System.IO.File.ReadAllText(file));
            }
            var AllText = builder.ToString();
            using (StreamWriter writer = new StreamWriter(TempFile))
            {
                ///合并文件
                writer.Write(AllText, Encoding.Default);
            }
            ///删除指定字符
            List<string> lines = new List<string>(File.ReadAllLines(TempFile));
            ///删除G28 Y0.0
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            lines.Remove("G28 Y0.0");
            ///删除M30
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            lines.Remove("M30");
            ///删除M09
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            lines.Remove("M09");
            ///删除M05
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            lines.Remove("M05");
            ///删除%
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            lines.Remove("%");
            ///删除%
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            lines.Remove("T01 M06");
            File.WriteAllLines(TempFile, lines.ToArray(), Encoding.Default);          //将删除完指定字符的文件写入Temp文件
            ///创建串联程序
            FileStream fs = new FileStream(TempFile, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string line = sr.ReadToEnd();                                             //读取Temp文件，并储存在Line中
            sr.Close();
            fs.Close();
            FileStream fs2 = new FileStream(NewFile, FileMode.Append);                //创建新文件
            StreamWriter sw = new StreamWriter(fs2, Encoding.Default);
            sw.WriteLine("%");
            sw.Write(line);                                                           //释放Line
            string Program_End = ReadIni("Program_End", "End_Line");
            sw.WriteLine(Program_End.Replace("-/-", "\r\n"));
            sw.Close();
            fs2.Close();
            File.Delete(TempFile);                                                    //删除Temp文件
            //判断是否复制串联好的文件到上级目录
            string a = (System.IO.Path.GetDirectoryName(FileRoute.Text)) + "\\程序";
            this.NCFIle.Text = a + "\\" + FolderName + ".nc";
            this.NCFIle2.Text = NewFile;
            if (true == this.copy.IsChecked)
            {
                Directory.CreateDirectory(a);
                File.Copy(NewFile, NCFIle.Text, true);
                if (File.Exists(NewFile))
                {
                    File.Delete(NewFile);
                }
            }
            this.Dk.IsEnabled = true;
            this.JC.IsEnabled = true;
            ModernDialog.ShowMessage("程序串联成功                                ", "提示", MessageBoxButton.OK);
            //判断是否串联完成后打开文件所在目录
            if (true == this.op.IsChecked)
            {
                if (true == this.copy.IsChecked)
                {
                    if (File.Exists(NCFIle.Text))
                    {
                        Process.Start("Explorer.exe", "/select," + NCFIle.Text);
                    }
                    else
                    {
                        ModernDialog.ShowMessage("文件不存在，请重新串联程序", "警告", MessageBoxButton.OK);
                    }
                }
                else
                {
                    if (File.Exists(NCFIle2.Text))
                    {
                        Process.Start("Explorer.exe", "/select," + NCFIle2.Text);
                    }
                    else
                    {
                        ModernDialog.ShowMessage("文件不存在，请重新串联程序", "警告", MessageBoxButton.OK);
                    }
                }
            }
        }

        private void Dk_Click(object sender, RoutedEventArgs e)
        {
            if (true == this.copy.IsChecked)
            {
                if (File.Exists(NCFIle.Text))
                {
                    Process.Start("Explorer.exe", "/select," + NCFIle.Text);
                }
                else
                {
                    ModernDialog.ShowMessage("文件不存在，请重新串联程序", "警告", MessageBoxButton.OK);
                }
            }
            else
            {
                if (File.Exists(NCFIle2.Text))
                {
                    Process.Start("Explorer.exe", "/select," + NCFIle2.Text);
                }
                else
                {
                    ModernDialog.ShowMessage("文件不存在，请重新串联程序", "警告", MessageBoxButton.OK);
                }
            }
        }

        private void JC_Click(object sender, RoutedEventArgs e)
        {
            string NcViewer = AppDomain.CurrentDomain.BaseDirectory + "\\NCVIEWER\\NcViewer.exe";
            if (File.Exists(NcViewer))
            {
                if (true == this.copy.IsChecked)
                {
                    Process.Start(NcViewer, NCFIle.Text);
                }
                else
                {
                    Process.Start(NcViewer, NCFIle2.Text);
                }
            }
            else
            {
                ModernDialog.ShowMessage("NcViewer 软件不存在，请重新安装本软件", "警告", MessageBoxButton.OK);
            }
            
        }
        private string InterceptStr(string s, string str_start, string str_end)
        {
            try
            {
                int i = s.IndexOf(str_start) + str_start.Length;
                int j = s.IndexOf(str_end);
                string str_value = s.Substring(i, j - i);
                return str_value;
            }
            catch
            { return "错误"; }
        }

        private void FileRoute_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(FileRoute.Text))
            {
                ///如果存在
                Process.Start(FileRoute.Text);
            }
        }

        private string ReadIni(string section, string name)
        {
            string inifilePath = AppDomain.CurrentDomain.BaseDirectory + "NC Config\\" + Cap.IniFileName + ".ini";  //设置路径
            IniFile iniFile = new IniFile(inifilePath);
            string Ini = "";
            Ini = iniFile.ReadIni(section, name);
            return Ini;

        }
        
    }  
}
