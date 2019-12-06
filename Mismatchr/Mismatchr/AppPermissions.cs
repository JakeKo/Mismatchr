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
        public double Score;

        public AppPermissions(string name, List<string> declaredPerm, List<string> usedPerm)
        {
            Name = name;
            declaredPermissions = new List<Permission>();
            usedPermissions = new List<Permission>();
            List<Permission> allPermissions = ParseXML.getDefaultPermissions();
            foreach (string permission in declaredPerm)
            {
                if (allPermissions.Where(x => x.Name == permission).Any())
                {
                    declaredPermissions.Add(allPermissions.Where(x => x.Name == permission).First());
                }
                
            }
            foreach (string permission in usedPerm)
            {
                if (allPermissions.Where(x => x.Name == permission).Any())
                {
                    usedPermissions.Add(allPermissions.Where(x => x.Name == permission).First());
                }
            }

            this.Score = getRisk();
        }

        public double getRisk()
        {
            return Permission.getAllRisk(usedPermissions)
                + 2 * Permission.getAllRisk(declaredPermissions.Where(x => !usedPermissions.Contains(x)).ToList());
        }
    }
}
