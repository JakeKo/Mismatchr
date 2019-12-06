using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Mismatchr
{
    public static class ParseXML
    {
        public static AppPermissions getOneAppsPermissions(string filepath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);
            List<string> declaredPerms = new List<string>();
            XmlNode topnode = doc.DocumentElement.SelectSingleNode("usesPermissions");
            foreach (XmlNode node1 in topnode.ChildNodes)
            {
                declaredPerms.Add(node1.InnerText);
            }

            List<string> usedPerms = new List<string>();
            XmlNode node = doc.DocumentElement.SelectSingleNode("actuallyUsesPermissions");
            foreach (XmlNode node1 in node.ChildNodes)
            {
                usedPerms.Add(node1.InnerText);
            }
            var app = new AppPermissions(filepath.Substring(15), declaredPerms, usedPerms);
            return app;
        }

        public static List<AppPermissions> getAllAppsPermissions()
        {
            string[] filePaths = Directory.GetFiles("just_xml_files/", "*.xml",
                SearchOption.TopDirectoryOnly);
            List<AppPermissions> permissions = new List<AppPermissions>();
            foreach (var file in filePaths)
            {
                permissions.Add(getOneAppsPermissions(file));
            }

            return permissions;
        }

        public static List<Permission> getDefaultPermissions()
        {
            List<Permission> defaults = new List<Permission>();
            foreach (var line in File.ReadAllLines("permissions_list/DefaultPermissions.txt"))
            {
                defaults.Add(new Permission(line));
            }
            return defaults;
        }
    }
}
