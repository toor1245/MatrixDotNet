using System;

namespace MatrixDotNet.Extensions.Statistics
{
    public abstract class ConfigStatistics<T> where T : unmanaged
    {
        protected string Description { get;  }
        protected int Size { get; }
        protected Matrix<T> _matrix;
        private Table[] _tables;

        public ConfigStatistics(Matrix<T> matrix,Table[] tables,string title,int size)
        {
            Description = title;
            Size = size;
            _matrix = matrix;
            _tables = tables;
        }
        
    }
}