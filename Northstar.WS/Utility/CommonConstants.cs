using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northstar.WS.Utility
{
    public static class CommonConstants
    {
        #region Resource name constants for Controllers
        public static string ResourceNameForRoomController = "Room";
        public static string ResourceNameForHotelController = "Hotel";
        public static string ResourceNameForAddressController = "Address";
        #endregion

        public static string ResourcePlaceHolder = "<resource>";
        public static string ResourceIdPlaceHolder = "<resourceId>";
        public static string JSONToStringPlaceHolder = "<jsonToString>";
        public static Dictionary<int, string> CustomErrorResponses = new Dictionary<int, string>()
        {
            {101, "An unknown error occurred while trying to access the resource: <resource>" },
            {102, "An unknown error occurred while trying to modify the resource: <resource>" },
            {301, "<resource> not found with id: <resourceId>"},
            {302, "Insertion Error! Unable to add following <resource>: <jsonToString>" },
            {303, "Update Error! Unable to update following <resource>: <jsonToString>" }
        };
    }
}
