using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for NcProgram.xaml
    /// </summary>
    public partial class NcProgram : System.Windows.Controls.UserControl
    {
        public NcProgram()
        {
            InitializeComponent();
        }

        private void Xz_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog m_Dialog = new FolderBrowserDialog();
            DialogResult result = m_Dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            string m_Dir = m_Dialog.SelectedPath.Trim();
            this.NXRoute.Text = m_Dir;

            if (System.IO.Directory.GetFiles(m_Dir, "*.nc").Length > 0)
            {
                this.Th.IsEnabled = true;
                string s = null;
                DirectoryInfo d = new DirectoryInfo(m_Dir);
                FileInfo[] Files = d.GetFiles("*.nc");
                List<string> lstr = new List<string>();

                foreach (FileInfo file in Files)
                {
                    s = file.FullName;
                    lstr.Add(s);
                }
                string filePath = s;
                if (!File.Exists(filePath))
                {
                    return;
                }
                var filest = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
                using (var sr = new StreamReader(filest))
                {
                    string Line1 = sr.ReadLine(); //读取文件中的一行
                    string Line2 = sr.ReadLine();
                    string Line3 = sr.ReadLine();
                    string Line4 = sr.ReadLine();
                    string Line = System.IO.Path.Combine(Line1, Line2, Line3, Line4);
                    sr.Close(); //关闭流
                    filest.Close();
                    string t54 = Line;
                    string G54 = "G54";
                    if (t54.Contains(G54))
                    {
                        this.WCS.Text = "G54";
                    }
                    else
                    {
                        string t55 = Line;
                        string G55 = "G55";
                        if (t55.Contains(G55))
                        {
                            this.WCS.Text = "G55";
                        }
                        else
                        {
                            string t56 = Line;
                            string G56 = "G56";
                            if (t55.Contains(G56))
                            {
                                this.WCS.Text = "G56";
                            }
                            else
                            {
                                string t57 = Line;
                                string G57 = "G57";
                                if (t57.Contains(G57))
                                {
                                    this.WCS.Text = "G57";
                                }
                                else
                                {
                                    string t58 = Line;
                                    string G58 = "G58";
                                    if (t58.Contains(G58))
                                    {
                                        this.WCS.Text = "G58";
                                    }
                                    else
                                    {
                                        string t100 = Line;
                                        string G100 = "G100";
                                        if (t100.Contains(G100))
                                        {
                                            this.WCS.Text = "G100";
                                        }
                                        else
                                        {
                                            string t591 = Line;
                                            string G591 = "G59.1";
                                            if (t591.Contains(G591))
                                            {
                                                this.WCS.Text = "G59.1";
                                            }
                                            else
                                            {
                                                string t592 = Line;
                                                string G592 = "G59.2";
                                                if (t592.Contains(G592))
                                                {
                                                    this.WCS.Text = "G59.2";
                                                }
                                                else
                                                {
                                                    string t593 = Line;
                                                    string G593 = "G59.3";
                                                    if (t593.Contains(G593))
                                                    {
                                                        this.WCS.Text = "G59.3";
                                                    }
                                                    else
                                                    {
                                                        string t594 = Line;
                                                        string G594 = "G59.4 ";
                                                        if (t594.Contains(G594))
                                                        {
                                                            this.WCS.Text = "G59.4";
                                                        }
                                                        else
                                                        {
                                                            string t595 = Line;
                                                            string G595 = "G59.5";
                                                            if (t595.Contains(G595))
                                                            {
                                                                this.WCS.Text = "G59.5";
                                                            }
                                                            else
                                                            {
                                                                string t596 = Line;
                                                                string G596 = "G59.6";
                                                                if (t596.Contains(G596))
                                                                {
                                                                    this.WCS.Text = "G59.6";
                                                                }
                                                                else
                                                                {
                                                                    string t597 = Line;
                                                                    string G597 = "G59.7";
                                                                    if (t597.Contains(G597))
                                                                    {
                                                                        this.WCS.Text = "G59.7";
                                                                    }
                                                                    else
                                                                    {
                                                                        string t598 = Line;
                                                                        string G598 = "G59.8";
                                                                        if (t598.Contains(G598))
                                                                        {
                                                                            this.WCS.Text = "G59.8";
                                                                        }
                                                                        else
                                                                        {
                                                                            string t599 = Line;
                                                                            string G599 = "G59.9";
                                                                            if (t599.Contains(G599))
                                                                            {
                                                                                this.WCS.Text = "G59.9";
                                                                            }
                                                                            else
                                                                            {
                                                                                ModernDialog.ShowMessage("您所选的文件夹可能有错，或不包含坐标，本工具只检测程序前三行是否有G54-G59.9，不支持G59", "警告", MessageBoxButton.OK);
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                this.Th.IsEnabled = false;
                ModernDialog.ShowMessage("您选择的文件夹不存在.NC文件程序", "警告", MessageBoxButton.OK);
            }
            
        }


        private void Th_Click(object sender, RoutedEventArgs e)
        {
            if (AWCS.Text == "G59")
            {
                ModernDialog.ShowMessage("暂不支持G59坐标系，请使用G59.1以后坐标系", "警告", MessageBoxButton.OK);
            }
            else
            {
            string path = NXRoute.Text;
            string path1 = WCS.Text;
            string path2 = AWCS.Text;
            string[] pathFile = Directory.GetFiles(path);
            string con = "";
            foreach (string str in pathFile)
            {
                FileStream fs = new FileStream(str, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs, Encoding.Default);
                con = sr.ReadToEnd();
                con = con.Replace(path1, path2);
                sr.Close();
                fs.Close();
                FileStream fs2 = new FileStream(str, FileMode.Open, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs2, Encoding.Default);
                sw.WriteLine(con);
                sw.Close();
                fs2.Close();
            }
            ModernDialog.ShowMessage("替换成功", "提示", MessageBoxButton.OK);
            string s = null;
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles("*.nc");
            List<string> lstr = new List<string>();

            foreach (FileInfo file in Files)
            {
                s = file.FullName;
                lstr.Add(s);
            }
            string filePath = s;
            if (!File.Exists(filePath))
            {
                return;
            }
            var filest = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            using (var sr = new StreamReader(filest))
            {
                string Line1 = sr.ReadLine(); //读取文件中的一行
                string Line2 = sr.ReadLine();
                string Line3 = sr.ReadLine();
                string Line4 = sr.ReadLine();
                string Line = System.IO.Path.Combine(Line1, Line2, Line3, Line4);
                sr.Close(); //关闭流
                filest.Close();
                string t54 = Line;
                string G54 = "G54";
                if (t54.Contains(G54))
                {
                    this.WCS.Text = "G54";
                }
                else
                {
                    string t55 = Line;
                    string G55 = "G55";
                    if (t55.Contains(G55))
                    {
                        this.WCS.Text = "G55";
                    }
                    else
                    {
                        string t56 = Line;
                        string G56 = "G56";
                        if (t55.Contains(G56))
                        {
                            this.WCS.Text = "G56";
                        }
                        else
                        {
                            string t57 = Line;
                            string G57 = "G57";
                            if (t57.Contains(G57))
                            {
                                this.WCS.Text = "G57";
                            }
                            else
                            {
                                string t58 = Line;
                                string G58 = "G58";
                                if (t58.Contains(G58))
                                {
                                    this.WCS.Text = "G58";
                                }
                                else
                                {
                                    string t100 = Line;
                                    string G100 = "G100";
                                    if (t100.Contains(G100))
                                    {
                                        this.WCS.Text = "G100";
                                    }
                                    else
                                    {
                                        string t591 = Line;
                                        string G591 = "G59.1";
                                        if (t591.Contains(G591))
                                        {
                                            this.WCS.Text = "G59.1";
                                        }
                                        else
                                        {
                                            string t592 = Line;
                                            string G592 = "G59.2";
                                            if (t592.Contains(G592))
                                            {
                                                this.WCS.Text = "G59.2";
                                            }
                                            else
                                            {
                                                string t593 = Line;
                                                string G593 = "G59.3";
                                                if (t593.Contains(G593))
                                                {
                                                    this.WCS.Text = "G59.3";
                                                }
                                                else
                                                {
                                                    string t594 = Line;
                                                    string G594 = "G59.4 ";
                                                    if (t594.Contains(G594))
                                                    {
                                                        this.WCS.Text = "G59.4";
                                                    }
                                                    else
                                                    {
                                                        string t595 = Line;
                                                        string G595 = "G59.5";
                                                        if (t595.Contains(G595))
                                                        {
                                                            this.WCS.Text = "G59.5";
                                                        }
                                                        else
                                                        {
                                                            string t596 = Line;
                                                            string G596 = "G59.6";
                                                            if (t596.Contains(G596))
                                                            {
                                                                this.WCS.Text = "G59.6";
                                                            }
                                                            else
                                                            {
                                                                string t597 = Line;
                                                                string G597 = "G59.7";
                                                                if (t597.Contains(G597))
                                                                {
                                                                    this.WCS.Text = "G59.7";
                                                                }
                                                                else
                                                                {
                                                                    string t598 = Line;
                                                                    string G598 = "G59.8";
                                                                    if (t598.Contains(G598))
                                                                    {
                                                                        this.WCS.Text = "G59.8";
                                                                    }
                                                                    else
                                                                    {
                                                                        string t599 = Line;
                                                                        string G599 = "G59.9";
                                                                        if (t599.Contains(G599))
                                                                        {
                                                                            this.WCS.Text = "G59.9";
                                                                        }
                                                                        else
                                                                        {
                                                                            ModernDialog.ShowMessage("您所选的文件夹可能有错，或不包含坐标，本工具只检测程序前三行是否有G54-G59.9，不支持G59", "警告", MessageBoxButton.OK);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            }
        }
    }
}
