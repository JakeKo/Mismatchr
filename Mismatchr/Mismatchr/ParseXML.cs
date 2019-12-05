using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Mismatchr
{
    public class ParseXML
    {
        public AppPermissions getOneAppsPermissions(string filepath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);

        }

        public List<AppPermissions> getAllAppsPermissions()
        {
            string[] filePaths = Directory.GetFiles("just_xml_files/", "*.xml",
                SearchOption.TopDirectoryOnly);
            string [] paths = new string [] {"just_xml_files/akk_astro_droid_moonphase_2.xml", "just_xml_files/com_alaskalinuxuser_hourglass_6.xml",
                "just_xml_files/com_btcontract_wallet_52.xml", "just_xml_files/com_lolo_io_onelist_6.xml", "just_xml_files/com_mkulesh_micromath_plus_313.xml",
                "just_xml_files/com_tht_k3pler_4.xml"};
            List<AppPermissions> permissions = new List<AppPermissions>();
            foreach (var file in paths)
            {
                permissions.Add(getOneAppsPermissions(file));
            }

            return permissions;
        }
    }
}
