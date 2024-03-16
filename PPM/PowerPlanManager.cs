using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PPM
{
    class PowerPlanManager
    {
        #region DLL imports

        public enum AccessFlags : uint
        {
            ACCESS_SCHEME = 16,
            ACCESS_SUBGROUP = 17,
            ACCESS_INDIVIDUAL_SETTING = 18
        }

        [DllImport("PowrProf.dll")]
        public static extern UInt32 PowerEnumerate(IntPtr RootPowerKey, IntPtr SchemeGuid, IntPtr SubGroupOfPowerSettingGuid, UInt32 AcessFlags, UInt32 Index, ref Guid Buffer, ref UInt32 BufferSize);

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern int GetSystemDefaultLCID();

        [DllImportAttribute("powrprof.dll", EntryPoint = "PowerSetActiveScheme")]
        public static extern uint PowerSetActiveScheme(IntPtr UserPowerKey, ref Guid ActivePolicyGuid);

        [DllImportAttribute("powrprof.dll", EntryPoint = "PowerGetActiveScheme")]
        public static extern uint PowerGetActiveScheme(IntPtr UserPowerKey, out IntPtr ActivePolicyGuid);

        [DllImportAttribute("powrprof.dll", EntryPoint = "PowerReadFriendlyName")]
        public static extern uint PowerReadFriendlyName(IntPtr RootPowerKey, ref Guid SchemeGuid, IntPtr SubGroupOfPowerSettingsGuid, IntPtr PowerSettingGuid, IntPtr Buffer, ref uint BufferSize);

        #endregion

        private List<PowerPlan> powerPlans = new List<PowerPlan>();

        public PowerPlanManager()
        {

        }

        public void LoadPlans()
        {
            Guid activeGuid = GetActiveGUID();
            GetAllPowerPlansGuids().ToList()
                .ForEach(powerPlanGuid => 
                { 
                    powerPlans.Add(new PowerPlan(powerPlanGuid, GetPowerPlanName(powerPlanGuid), activeGuid.Equals(powerPlanGuid))); 
                });
        }

        public void SetActive(PowerPlan plan)
        {
            Guid guid = plan.GUID;
            PowerSetActiveScheme(IntPtr.Zero, ref guid);
            this.powerPlans.Find(x => x.guid.Equals(guid)).IsActive = true;
        }

        private static IEnumerable<Guid> GetAllPowerPlansGuids()
        {
            var schemeGuid = Guid.Empty;

            uint sizeSchemeGuid = (uint)Marshal.SizeOf(typeof(Guid));
            uint schemeIndex = 0;

            while (PowerEnumerate(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, (uint)AccessFlags.ACCESS_SCHEME, schemeIndex, ref schemeGuid, ref sizeSchemeGuid) == 0)
            {
                yield return schemeGuid;
                schemeIndex++;
            }
        }

        private static string GetPowerPlanName(Guid guid)
        {
            string name = string.Empty;
            IntPtr lpszName = (IntPtr)null;
            uint dwSize = 0;

            PowerReadFriendlyName((IntPtr)null, ref guid, (IntPtr)null, (IntPtr)null, lpszName, ref dwSize);

            if (dwSize > 0)
            {
                lpszName = Marshal.AllocHGlobal((int)dwSize);
                if (0 == PowerReadFriendlyName((IntPtr)null, ref guid, (IntPtr)null, (IntPtr)null, lpszName, ref dwSize))
                {
                    name = Marshal.PtrToStringUni(lpszName);
                }
                if (lpszName != IntPtr.Zero)
                    Marshal.FreeHGlobal(lpszName);
            }

            return name;
        }

        #region GetPowerPlanName alt
        /*public static string GetPowerPlanName(Guid guid)
        {
            uint sizeName = 1024;
            IntPtr pSizeName = Marshal.AllocHGlobal((int)sizeName);
        
            string friendlyName;
        
            try
            {
                PowerReadFriendlyName(IntPtr.Zero, ref guid, IntPtr.Zero, IntPtr.Zero, pSizeName, ref sizeName);
                friendlyName = Marshal.PtrToStringUni(pSizeName);
            }
            finally
            {
                Marshal.FreeHGlobal(pSizeName);
            }
        
            return friendlyName;
        }*/
        #endregion


        public static Guid GetActiveGUID()
        {
            Guid ActiveScheme = Guid.Empty;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(IntPtr)));
            if (PowerGetActiveScheme((IntPtr)null, out ptr) == 0)
            {
                ActiveScheme = (Guid)Marshal.PtrToStructure(ptr, typeof(Guid));
                if (ptr != null)
                {
                    Marshal.FreeHGlobal(ptr);
                }
            }
            return ActiveScheme;
        }

        /// <summary>
        /// Returns the active Power Plan
        /// </summary>
        /// <returns></returns>
        public PowerPlan GetActivePowerPlan()
        {
            Guid guid = GetActiveGUID();
            return this.powerPlans.Find(p => (p.GUID == guid));
        }


        /// <summary>
        /// Opens Power Options section of the Control Panel.
        /// </summary>
        public void OpenControlPanel()
        {
            var root = Environment.GetEnvironmentVariable("SystemRoot");
            Process.Start(root + "\\system32\\control.exe", "/name Microsoft.PowerOptions");
        }

        /// <summary>
        /// Return all the Loaded Power Planss
        /// </summary>
        public List<PowerPlan> PowerPlans => this.powerPlans;


    }
}
