using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mismatchr
{
    public class AppPermissions
    {
        public string Name { get; set; }
        public List <string> declaresPermissions { get; set; }
        public List<string> actuallyUsesPermissions { get; set; }
    }
}
