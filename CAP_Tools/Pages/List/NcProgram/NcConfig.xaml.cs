using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using static CAP_Tools.MainWindow;

namespace CAP_Tools.Pages.List
{
    /// <summary>
    /// Interaction logic for NcTabPage.xaml
    /// </summary>
    public partial class NcConfig : UserControl
    {
        public NcConfig()
        {
            InitializeComponent();

            string Path = AppDomain.CurrentDomain.BaseDirectory + "NC Config\\";  //设置NC Config路径
            if (Directory.Exists(Path))
            {
                ///判断文件夹内是否有ini文件
                if (Directory.GetFiles(Path, "*.ini").Length > 0)
                {
                    DirectoryInfo d = new DirectoryInfo(Path);
                    FileInfo[] Files = d.GetFiles("*.ini");
                    List<string> lstr = new List<string>();
                    ///获取文件夹下文件名，将路径显示到ComboBox
                    foreach (FileInfo file in Files)
                    {
                        string Name = System.IO.Path.GetFileNameWithoutExtension(file.Name);
                        Config.Items.Add(Name);
                    }
                    Config.SelectedItem = Cap.IniFileName; //默认选中
                }
                else
                {
                    ModernDialog.ShowMessage("默认配置文件不存在，请新建配置文件，或重新安装软件", "警告", MessageBoxButton.OK);
                }
            }
            else
            {
                Directory.CreateDirectory(Path);
                ModernDialog.ShowMessage("默认配置文件不存在，请新建配置文件，或重新安装软件", "警告", MessageBoxButton.OK);
            }
            
        }

        private void NcConfig_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ///更换ComboBox选项后获取选中值名称
            Cap.IniFileName = Config.SelectedItem.ToString();
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
            }
            else
            {
                ModernDialog.ShowMessage(Cap.IniFileName + " 配置文件不存在，请检查", "警告", MessageBoxButton.OK);
            }


        }

        private void Save_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string inifilePath = AppDomain.CurrentDomain.BaseDirectory + "NC Config\\" + Cap.IniFileName + ".ini";  //设置路径
            IniFile iniFile = new IniFile(inifilePath);
            ///读取ini文件数据

            iniFile.WriteIni("WCS_Config", "WCS_Line", WCS_Line.Text);
            iniFile.WriteIni("WCS_Config", "WCS_Start", WCS_Start.Text);
            iniFile.WriteIni("WCS_Config", "WCS_End", WCS_End.Text);

            iniFile.WriteIni("T_Config", "T_Line", T_Line.Text);
            iniFile.WriteIni("T_Config", "T_Start", T_Start.Text);
            iniFile.WriteIni("T_Config", "T_End", T_End.Text);

            ModernDialog.ShowMessage(Cap.IniFileName + " 配置文件保存成功", "提示", MessageBoxButton.OK);
        }

        

        private void NewConfig_Click(object sender, RoutedEventArgs e)
        {

            if (Config.Items.Cast<object>().All(x => x.ToString() != NewConfigName.Text))
            {
                string inifilePath = AppDomain.CurrentDomain.BaseDirectory + "NC Config\\" + NewConfigName.Text + ".ini";  //设置路径
                IniFile iniFile = new IniFile(inifilePath);
                ///新建空的ini配置文件

                iniFile.WriteIni("WCS_Config", "WCS_Line", "");
                iniFile.WriteIni("WCS_Config", "WCS_Start", "");
                iniFile.WriteIni("WCS_Config", "WCS_End", "");

                iniFile.WriteIni("T_Config", "T_Line", "");
                iniFile.WriteIni("T_Config", "T_Start", "");
                iniFile.WriteIni("T_Config", "T_End", "");
                Config.Items.Add(NewConfigName.Text);
                Config.SelectedItem = NewConfigName.Text;
                ModernDialog.ShowMessage(NewConfigName.Text + " 配置文件新建成功", "警告", MessageBoxButton.OK);
            }
            else
            {
                ModernDialog.ShowMessage(NewConfigName.Text + " 配置文件已存在", "警告", MessageBoxButton.OK);
            }
        }
    }
}
