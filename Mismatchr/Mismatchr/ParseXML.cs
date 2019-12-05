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
            List<string> fakeperms = new List<string>();
            XmlNode topnode = doc.DocumentElement.SelectSingleNode("usesPermissions");
            foreach (XmlNode node1 in topnode.ChildNodes)
            {
                fakeperms.Add(node1.InnerText);
            }

            List<string> perms = new List<string>();
            XmlNode node = doc.DocumentElement.SelectSingleNode("actuallyUsesPermissions");
            foreach (XmlNode node1 in node.ChildNodes)
            {
                perms.Add(node1.InnerText);
            }
            var app = new AppPermissions();
            app.Name = filepath.Substring(15);
            app.actuallyUsesPermissions = perms;
            app.declaresPermissions = fakeperms;
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
    }
}
