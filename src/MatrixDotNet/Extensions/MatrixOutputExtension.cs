using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MatrixDotNet.Extensions.Statistics;

namespace MatrixDotNet.Extensions
{
    public static partial class MatrixExtension
    {
        /// <summary>
        /// Pretty output.
        /// </summary>
        /// <param name="matrix"></param>
        /// <typeparam name="T"></typeparam>
        public static void Pretty<T>(this Matrix<T> matrix) where T : unmanaged
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            SetColorMessage(matrix);
            Console.ResetColor();
        }

        /// <summary>
        /// Save matrix to markdown.
        /// </summary>
        /// <param name="matrix">matrix which save to file.</param>
        /// <param name="title">title markdown.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Save matrix to markdown.</returns>
        public static async Task SaveAsync<T>(this Matrix<T> matrix,string title) where T : unmanaged
        {
            string logs = ".\\MatrixLogs2";
            DirectoryInfo directoryInfo = new DirectoryInfo(logs);
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            
            if (!directoryInfo.Exists)
            {
                string path = Directory.GetParent(assembly.Location).FullName;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\\\\*** MatrixLogs2 directory created at: {path}\\ \n");
                directoryInfo.Create();
                Console.ResetColor();
            }

            try
            {
                using (StreamWriter stream = new StreamWriter(@$"{logs}\{title}.md",false,Encoding.UTF8))
                {
                    var output = matrix.InitColumnSize();
                    
                    await stream.WriteLineAsync("```ini" + stream.NewLine + assembly.FullName + stream.NewLine + "```" + stream.NewLine + 
                                                $"Number of rows: {matrix.Rows}" + stream.NewLine + $"Number of columns: {matrix.Columns}" 
                                                + stream.NewLine );

                    int sum = 0;
                    int[] slash = new int[matrix.Columns];
                    for (int i = 0; i < matrix.Columns; i++)
                    {
                        sum += output[i] + 3;
                        string format = $"| {i} " + "".PadRight(output[i]);
                        slash[i] = format.Length;
                        await stream.WriteAsync(format);
                    }
                    await stream.WriteAsync("|" + stream.NewLine + "|");
                    int value = slash[0];

                    for (int i = 1, j = 0; i < matrix.Columns + sum;i++)
                    {
                        if (i == value)
                        {
                            await stream.WriteAsync("|");
                            j++;
                            value += slash[j];
                        }
                        else
                        {
                            await stream.WriteAsync("-");
                        }
                    }

                    await stream.WriteLineAsync();

                    for (int i = 0; i < matrix.Rows; i++)
                    {
                        await stream.WriteAsync("|");
                        for (int j = 0; j < matrix.Columns; j++)
                        {
                            var n = output[j];
                            int length = $"{matrix[i, j].ToString():f2}".Length;
                            string format = $"{matrix[i, j].ToString():f2}";
                            if (length >= n)
                            {
                                await stream.WriteAsync(format + "".PadRight((length - n) + 3) + "|");
                            }
                            else
                            {
                                await stream.WriteAsync(format + "".PadRight((n - length) + 3) + "|");
                            }
                        }

                        await stream.WriteLineAsync();
                    }
                    
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\\\\*** Created file {title}.md");
                    Console.ResetColor();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static async Task SaveAndOpenAsync<T>(this Matrix<T> matrix,string title) where T : unmanaged
        {
            #if OS_WINDOWS
            
            await matrix.SaveAsync(title);
            Process.Start("explorer.exe",$".\\MatrixLogs\\{title}.md");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\\\\*** File {title}.md opened");
            Console.ResetColor();
            
            #elif OS_LINUX
            await matrix.SaveAsync(title);
            Process.Start("explorer.exe",$".\\MatrixLogs\\{title}.md");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\\\\*** File {title}.md opened");
            Console.ResetColor();
            
            #elif OS_MAC

            #endif
        }

        private static void SetColorMessage<T>(Matrix<T> matrix) where  T : unmanaged
        {
            if (matrix is null)
                throw new NullReferenceException();
            
            var output = matrix.InitColumnSize();
            SetColorMessageSmallSize(matrix, output);

        }
        private static void SetColorMessageSmallSize<T>(Matrix<T> matrix,int[] output) where T : unmanaged 
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine();
            
            builder.AppendLine($"Number of rows: {matrix.Rows}");
            builder.AppendLine($"Number of columns: {matrix.Columns}\n");
            
            for (int i = 0; i < matrix.Rows; i++)
            {

                for (int j = 0; j < matrix.Columns; j++)
                {
                    var n = output[j];
                    int length = string.Format("{0:f2}",matrix[i, j]).Length;
                    string format = string.Format("{0:f2}",matrix[i, j]);
                    if (length >= n)
                    {
                        builder.Append(" ".PadLeft(2) + format + "".PadRight(length - n) + "  |");    
                    }
                    else
                    {
                        builder.Append(" ".PadLeft(2) + format + "".PadRight(n - length) + "  |");
                    }
                }

                builder.AppendLine();
            }

            Console.WriteLine(builder.ToString());
        }
        
        internal static string OutputPretty<T>(Matrix<T> matrix) where  T : unmanaged
        {
            if (matrix is null)
                throw new NullReferenceException();

            StringBuilder builder = new StringBuilder();
            builder.AppendLine();

            int n = 9;
            
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    int length = $"{matrix[i, j]:f2}".Length;
                    string format = $"{matrix[i, j]:f2}";
                    
                    if (length > n)
                    {
                        builder.Append(" ".PadLeft(2) + format + "".PadRight(length - n) +   "|");    
                    }
                    else
                    {
                        builder.Append(" ".PadLeft(2) + format + "".PadRight(n + (n - length)) +   "|");
                    }
                }

                builder.AppendLine();
            }
            
            return builder.ToString();
        }

        private static int[] InitColumnSize<T>(this Matrix<T> matrix) where T : unmanaged
        {
            T[] arr = matrix.MaxColumns();
            T[] arr2 = matrix.MinColumns();
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
    }
}