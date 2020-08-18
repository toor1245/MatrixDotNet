using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using MatrixDotNet.Extensions.Statistics;

namespace MatrixDotNet.Extensions.Options
{
    public abstract class Template
    {
        #region .properties
        
        protected abstract string Text { get; }

        public string Title { get;}

        protected static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();
        
        protected static DirectoryInfo Directory => new DirectoryInfo(Folder);

        protected static string RootPath { get; } = Directory.FullName;

        protected static string Folder
        {
            get
            {
                
#if OS_WINDOWS
                return ".\\MatrixLogs";
#elif OS_LINUX
                return "MatrixLogs";
#endif
            }
        }

        #endregion

        #region .ctor

        protected Template(string title)
        {
            Title = title;
            IsExists();
        }
        
        #endregion

        #region .methods

        internal abstract string Path { get; }

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
            FileInfo fileInfo = new FileInfo(Path);
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

        protected void Open()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
#if OS_WINDOWS
            Process.Start("explorer.exe",Title);
#elif OS_LINUX
            Process.Start("cat",$"{Path}");
#endif
            Console.ResetColor();
        }
        
        internal static int[] InitColumnSize<T>(Matrix<T> matrix) where T : unmanaged
        {
            var arr = matrix.MaxColumns();
            var arr2 = matrix.MinColumns();
            int[] output = new int[arr.Length];
            
            for (int i = 0; i < output.Length; i++)
            {
                var x = string.Format("{0:f2}",arr[i]).Length;
                var y = string.Format("{0:f2}", arr2[i]).Length;

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

        #endregion
    }
}