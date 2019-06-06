using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
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
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            ModernDialog.ShowMessage(MainWindow.Cap.IniFileName, "警告", MessageBoxButton.OK);




        }

        private void PZ_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ///更换ComboBox选项后获取选中值名称
            string a = PZ.SelectedItem.ToString();
            if(a.Contains(":"))
            {
                ///截取“：”后的字符
                int index = a.LastIndexOf(':'); //截取最后出现']'后面的所有字符
                a = a.Substring(index + 1);
                a = a.Trim(); //去除首尾空格
                Cap.IniFileName = a;
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
                

                

            }
            else
            {
                //ModernDialog.ShowMessage("无值", "警告", MessageBoxButton.OK);
            }
            

        }
    }
}
