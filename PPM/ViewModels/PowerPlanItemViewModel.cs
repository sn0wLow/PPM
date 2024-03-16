using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PPM
{
    public class PowerPlanItemViewModel : BaseViewModel
    {
        public event EventHandler Selected;
        public PowerPlanItemViewModel()
        {
            this.SelectPowerPlan = new RelayCommand(() =>
            {
                this.IsActive = true;
                Selected?.Invoke(this.GUID, EventArgs.Empty);
            });
        }

        public ICommand SelectPowerPlan { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public Guid GUID { get; set; }
    }
}
