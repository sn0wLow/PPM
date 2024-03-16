using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PPM
{
    public class TaskBarIconViewModel
    {
        public PopupActivationMode PopupActivationMode { get; set; } = PopupActivationMode.RightClick;
        public BitmapImage IconSource { get; } = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Icons/Power_Options_Tray.ico"));
    }
}
