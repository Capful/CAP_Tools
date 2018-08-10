using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CAP_Tools.Pages.List
{
    /// <summary>
    /// Interaction logic for BasicPage1.xaml
    /// </summary>
    public partial class BasicPage1 : System.Windows.Controls.UserControl
    {
        public BasicPage1()
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
            this.Route.Text = m_Dir;
            ///判断选择的文件夹中是否含有后缀名为NC的文件
            if (System.IO.Directory.GetFiles(m_Dir, "*.nc").Length > 0)
            {
                ///如果存在，将替换按钮显示
                this.CL.IsEnabled = true;
                ///读取选择的文件夹中NC文件
                ///清空ListBox
                abc.Items.Clear();
                string a = null;
                DirectoryInfo d = new DirectoryInfo(m_Dir);
                FileInfo[] Files = d.GetFiles("*.nc");
                List<string> lstr = new List<string>();
                ///获取文件夹下文件名，将路径显示到ListBox
                foreach (FileInfo file in Files)
                {
                    /// a = file.FullName; 路径名称在文件名
                    a = file.Name;
                    abc.Items.Add(new StudentInfo(a, "D10"));
                };
            }
            else
            {
                ///不存在
                this.CL.IsEnabled = false;
                ///清空ListBox
                abc.Items.Clear();
                ModernDialog.ShowMessage("您选择的文件夹不存在.NC文件程序", "警告", MessageBoxButton.OK);
            }

        }

        private void CL_Click(object sender, RoutedEventArgs e)
        {
            ///获取选择的路径
            string str = Route.Text;
            ///获取文件夹名字
            string SongName = str.Substring(str.LastIndexOf('\\') + 1);
            string a = str + "\\" + SongName + ".nc";
            string b = str + "\\" + "Temp" + ".txt";
            string c = "/select," + @a;
            ///合并文件夹内的文本，将值存在ALLText
            var builder = new StringBuilder();
            var files = System.IO.Directory.GetFiles(str, "*.nc");
            foreach (var file in files)
            {
                builder.Append(System.IO.File.ReadAllText(file));
            }
            var AllText = builder.ToString();
            using (StreamWriter writer = new StreamWriter(b))
            {
                ///合并文件
                writer.Write(AllText, Encoding.Default);
            }
            ///删除指定字符
            List<string> lines = new List<string>(File.ReadAllLines(b));
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
            File.WriteAllLines(b, lines.ToArray(), Encoding.Default);
            FileStream fs = new FileStream(b, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string line = sr.ReadToEnd();//直接读取全部
            sr.Close();
            fs.Close();
            FileStream fs2 = new FileStream(a, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs2, Encoding.Default);
            sw.WriteLine("%");
            sw.Write(line);
            sw.WriteLine("M05");
            sw.WriteLine("M09");
            sw.WriteLine("G28 Y0.0");
            sw.WriteLine("M30");
            sw.WriteLine("%");
            sw.Close();
            fs2.Close();
            File.Delete(b);
            MessageBoxResult result = ModernDialog.ShowMessage("串联成功，是否打开文件所在目录？", "提示", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Process.Start("Explorer.exe", c);
            }
        }
    }

    internal class StudentInfo
    {
        public string FileName { set; get; }
        public string Tool { set; get; }

        public StudentInfo(string FileName, string Tool)
        {
            this.FileName = FileName;
            this.Tool = Tool;
        }
    }
}
