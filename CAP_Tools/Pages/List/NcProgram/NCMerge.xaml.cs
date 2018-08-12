using FirstFloor.ModernUI.Windows.Controls;
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
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
            DialogResult result = m_Dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            string m_Dir = m_Dialog.SelectedPath.Trim();
            this.FileRoute.Text = m_Dir;
            ///判断选择的文件夹中是否含有后缀名为NC的文件
            if (System.IO.Directory.GetFiles(m_Dir, "*.nc").Length > 0)
            {
                ///如果存在，将替换按钮显示
                this.CL.IsEnabled = true;
                ///读取选择的文件夹中NC文件
                ///清空ListBox
                list.Items.Clear();
                string NcName = null;
                DirectoryInfo d = new DirectoryInfo(m_Dir);
                FileInfo[] Files = d.GetFiles("*.nc");
                List<string> lstr = new List<string>();
                ///获取文件夹下文件名，将路径显示到ListBox
                foreach (FileInfo file in Files)
                {
                    /// a = file.FullName; 路径名称在文件名
                    NcName = file.Name;
                    list.Items.Add(NcName);
                }
                }
                else
                {
                ///不存在
                this.CL.IsEnabled = false;
                ///清空ListBox
                list.Items.Clear();
                ModernDialog.ShowMessage("您选择的文件夹不存在.NC文件程序", "警告", MessageBoxButton.OK);
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
            //判断是否打开文件夹
            MessageBoxResult result = ModernDialog.ShowMessage("串联成功，是否打开文件所在目录？", "提示", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Process.Start("Explorer.exe", SelectFile);
            }
        }
    }
}
