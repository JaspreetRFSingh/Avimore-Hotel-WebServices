namespace Northstar.WS.Models
{
    //Generic error class to represent a model
    public class ApiError
    {
        public string Message
        {
            get; set;
        }
        public int code { get; set; }
    }
}
