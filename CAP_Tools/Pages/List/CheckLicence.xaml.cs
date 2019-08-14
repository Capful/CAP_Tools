using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Configuration.Install;
using System.Collections;

namespace CAP_Tools.Pages.List
{
    /// <summary>
    /// Interaction logic for CheckLicence.xaml
    /// </summary>
    public partial class CheckLicence : UserControl
    {
        private const string NOlicence = "未检测到许可证服务";

        public CheckLicence()
        {
            InitializeComponent();

            if (IsServiceIsExisted("Siemens PLM License Server") == true)
            {
                ///存在
                Licence_Name.Text = "Siemens PLM License Server";
                Licence_Status.Text = GetSystemServiceStatusString(Licence_Name.Text);
                Licence_Path.Text = GetWindowsServiceInstallPath(Licence_Name.Text);

                ///按钮可用
                Open.IsEnabled = true;
                Stop.IsEnabled = true;
                ReStart.IsEnabled = true;
                UnService.IsEnabled = true;
            }
            else
            {
                ///不存在
                Licence_Name.Text = NOlicence;
                Licence_Status.Text = NOlicence;
                Licence_Path.Text = NOlicence;

                ///按钮不可用
                Open.IsEnabled = false;
                Stop.IsEnabled = false;
                ReStart.IsEnabled = false;
                UnService.IsEnabled = false; 

            }

            ///定时检查服务
            CheckLicence1();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            StartService(Licence_Name.Text);
            Licence_Status.Text = GetSystemServiceStatusString(Licence_Name.Text);
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            StopService(Licence_Name.Text);
            Licence_Status.Text = GetSystemServiceStatusString(Licence_Name.Text);
        }

        private void ReStart_Click(object sender, RoutedEventArgs e)
        {
            StopService(Licence_Name.Text);
            Thread.Sleep(100);
            StartService(Licence_Name.Text);
        }

        private void UnService_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Licence_Path_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(Licence_Path.Text))
            {
                ///如果存在
                Process.Start(Licence_Path.Text);
            }
        }

        #region 停止服务
        /// <summary>
        /// 停止系统服务
        /// </summary>
        /// <param name="serviceName">系统服务名称</param>
        /// <returns></returns>
        public static bool StopService(string serviceName)
        {
            try
            {
                using (var control = new ServiceController(serviceName))
                {

                    if (control.Status == ServiceControllerStatus.Running)
                    {
                        control.Stop();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 启动服务
        /// <summary>
        /// 启动系统服务
        /// </summary>
        /// <param name="serviceName">系统服务名称</param>
        /// <returns></returns>
        public static bool StartService(string serviceName)
        {
            try
            {
                using (var control = new ServiceController(serviceName))
                {
                    if (control.Status != ServiceControllerStatus.Running)
                    {
                        control.Start();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 返回服务状态 
        /// <summary>
        /// 返回服务状态
        /// </summary>
        /// <param name="serviceName">系统服务名称</param>
        /// <returns>1:服务未运行 2:服务正在启动 3:服务正在停止 4:服务正在运行 5:服务即将继续 6:服务即将暂停 7:服务已暂停 0:未知状态</returns>
        public static string GetSystemServiceStatusString(string serviceName)
        {
            try
            {
                using (var control = new ServiceController(serviceName))
                {
                    var status = string.Empty;
                    switch ((int)control.Status)
                    {
                        case 1:
                            status = "服务未运行";
                            break;
                        case 2:
                            status = "服务正在启动...";
                            break;
                        case 3:
                            status = "服务正在停止...";
                            break;
                        case 4:
                            status = "服务正在运行";
                            break;
                        case 5:
                            status = "服务即将继续";
                            break;
                        case 6:
                            status = "服务即将暂停";
                            break;
                        case 7:
                            status = "服务已暂停";
                            break;
                        case 0:
                            status = NOlicence;
                            break;
                    }
                    return status;
                }
            }
            catch
            {
                return NOlicence;
            }
        }
        #endregion

        #region 定时检查许可证 
        /// <summary>
        /// 定时检查许可证
        /// </summary>
        private void CheckLicence1()
        {
            ///每隔2秒判断服务状态
            System.Timers.Timer Check = new System.Timers.Timer(1000);//每隔1秒执行一次，没用winfrom自带的
            Check.Elapsed += Check_Licence;//委托，要执行的方法
            Check.AutoReset = true;//获取该定时器自动执行
            Check.Enabled = true;//这个一定要写，要不然定时器不会执行的
        }

        /// <summary>
        /// 定时检查许可证
        /// </summary>
        private void Check_Licence(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (IsServiceIsExisted("Siemens PLM License Server") == true)
            {
                ///存在
                Licence_Name.Dispatcher.Invoke(
               new Action(
                    delegate
                    {
                        Licence_Name.Text = "Siemens PLM License Server";
                    }
               )
             );
                Licence_Status.Dispatcher.Invoke(
               new Action(
                    delegate
                    {
                        Licence_Status.Text = GetSystemServiceStatusString(Licence_Name.Text);
                    }
               )
             );
                Licence_Path.Dispatcher.Invoke(
                   new Action(
                        delegate
                        {
                            Licence_Path.Text = GetWindowsServiceInstallPath(Licence_Name.Text);
                        }
                   )
             );
                ///打开按钮
                Open.Dispatcher.Invoke(
                       new Action(
                            delegate
                            {
                                Open.IsEnabled = true;
                            }
                       )
                 );
                //停止按钮
                Stop.Dispatcher.Invoke(
                       new Action(
                            delegate
                            {
                                Stop.IsEnabled = true;
                            }
                       )
                 );
                //重启按钮
                ReStart.Dispatcher.Invoke(
                       new Action(
                            delegate
                            {
                                ReStart.IsEnabled = true;
                            }
                       )
                 );
                //卸载按钮
                UnService.Dispatcher.Invoke(
                       new Action(
                            delegate
                            {
                                UnService.IsEnabled = true;
                            }
                       )
                 );

            }
            else
            {
                ///不存在
                Licence_Name.Dispatcher.Invoke(
               new Action(
                    delegate
                    {
                        Licence_Name.Text = NOlicence;
                    }
               )
             );
                Licence_Status.Dispatcher.Invoke(
               new Action(
                    delegate
                    {
                        Licence_Status.Text = NOlicence;
                    }
               )
             );
                Licence_Path.Dispatcher.Invoke(
                   new Action(
                        delegate
                        {
                            Licence_Path.Text = NOlicence;
                        }
                   )
             );
                ///打开按钮
                Open.Dispatcher.Invoke(
                       new Action(
                            delegate
                            {
                                Open.IsEnabled = false;
                            }
                       )
                 );
                //停止按钮
                Stop.Dispatcher.Invoke(
                       new Action(
                            delegate
                            {
                                Stop.IsEnabled = false;
                            }
                       )
                 );
                //重启按钮
                ReStart.Dispatcher.Invoke(
                       new Action(
                            delegate
                            {
                                ReStart.IsEnabled = false;
                            }
                       )
                 );
                //卸载按钮
                UnService.Dispatcher.Invoke(
                       new Action(
                            delegate
                            {
                                UnService.IsEnabled = false;
                            }
                       )
                 );
            }
        }
        #endregion

        #region 检查服务存在的存在性 
        /// <summary>
        /// 检查服务存在的存在性
        /// </summary>
        /// <param name=" NameService ">服务名</param>
        /// <returns>存在返回 true,否则返回 false;</returns>
        public static bool IsServiceIsExisted(string NameService)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController s in services)
            {
                if (s.ServiceName.ToLower() == NameService.ToLower())
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 获取服务安装路径 
        /// <summary>
        /// 获取服务安装路径
        /// </summary>
        /// <param name="ServiceName">服务名称</param>
        /// <returns></returns>
        private string GetWindowsServiceInstallPath(string ServiceName)
        {
            string key = @"SYSTEM\CurrentControlSet\Services\" + ServiceName;
            string path = "";

            try
            {
                path = Registry.LocalMachine.OpenSubKey(key).GetValue("ImagePath").ToString();
                path = path.Replace("\"", string.Empty);            //替换掉双引号
                FileInfo fi = new FileInfo(path);
                return fi.Directory.ToString();
            }
            catch (Exception)
            {
                path = NOlicence;
            }
            return path;
        }
        #endregion

    }
}
