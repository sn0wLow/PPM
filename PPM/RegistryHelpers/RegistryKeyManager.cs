using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPM
{
    public static class RegistryKeyManager
    {
        public static RegistryKeyValueState CheckKeyValue(RegistryKey registryKey, string keyName, string keyValue)
        {
            var regValue = registryKey.GetValue(keyName);

            if (regValue == null)
            {
                return RegistryKeyValueState.NonExistant;
            }

            if (!regValue.Equals(keyValue))
            {
                return RegistryKeyValueState.IsInvalid;
            }

            return RegistryKeyValueState.IsValid;
        }

        public static void SetKeyValue(RegistryKey registryKey, string keyName, string keyValue)
        {
            registryKey.SetValue(keyName, keyValue);

        }

        public static void RemoveKey(RegistryKey registryKey, string keyName)
        {
            registryKey.DeleteValue(keyName);
        }
    }
}
