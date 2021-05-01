using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northstar.WS.Utility
{
    public static class CommonConstants
    {
        #region Resource name constants for Controllers
        public static string Room = "Room";
        public static string Hotel = "Hotel";
        #endregion

        public static string ResourcePlaceHolder = "<resource>";
        public static string ResourceIdPlaceHolder = "<resourceId>";
        public static Dictionary<int, string> CustomErrorResponses = new Dictionary<int, string>()
        {
            {301, "<resource> not found with <resourceId>"},
            {302, "Error!" }
        };
    }
}
