using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PPM
{
    public class PowerPlanPopUpViewModel : BaseViewModel
    {
        public PowerPlanPopUpViewModel()
        {
            OpenSettingsWindowCommand = new RelayCommand(() =>
            {
                Application.Current.MainWindow.Show();
            });

            CloseApplicationCommand = new RelayCommand(() =>
            {
                System.Windows.Application.Current.Shutdown();
            });

            this.PowerPlanItems = new ObservableCollection<PowerPlanItemViewModel>();


            PowerPlanManager powerPlanManager = new PowerPlanManager();
            powerPlanManager.LoadPlans();

            foreach (var powerPlan in powerPlanManager.PowerPlans)
            {
                PowerPlanItemViewModel powerPlanItemViewModel = new PowerPlanItemViewModel()
                {
                    GUID = powerPlan.GUID,
                    Name = powerPlan.Name,
                    IsActive = powerPlan.IsActive
                };

                powerPlanItemViewModel.Selected += (guid, e) =>
                {
                    Guid activeGuid = (Guid)guid;
                    this.PowerPlanItems.Where(x => !x.GUID.Equals(guid)).ToList().ForEach(powerPlan => powerPlan.IsActive = false);
                    PowerPlanManager.PowerSetActiveScheme(IntPtr.Zero, ref activeGuid);
                };

                this.PowerPlanItems.Add(powerPlanItemViewModel);
            }
        }

        public ICommand OpenSettingsWindowCommand { get; set; }
        public ICommand CloseApplicationCommand { get; set; }
        public ObservableCollection<PowerPlanItemViewModel> PowerPlanItems { get; set; }
    }
}
