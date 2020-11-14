using System.Text.RegularExpressions;

namespace Samples
{
    public class DynamicTokenDefinition : LocationEntity
    {
        public Regex Representation { get; }
        
        public static DynamicTokenDefinition[] Dynamics { get; }

        static DynamicTokenDefinition()
        {
            Dynamics = new[]
            {
                new DynamicTokenDefinition(@"(^builder\S*;)")
            };
        }
        
        public DynamicTokenDefinition(string rep)
        {
            Representation = new Regex(@"\G" + rep, RegexOptions.Compiled);
        }
    }
}