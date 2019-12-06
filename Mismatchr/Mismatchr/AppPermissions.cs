using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mismatchr
{
    public class AppPermissions
    {
        public string Name { get; set; }
        public List<Permission> declaredPermissions;
        public List<Permission> usedPermissions;

        public AppPermissions(string name, List<string> declaredPerm, List<string> usedPerm)
        {
            Name = name;
            declaredPermissions = new List<Permission>();
            usedPermissions = new List<Permission>();
            foreach (string permission in declaredPerm)
            {
                declaredPermissions.Add(new Permission(permission));
            }
            foreach (string permission in usedPerm)
            {
                usedPermissions.Add(new Permission(permission));
            }
        }

        public double getRisk()
        {
            return Permission.getAllRisk(usedPermissions)
                + 2 * Permission.getAllRisk(declaredPermissions.Where(x => usedPermissions.Contains(x)).ToList());
        }
    }
}
