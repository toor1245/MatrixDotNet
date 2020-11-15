using System;

namespace Samples.Samples
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Output : Attribute
    {
        public DefineProject Project { get; }
        
        public Output(DefineProject project)
        {
            Project = project;
        }
    }
}