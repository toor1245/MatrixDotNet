using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using MatrixDotNet.Extensions.Statistics;

namespace MatrixDotNet.Extensions.Options
{
    public abstract class Template
    {
        #region .properties
        
        protected abstract string Text { get; }

        public string Title { get; }

        protected static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();

        private static DirectoryInfo Directory => new DirectoryInfo(Folder);

        protected static string RootPath { get; } = Directory.FullName;

        protected int Rows { get; set; }
        
        protected int Columns { get; set; }

        private static string Folder => Path.Combine(FormatStorage.MatrixLogs);

        protected internal virtual string GetPath() => Path.Combine(Folder, Title);
        
        protected abstract string FullPath { get; }

        private string PathBin => Path.Combine(Folder, Title) + FormatStorage.Dat;

        #endregion

        #region .ctor

        protected Template(string title)
        {
            Title = title;
            IsExists();
        }
        
        #endregion

        #region .methods
        

        private static void IsExists()
        {
            if (!Directory.Exists)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\\\\*** MatrixLogs directory created at: {RootPath}\n");
                Directory.Create();
                Console.ResetColor();
            }
        }

        protected void IsFileExists(string title)
        {
            FileInfo fileInfo = new FileInfo(GetPath());
            if (!fileInfo.Exists)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\\\\*** Created file at: {title}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\\\\*** Update file at: {title}");
                Console.ResetColor();
            }
        }

        public abstract string Save<T>(Matrix<T> matrix) where T : unmanaged;

        public abstract void Open();

        internal static int[] InitColumnSize<T>(Matrix<T> matrix) where T : unmanaged
        {
            var arr = matrix.MaxColumns();
            var arr2 = matrix.MinColumns();
            int[] output = new int[arr.Length];
            
            for (int i = 0; i < output.Length; i++)
            {
                var x = $"{arr[i]:f2}".Length;
                var y = $"{arr2[i]:f2}".Length;

                if (x > y)
                {
                    output[i] = x;
                }
                else
                {
                    output[i] = y;
                }
            }

            return output;
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