using System.Collections.Generic;

namespace Mismatchr
{
    public class Permission
    {
        public string Name { get; set; }
        public string[] ProtectionLevel { get; set; }

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
                ProtectionLevel = split[1].Split(',');
            }
        }

        public Permission(string name, string level)
        {
            Name = name;
            ProtectionLevel = level.Split(',');
        }

        public double GetRisk()
        {
            double risk = 0;
            foreach( string level in ProtectionLevel)
            {
                switch (level)
                {
                    case "dangerous":
                        risk += 1;
                        break;
                    case "signature":
                        risk += 0.5;
                        break;
                    case "normal":
                        risk += 0.25;
                        break;
                    case "priviliged":
                        risk -= 0.25;
                        break;
                    default:
                        break;
                }
            }
            return risk;
        }
    }
}
