
namespace Northstar.WS.Infrastructure
{
    /// <summary>
    /// A model to hold the term to use as an orderBy param
    /// </summary>
    public class SortTerm
    {
        public string Name { get; set; }

        public bool IsDescending { get; set; }

        public bool Default { get; set; }
    }
}
