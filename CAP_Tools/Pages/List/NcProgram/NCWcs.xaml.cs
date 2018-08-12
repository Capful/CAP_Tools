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
                this.Th.IsEnabled = true;
                ///读取选择的文件夹中NC文件
                ///清空ListBox
                list.Items.Clear();
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
                    list.Items.Add(FileName);
                }
                if (!File.Exists(FilePath))
                {
                    return;
                }
                var filest = new FileStream(FilePath, FileMode.Open, FileAccess.ReadWrite);
                using (var sr = new StreamReader(filest))
                {
                    string NC = sr.ReadToEnd();//直接读取全部
                    sr.Close(); //关闭流
                    filest.Close();
                    ///判断文件中是否含有G54,如果没有，继续查找，直到查找到G59.9
                    string G54 = "G54";
                    if (NC.Contains(G54))              //判断NC值中是否含有G54字符
                    {
                        this.WCS.Text = "G54";
                    }
                    else
                    {
                        string G55 = "G55";
                        if (NC.Contains(G55))
                        {
                            this.WCS.Text = "G55";
                        }
                        else
                        {
                            string G56 = "G56";
                            if (NC.Contains(G56))
                            {
                                this.WCS.Text = "G56";
                            }
                            else
                            {
                                string G57 = "G57";
                                if (NC.Contains(G57))
                                {
                                    this.WCS.Text = "G57";
                                }
                                else
                                {
                                    string G58 = "G58";
                                    if (NC.Contains(G58))
                                    {
                                        this.WCS.Text = "G58";
                                    }
                                    else
                                    {
                                        string G100 = "G100";
                                        if (NC.Contains(G100))
                                        {
                                            this.WCS.Text = "G100";
                                        }
                                        else
                                        {
                                            string G591 = "G59.1";
                                            if (NC.Contains(G591))
                                            {
                                                this.WCS.Text = "G59.1";
                                            }
                                            else
                                            {
                                                string G592 = "G59.2";
                                                if (NC.Contains(G592))
                                                {
                                                    this.WCS.Text = "G59.2";
                                                }
                                                else
                                                {
                                                    string G593 = "G59.3";
                                                    if (NC.Contains(G593))
                                                    {
                                                        this.WCS.Text = "G59.3";
                                                    }
                                                    else
                                                    {
                                                        string G594 = "G59.4 ";
                                                        if (NC.Contains(G594))
                                                        {
                                                            this.WCS.Text = "G59.4";
                                                        }
                                                        else
                                                        {
                                                            string G595 = "G59.5";
                                                            if (NC.Contains(G595))
                                                            {
                                                                this.WCS.Text = "G59.5";
                                                            }
                                                            else
                                                            {
                                                                string G596 = "G59.6";
                                                                if (NC.Contains(G596))
                                                                {
                                                                    this.WCS.Text = "G59.6";
                                                                }
                                                                else
                                                                {
                                                                    string G597 = "G59.7";
                                                                    if (NC.Contains(G597))
                                                                    {
                                                                        this.WCS.Text = "G59.7";
                                                                    }
                                                                    else
                                                                    {
                                                                        string G598 = "G59.8";
                                                                        if (NC.Contains(G598))
                                                                        {
                                                                            this.WCS.Text = "G59.8";
                                                                        }
                                                                        else
                                                                        {
                                                                            string G599 = "G59.9";
                                                                            if (NC.Contains(G599))
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
                ///不存在
                this.Th.IsEnabled = false;
                ///清空ListBox
                list.Items.Clear();
                ModernDialog.ShowMessage("您选择的文件夹不存在.NC文件程序", "警告", MessageBoxButton.OK);
            }

        }


        private void Th_Click(object sender, RoutedEventArgs e)
        {
            ///如果输入值为G59则报错，暂时不支持G59
            if (AWCS.Text == "G59")
            {
                ModernDialog.ShowMessage("暂不支持G59坐标系，请使用G59.1以后坐标系", "警告", MessageBoxButton.OK);
            }
            else
            {
                ///批量替换文本中的值
                string Path = FileRoute.Text;
                string WcsPath = WCS.Text;
                string AWcsPath = AWCS.Text;
                string[] pathFile = Directory.GetFiles(Path);
                string con = "";
                foreach (string str in pathFile)
                {
                    FileStream fs = new FileStream(str, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs, Encoding.Default);
                    con = sr.ReadToEnd();
                    ///查找文本中的WcsPath，替换为AWcsPath
                    con = con.Replace(WcsPath, AWcsPath);
                    sr.Close();
                    fs.Close();
                    FileStream fs2 = new FileStream(str, FileMode.Open, FileAccess.Write);
                    ///Encoding.Default 参照电脑默认编码保存文件
                    StreamWriter sw = new StreamWriter(fs2, Encoding.Default);
                    sw.WriteLine(con);
                    sw.Close();
                    fs2.Close();
                }
                MessageBoxResult result = ModernDialog.ShowMessage("替换成功，是否打开文件夹？", "提示", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    ///完成后打开文件夹
                    Process.Start("Explorer.exe", Path);
                }

                ///完成后检测文件夹中的值，并返回
                string PathName = null;
                DirectoryInfo d = new DirectoryInfo(Path);
                FileInfo[] Files = d.GetFiles("*.nc");
                List<string> lstr = new List<string>();

                foreach (FileInfo file in Files)
                {
                    PathName = file.FullName;
                    lstr.Add(PathName);
                }
                string filePath = PathName;
                if (!File.Exists(filePath))
                {
                    return;
                }
                var filest = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
                using (var sr = new StreamReader(filest))
                {
                    string NC = sr.ReadToEnd();//直接读取全部
                    sr.Close(); //关闭流
                    filest.Close();
                    ///判断文件中是否含有G54,如果没有，继续查找，直到查找到G59.9
                    string G54 = "G54";
                    if (NC.Contains(G54))              //判断NC值中是否含有G54字符
                    {
                        this.WCS.Text = "G54";
                    }
                    else
                    {
                        string G55 = "G55";
                        if (NC.Contains(G55))
                        {
                            this.WCS.Text = "G55";
                        }
                        else
                        {
                            string G56 = "G56";
                            if (NC.Contains(G56))
                            {
                                this.WCS.Text = "G56";
                            }
                            else
                            {
                                string G57 = "G57";
                                if (NC.Contains(G57))
                                {
                                    this.WCS.Text = "G57";
                                }
                                else
                                {
                                    string G58 = "G58";
                                    if (NC.Contains(G58))
                                    {
                                        this.WCS.Text = "G58";
                                    }
                                    else
                                    {
                                        string G100 = "G100";
                                        if (NC.Contains(G100))
                                        {
                                            this.WCS.Text = "G100";
                                        }
                                        else
                                        {
                                            string G591 = "G59.1";
                                            if (NC.Contains(G591))
                                            {
                                                this.WCS.Text = "G59.1";
                                            }
                                            else
                                            {
                                                string G592 = "G59.2";
                                                if (NC.Contains(G592))
                                                {
                                                    this.WCS.Text = "G59.2";
                                                }
                                                else
                                                {
                                                    string G593 = "G59.3";
                                                    if (NC.Contains(G593))
                                                    {
                                                        this.WCS.Text = "G59.3";
                                                    }
                                                    else
                                                    {
                                                        string G594 = "G59.4 ";
                                                        if (NC.Contains(G594))
                                                        {
                                                            this.WCS.Text = "G59.4";
                                                        }
                                                        else
                                                        {
                                                            string G595 = "G59.5";
                                                            if (NC.Contains(G595))
                                                            {
                                                                this.WCS.Text = "G59.5";
                                                            }
                                                            else
                                                            {
                                                                string G596 = "G59.6";
                                                                if (NC.Contains(G596))
                                                                {
                                                                    this.WCS.Text = "G59.6";
                                                                }
                                                                else
                                                                {
                                                                    string G597 = "G59.7";
                                                                    if (NC.Contains(G597))
                                                                    {
                                                                        this.WCS.Text = "G59.7";
                                                                    }
                                                                    else
                                                                    {
                                                                        string G598 = "G59.8";
                                                                        if (NC.Contains(G598))
                                                                        {
                                                                            this.WCS.Text = "G59.8";
                                                                        }
                                                                        else
                                                                        {
                                                                            string G599 = "G59.9";
                                                                            if (NC.Contains(G599))
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