namespace Northstar.WS.Models
{
    //Generic model class to represent an error/success response
    public class GenericApiResponse
    {
        public string Message
        {
            get; set;
        }
        public int code { get; set; }
    }
}
