using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Forms;

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
                    ///如果存在，将替换按钮显示
                    this.CL.IsEnabled = true;
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
                                ///截取第七行字符中两个指定字符间的字符
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
            sw.WriteLine("M05");
            sw.WriteLine("M09");
            sw.WriteLine("G28 Y0.0");
            sw.WriteLine("M30");
            sw.WriteLine("%");
            sw.Close();
            fs2.Close();
            File.Delete(TempFile);                                                    //删除Temp文件
            //判断是否复制串联好的文件到上级目录
            if (true == this.copy.IsChecked)
            {
                string a = (System.IO.Path.GetDirectoryName(FileRoute.Text)) + "\\程序";
                Directory.CreateDirectory(a);
                File.Copy(NewFile, a + "\\" + FolderName + ".nc", true);
                if (File.Exists(NewFile))
                {
                    File.Delete(NewFile);
                }
            }
            this.Dk.IsEnabled = true;
            ModernDialog.ShowMessage("程序串联成功                                ", "提示", MessageBoxButton.OK);
        }

        private void Dk_Click(object sender, RoutedEventArgs e)
        {
            if (true == this.copy.IsChecked)
            {
                string Path = (System.IO.Path.GetDirectoryName(FileRoute.Text)) + "\\程序";
                Process.Start("Explorer.exe", Path);
                this.Dk.IsEnabled = true;
            }
            else
            {
                string Path1 = FileRoute.Text;
                Process.Start("Explorer.exe", Path1);
            }
        }
    }

}
