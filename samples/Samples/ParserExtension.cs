using System.Linq;

namespace Samples
{
    public static class ParserExtension
    {
        public static bool IsAnyOf(this char source,char[] spaceChars)
        {
            return spaceChars.Any(t => source == t);
        }
    }
}