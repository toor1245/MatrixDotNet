using System;
using System.Reflection;

namespace MatrixDotNet.Extensions.Experimenting
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class Experiment : Attribute
    {
        public string Description { get; set; }

        private MethodBody Body { get; set; }

        public Experiment(string description,MethodBody body)
        {
            Description = description;
            Body = body;
        }
    }
}