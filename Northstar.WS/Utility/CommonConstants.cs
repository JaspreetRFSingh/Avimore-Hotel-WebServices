using System.Collections.Generic;

namespace Northstar.WS.Utility
{
    /// <summary>
    /// Static class CommonConstants having all the common constants to be used at various places in the application 
    /// </summary>
    public static class CommonConstants
    {
        #region DB Related constants
        public static string DefaultConnectionStringAvimoreDb = "name=ConnectionStrings:DefaultConnection";
        #endregion

        #region Resource name constants for Controllers
        public static string ResourceNameForRoomController = "Room";
        public static string ResourceNameForHotelController = "Hotel";
        public static string ResourceNameForAddressController = "Address";
        public static string ResourceNameForUserController = "User";
        #endregion

        public static string ResourcePlaceHolder = "<resource>";
        public static string ResourceIdPlaceHolder = "<resourceId>";
        public static string JSONToStringPlaceHolder = "<jsonToString>";
        public static Dictionary<int, string> CustomGenericServiceResponses = new Dictionary<int, string>()
        {
            {101, "An unknown error occurred while trying to access the resource: <resource>" },
            {102, "An unknown error occurred while trying to modify the resource: <resource>" },
            {200, "Retrieval successful for <resource>" },
            {201, "Inserting a record for <resource> was performed successfully!" },
            {202, "Updating a record for <resource> was performed successfully!" },
            {203, "Deleting a record for <resource> was performed successfully!" },
            {300, "No records exist for <resource>s!" },
            {301, "<resource> not found with id: <resourceId>"},
            {302, "Insertion Error! Unable to add following <resource>: <jsonToString>" },
            {303, "Update Error! Unable to update following <resource>: <jsonToString>" }
        };
    }
}
