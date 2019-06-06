using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;

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
            IniFile iniFile = new IniFile(".///NC Config//星创后处理.ini");
            //iniFile.writeIni("section1", "key1", "value11");


            WCS_Line.Text = iniFile.ReadIni("WCS_Config", "WCS_Line");
            WCS_Start.Text = iniFile.ReadIni("WCS_Config", "WCS_Start");
            WCS_End.Text = iniFile.ReadIni("WCS_Config", "WCS_End");

            T_Line.Text = iniFile.ReadIni("T_Config", "T_Line");
            T_Start.Text = iniFile.ReadIni("T_Config", "T_Start");
            T_End.Text = iniFile.ReadIni("T_Config", "T_End");



           
        }

        private void PZ_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string a = PZ.Text;
            ModernDialog.ShowMessage(a, "警告", MessageBoxButton.OK);
        }
    }
}
