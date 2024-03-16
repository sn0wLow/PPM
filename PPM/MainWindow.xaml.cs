using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PPM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.Hide();
            InitializeComponent();
            //TaskbarIcon taskbarIcon = new TaskbarIcon();
            //taskbarIcon.PopupActivation = PopupActivationMode.RightClick | PopupActivationMode.LeftClick;
            //Uri iconUri = new Uri("pack://application:,,,/Resources/Images/Icons/Power_Options.ico");
            //new BitmapImage(iconUri);
            //taskbarIcon.IconSource = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Icons/Power_Options.ico"));


            //ContextMenu contextMenu = new ContextMenu();

            //List<MenuItem> menuItems = new List<MenuItem>();

            //foreach (var powerPlan in powerPlanManager.PowerPlans)
            //{
            //    MenuItem menuItem = new MenuItem()
            //    {
            //        Header = powerPlan.Name,
            //        IsChecked = powerPlanManager.GetActivePowerPlan().Equals(powerPlan),
            //        Tag = powerPlan.GUID,
            //    };

            //    menuItem.Click += (o, e) =>
            //    {
            //        powerPlanManager.SetActive(powerPlanManager.PowerPlans.Where(x => x.GUID.Equals(new Guid((menuItem.Tag.ToString())))).First());
            //        menuItems.ForEach(x => x.IsChecked = powerPlanManager.GetActivePowerPlan().GUID.Equals(new Guid((x.Tag.ToString()))));
            //    };

            //    Button button = new Button()
            //    {
            //        Content = powerPlan.Name,
            //        Tag = powerPlan.GUID,
            //    };

            //    button.Click += (o, e) =>
            //    {
            //        powerPlanManager.SetActive(powerPlanManager.PowerPlans.Where(x => x.GUID.Equals(new Guid((button.Tag.ToString())))).First());
            //        System.Windows.MessageBox.Show(powerPlanManager.GetActivePowerPlan().Name);
            //    };

            //    menuItems.Add(menuItem);

            //    this.MainStackPanel.Children.Add(button);
            //    contextMenu.Items.Add(menuItem);
            //}



            //taskbarIcon.ContextMenu = contextMenu;
            //MainGrid.Children.Add(taskbarIcon);
            //this.Hide();

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
