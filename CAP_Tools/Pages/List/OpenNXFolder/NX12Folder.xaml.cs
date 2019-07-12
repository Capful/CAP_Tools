using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace CAP_Tools.Pages.List.OpenFolder
{
    /// <summary>
    /// Interaction logic for NX10Folder.xaml
    /// </summary>
    public partial class NX12Folder : UserControl
    {
        public NX12Folder()
        {
            InitializeComponent();
            ///判断NX是否安装，如果有安装再判断详细版本
            ///判断注册表项是否存在
            RegistryKey NX = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Unigraphics Solutions\Installed Applications");
            if (NX == null)
            {
                ///未安装
                ///定义按钮为灰色不可选
                Home.IsEnabled = false;
                UGII.IsEnabled = false;
                ModelTemplates.IsEnabled = false;
                Template_Part.IsEnabled = false;
                Postprocessor.IsEnabled = false;
                ///定义提示文字
                Tip.Text = "抱歉，您未安装NX";
            }
            else
            {
                ///判断NX是否安装
                if (CheckNX12() == true)
                {
                    ///已安装
                    ///指定路径
                    string NXPath = Path.GetDirectoryName(Path.GetDirectoryName(GetNXEXE()));
                    string UGII = NXPath + @"\UGII";
                    string ModelTemplates = NXPath + @"\LOCALIZATION\prc\simpl_chinese\startup";
                    string Template_Part = NXPath + @"\MACH\resource\template_part\metric";
                    string Template_CAM = NXPath + @"\MACH\resource\template_set";
                    string Postprocessor = NXPath + @"\MACH\resource\postprocessor";
                    ///指定鼠标悬停提示
                    this.Home.ToolTip = NXPath.ToString();
                    this.UGII.ToolTip = UGII.ToString();
                    this.ModelTemplates.ToolTip = ModelTemplates.ToString();
                    this.Template_Part.ToolTip = Template_Part.ToString();
                    this.Template_CAM.ToolTip = Template_CAM.ToString();
                    this.Postprocessor.ToolTip = Postprocessor.ToString();
                }
                else
                {
                    ///未安装
                    ///定义按钮为灰色不可选
                    this.Home.IsEnabled = false;
                    this.UGII.IsEnabled = false;
                    this.ModelTemplates.IsEnabled = false;
                    this.Template_Part.IsEnabled = false;
                    this.Template_CAM.IsEnabled = false;
                    this.Postprocessor.IsEnabled = false;
                    ///定义提示文字
                    this.Tip.Text = "抱歉，您未安装NX12";
                }
            }
        }

        private string GetNXEXE()
        {
            ///获取NX安装路径
            RegistryKey driverKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Unigraphics Solutions\Installed Applications");
            ///指定对应版本
            string EXE = (string)driverKey.GetValue("Unigraphics V30.0");
            return EXE;
        }

        private bool CheckNX12()
        {
            string NXPath = Path.GetDirectoryName(Path.GetDirectoryName(GetNXEXE()));
            if (NXPath != null)
            {
                if (File.Exists(GetNXEXE()))
                {
                    return true;
                }
            }
            return false;
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            ///获取NX安装路径
            string NXPath = Path.GetDirectoryName(Path.GetDirectoryName(GetNXEXE()));
            ///回退2级目录(System.IO.Path.GetDirectoryName(System.IO.Path.GetDirectoryName(@"C:\ABC\Temp\DC\")))得到"C:\ABC\Temp"
            ///打开主目录
            System.Diagnostics.Process.Start(NXPath);
        }

        private void UGII_Click(object sender, RoutedEventArgs e)
        {
            string NXPath = Path.GetDirectoryName(Path.GetDirectoryName(GetNXEXE()));
            string UGII = NXPath + @"\UGII";
            ///打开UGII目录
            System.Diagnostics.Process.Start(UGII);
        }

        private void ModelTemplates_Click(object sender, RoutedEventArgs e)
        {
            string NXPath = Path.GetDirectoryName(Path.GetDirectoryName(GetNXEXE()));
            string ModelTemplates = NXPath + @"\LOCALIZATION\prc\simpl_chinese\startup";

            ///打开默认模板目录
            System.Diagnostics.Process.Start(ModelTemplates);
        }

        private void Template_Part_Click(object sender, RoutedEventArgs e)
        {
            string NXPath = Path.GetDirectoryName(Path.GetDirectoryName(GetNXEXE()));
            string Template_Part = NXPath + @"\MACH\resource\template_part\metric";

            ///打开加工模板目录
            System.Diagnostics.Process.Start(Template_Part);
        }

        private void Template_CAM_Click(object sender, RoutedEventArgs e)
        {
            string NXPath = Path.GetDirectoryName(Path.GetDirectoryName(GetNXEXE()));
            string Template_CAM = NXPath + @"\MACH\resource\template_set";

            ///打开加工模板目录
            System.Diagnostics.Process.Start(Template_CAM);
        }


        private void Postprocessor_Click(object sender, RoutedEventArgs e)
        {
            string NXPath = Path.GetDirectoryName(Path.GetDirectoryName(GetNXEXE()));
            string Postprocessor = NXPath + @"\MACH\resource\postprocessor";

            ///打开机床后处理目录
            System.Diagnostics.Process.Start(Postprocessor);
        }
    }
}
