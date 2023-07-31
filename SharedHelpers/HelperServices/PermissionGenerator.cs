using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedHelpers.HelperServices
{
    public static class PermissionGenerator
    {
        public static List<string> GeneratePermissions()
        {
            List<string> permissions = new List<string>();
            foreach (var module in Enum.GetValues(typeof(Enums.Modules)))
            {
                foreach (var permission in Enum.GetValues(typeof(Enums.Permissions)))
                {
                    permissions.Add($"{module.ToString()}.{permission.ToString()}");
                }
            }
            return permissions;
        }
        // Function to return modules from Enum Enums.Modules
        public static List<string> GenerateModules()
        {
            List<string> modules = new List<string>();
            foreach (var module in Enum.GetValues(typeof(Enums.Modules)))
            {
                modules.Add(module.ToString());
            }
            return modules;
        }
    }
}
