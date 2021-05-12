
namespace Northstar.WS.Infrastructure
{
    /// <summary>
    /// Class representing the search query
    /// </summary>
    public class SearchTerm
    {
        public string Name { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
        public bool IsValidSyntax { get; set; }
    }
}
