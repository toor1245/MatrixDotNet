using System;

namespace Samples.Samples
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Output: Attribute
    {
        public Output()
        {
            Result = GetType();
        }

        public Type Result { get;  }

    }
}