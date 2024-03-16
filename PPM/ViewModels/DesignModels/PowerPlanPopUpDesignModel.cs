using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PPM
{
    public class PowerPlanPopUpDesignModel : BaseViewModel
    {
        public PowerPlanPopUpDesignModel()
        {
            PowerPlanItems = new ObservableCollection<PowerPlanItemViewModel>
            {
                new PowerPlanItemViewModel() { IsActive = false, Name = "Power Plan Design Name 1" },
                new PowerPlanItemViewModel() { IsActive = true, Name = "Power Plan Design Name 2" },
                new PowerPlanItemViewModel() { IsActive = false, Name = "Short Name 3" },
                new PowerPlanItemViewModel() { IsActive = false, Name = "Power Plan Design Name 4" },
                new PowerPlanItemViewModel() { IsActive = false, Name = "Power Plan Design Name but longer 5" }
            };
        }
        public ObservableCollection<PowerPlanItemViewModel> PowerPlanItems { get; set; }
    }
}
