using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MatrixDotNet.Extensions.Options;
using MatrixDotNet.Extensions.Statistics;

namespace MatrixDotNet.Extensions
{
    public static partial class MatrixExtension
    {
        private static Assembly Assembly { get; } = Assembly.GetExecutingAssembly();
        private static string RootPath { get; } = Directory.FullName;

        private static string Folder
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

        private static string TemplateFolderPath
        {
            get
            {
                #if OS_WINDOWS
                return ".\\templates";
                #elif OS_LINUX
                
                return "../../../MatrixDotNet/templates";
                
                #endif
            }
        }
        
        private static string DoctypeTemplatePath
        {
            get
            {
                #if OS_WINDOWS
                return ".\\templates";
                #elif OS_LINUX
                return @"\templates\doctype.txt";
                #endif
            }
        }

        private static string HtmlTemplate => File.ReadAllText(DoctypeTemplatePath);

        private static DirectoryInfo Directory => new DirectoryInfo(Folder);

        private static string PathMarkdown(string title)
        {
            #if OS_WINDOWS
            return @$"{Folder}\{title}.md";
            #elif OS_LINUX
            return @$"{Folder}/{title}.md";
            #endif
        }
        
        private static string PathHtml(string title)
        {
            #if OS_WINDOWS
            return @$"{Folder}\{title}.html";
            #elif OS_LINUX
            return @$"{Folder}/{title}.html";
            #endif
        }

        /// <summary>
        /// Pretty output.
        /// </summary>
        /// <param name="matrix">the matrix which to display.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        public static void Pretty<T>(this Matrix<T> matrix) where T : unmanaged
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            SetColorMessage(matrix);
            Console.ResetColor();
        }
        
        /// <summary>
        /// Saves matrix to markdown.
        /// </summary>
        /// <param name="matrix">matrix which save to file.</param>
        /// <param name="title">title markdown.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Saves matrix to markdown.</returns>
        public static async Task SaveToMarkdownAsync<T>(this Matrix<T> matrix,string title) where T : unmanaged
        {
            if (!Directory.Exists)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\\\\*** MatrixLogs directory created at: {RootPath}\\ \n");
                Directory.Create();
                Console.ResetColor();
            }
            try
            {
                string path = PathMarkdown(title);
                using StreamWriter stream = new StreamWriter(path,false,Encoding.UTF8);
                var output = matrix.InitColumnSize();
                    
                await stream.WriteLineAsync("```ini" + stream.NewLine + Assembly.FullName + stream.NewLine + stream.NewLine + 
                                            $"Number of rows: {matrix.Rows}" + stream.NewLine + 
                                            $"Number of columns: {matrix.Columns}" + stream.NewLine + stream.NewLine + 
                                            "```" + stream.NewLine);

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
                        int length = string.Format("{0:f2}",matrix[i, j]).Length;
                        string format = string.Format("{0:f2}",matrix[i, j]);
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
                
                FileInfo fileInfo = new FileInfo(PathMarkdown(title));
                if (!fileInfo.Exists)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\\\\*** Created file {title}.md");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine($"\\\\*** Update file {title}.md");
                    Console.ResetColor();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        
        /// <summary>
        /// Saves matrix to html.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="title">the title of file *.html.</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>Saves Matrix to Html.</returns>
        public static async Task SaveToHtmlAsync<T>(this Matrix<T> matrix,string title) where T : unmanaged
        {
            if (!Directory.Exists)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\\\\*** MatrixLogs directory created at: {RootPath}\\ \n");
                Directory.Create();
                Console.ResetColor();
            }
            
            try
            {
                string path = PathHtml(title);
                
                using StreamWriter stream = new StreamWriter(path,false,Encoding.UTF8);
                var output = matrix.InitColumnSize();

                await stream.WriteLineAsync(Template.Text);
                await stream.WriteLineAsync(Template.SetMatrixToHtml(matrix));
                await stream.WriteLineAsync("</table>" + stream.NewLine + "</body>");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static async Task SaveAndOpenAsync<T>(this Matrix<T> matrix,string title) where T : unmanaged
        {
            await matrix.SaveToMarkdownAsync(title);
            OpenFileOs(title);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n\\\\*** File {PathMarkdown(title)} opened");
            Console.ResetColor();
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
        
        // opens file by Os
        private static void OpenFileOs(string title)
        {
            #if OS_WINDOWS
            Process.Start("explorer.exe",PathMarkdown(title));
            #elif OS_LINUX
            Console.ForegroundColor = ConsoleColor.Yellow;
            Process.Start("cat",$"{PathMarkdown(title)}");
            Console.ResetColor();
            #endif
        }
        
        private static string GetAssemblyDirectory(System.Reflection.Assembly assembly) {
            return System.IO.Path.GetDirectoryName(assembly.Location);
        }
    }
}