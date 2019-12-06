using System.Collections.Generic;
using System.Linq;

namespace Mismatchr
{
    public class Permission
    {
        public string Name { get; set; }
        public string ProtectionLevel { get; set; }
        public string[] Flags { get; set; }

        public static double getAllRisk(List<Permission> permissions)
        {
            double totalRisk = 0;
            foreach(Permission perm in permissions)
            {
                totalRisk += perm.GetRisk();
            }
            return totalRisk;
        }

        public Permission(string line)
        {
            string[] split = line.Split(':');
            Name = split[0];
            if (split.Length > 1)
            {
                ProtectionLevel = split[1].Split(',')[0];
                Flags = split[1].Split(',').Skip(1).ToArray();
            }
        }

        public Permission(string line, List<Permission> defaults)
        {
            string[] split = line.Split(':');
            Name = split[0];
            if (split.Length > 1)
            {
                ProtectionLevel = split[1].Split(',')[0];
                Flags = split[1].Split(',');
            }
            else
            {
                Permission defPerm = defaults.Find(x => x.Name == Name);
                ProtectionLevel = defPerm.ProtectionLevel;
                Flags = defPerm.Flags;
            }
        }

        public double GetRisk()
        {
            switch (ProtectionLevel)
            {
                case "dangerous;":
                    return 2 - (Flags.Length * 0.25);
                case "signature;":
                    return 1 - (Flags.Length * 0.25);
                case "normal;":
                    return 0.5 - (Flags.Length * 0.25);
                default:
                    return 0;
            }
        }
    }
}
