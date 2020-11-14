using System;
using System.IO;

using System.Text.RegularExpressions;

namespace Samples
{
    public class Parser
    {
        private readonly char[] _spaceChars = { ' ', '\n', '\r', '\t' };
        public  string Source { get; set; }
        private int _offset;

        public Parser(string src)
        {
            Source = File.ReadAllText(src);
            var regex = new Regex(@"(\w*builder\S*;)");
            var regex2 = new Regex(@"StringBuilder\D*;");
            var regex3 = new Regex(@"return");
            var matches = regex.Matches(Source);
            var matches2 = regex2.Matches(Source);
            var matches3 = regex3.Matches(Source);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    Source = Source.Replace(match.Value, "");
                }

                foreach (Match match in matches2)
                {
                    Source = Source.Replace(match.Value, "");
                }
                
                foreach (Match match in matches3)
                {
                    Source = Source.Replace(match.Value, "");
                }
            }
            else
            {
                Console.WriteLine("Совпадений не найдено");
            }
            //Parse();
        }
        
        private void Parse()
        {
            while(InBounds())
            {
                if(!InBounds()) break;
                if (Source[_offset].IsAnyOf(_spaceChars))
                {
                    _offset++;
                    continue;;
                }
                ProcessDynamic();
            }
        }
        
        private void ProcessDynamic()
        {
            foreach(var def in DynamicTokenDefinition.Dynamics)
            {
                var match = def.Representation.Match(Source, _offset);
                if (!match.Success)
                {
                    _offset++;
                    continue;
                }

                var replace = Regex.Replace(Source,@"(builder\S*;)",string.Empty);
                Source = replace;
                _offset += match.Length;
            }
        }
	
        private bool InBounds()
        {
            return _offset < Source.Length;
        }
        
    }
}