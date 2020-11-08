using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace MatrixDotNet.Extensions.Options
{
    public abstract class Template
    {
        #region .properties
        public string Title { get; }

        protected static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();

        private static DirectoryInfo Directory => new DirectoryInfo(Folder);

        protected static string RootPath { get; } = Directory.FullName;

        protected int Rows { get; set; }
        
        protected int Columns { get; set; }

        private static string Folder => Path.GetRelativePath(".", "MatrixLogs");

        public string RelativePath => Path.ChangeExtension(Path.Combine(Folder, Title), FileExtension);

        public string FullPath => Path.GetFullPath(RelativePath);

        private string PathBin => Path.ChangeExtension(Path.Combine(Folder, Title),".dat");

        public abstract string FileExtension { get; }

        #endregion

        #region .ctor

        protected Template(string title)
        {
            Title = title;
            if (!Directory.Exists)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\\\\*** MatrixLogs directory created at: {RootPath}\n");
                Directory.Create();
                Console.ResetColor();
            }
        }
        
        #endregion
        
        #region .methods
        
        public abstract string CreateText<T>(Matrix<T> matrix) where T : unmanaged;

        public void Open()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Process.Start(RelativePath);
            Console.ResetColor();
        }

        public Task BinarySaveAsync<T>(Matrix<T> matrix) where T : unmanaged
        {
            var binaryFormatter = new BinaryFormatter();
            using var stream = new FileStream(PathBin,FileMode.OpenOrCreate);
            binaryFormatter.Serialize(stream,matrix);
            return Task.CompletedTask;
        }

        public Task<Matrix<T>> BinaryOpenAsync<T>() where T : unmanaged
        {
            var binaryFormatter = new BinaryFormatter();
            using var stream = new FileStream(PathBin,FileMode.OpenOrCreate);
            var des = (Matrix<T>)binaryFormatter.Deserialize(stream);
            return Task.FromResult(des);
        }
        
        #endregion
    }
}