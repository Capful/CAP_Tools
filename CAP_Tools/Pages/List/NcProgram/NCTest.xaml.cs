using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace CAP_Tools.Pages.List.NcProgram
{
    /// <summary>
    /// Interaction logic for NCTest.xaml
    /// </summary>
    public partial class NCTest : System.Windows.Controls.UserControl
    {


        public NCTest()
        {
            InitializeComponent();
            ///listView.ItemsSource = new Emp[]
            //{
            //   new Emp{A="小明",B=16,C="北京"},
            //   new Emp{A="小2",B=116,C="3京"},
            //  new Emp{A="小2",B=126,C="3京"},
            //};
        }

        public class Emp
        {
            public string A { get; set; }

            public int B { get; set; }

            public string C { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ///打开选择文件夹对话框
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
            DialogResult result = m_Dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            string m_Dir = m_Dialog.SelectedPath.Trim();
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
                ///读取选择的文件夹中NC文件
                ///清空ListBox
                listView.Items.Clear();
                string FilePath = null;
                string FileName = null;
                DirectoryInfo d = new DirectoryInfo(m_Dir);
                FileInfo[] Files = d.GetFiles("*.nc");
                List<string> lstr = new List<string>();
                ///获取文件夹下文件名，将路径显示到ListBox
                foreach (FileInfo file in Files)
                {
                    FilePath = file.FullName;
                    FileName = file.Name;
                    StreamReader objReader = new StreamReader(FilePath);
                    string sLine = "";
                    string sd = string.Empty;
                    int i = 0;
                    while ((sLine = objReader.ReadLine()) != null)
                    {
                        i++;
                        if (i == 7)
                        {
                            sd = sLine;
                            break;
                        }
                    }
                    string G = "G43";

                    listView.Items.Add(new { A = FileName, B = sLine });
                }
            }
        }
    }
}
