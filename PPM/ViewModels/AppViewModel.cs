using Microsoft.Win32;
using System;
using System.Windows.Input;

namespace PPM
{
    public class AppViewModel : BaseViewModel
    {
        string exePath;
        public AppViewModel()
        {
            try
            {
                this.StartWithWindows = false;

                var regKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                exePath = $"\"{System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName}\"";
                var regKeyValueState = RegistryKeyManager.CheckKeyValue(regKey, "PPM", exePath);

                if (regKeyValueState == RegistryKeyValueState.IsValid)
                {
                    StartWithWindows = true;
                }
                if (regKeyValueState == RegistryKeyValueState.IsInvalid)
                {
                    RegistryKeyManager.SetKeyValue(regKey, "PPM", this.exePath);
                    StartWithWindows = true;
                }
            }
            catch (Exception)
            {
                this.StartWithWindows = false;
            }

            this.ToggleAddAutoStart =
                new RelayParameterizedCommand((parameter) =>
                {
                    try
                    {
                        if ((bool)parameter)
                        {
                            var regKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                            RegistryKeyManager.SetKeyValue(regKey, "PPM", this.exePath);

                            this.StartWithWindows = true;
                        }
                        else
                        {
                            var regKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                            RegistryKeyManager.RemoveKey(regKey, "PPM");

                            this.StartWithWindows = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Could not add App to Autostart");
                        this.StartWithWindows = false;
                    }
                });


        }


        public ICommand ToggleAddAutoStart { get; set; }
        public bool StartWithWindows { get; set; }
    }
}
