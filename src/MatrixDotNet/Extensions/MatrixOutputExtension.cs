using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MatrixDotNet.Extensions.Options;

namespace MatrixDotNet.Extensions
{
    public static partial class MatrixExtension
    {
        
        /// <summary>
        /// Pretty output.
        /// </summary>
        /// <param name="matrix">the matrix which to display.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        public static void Pretty<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (matrix is null)
                throw new NullReferenceException();
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine($"Number of rows: {matrix.Rows}");
                builder.AppendLine($"Number of columns: {matrix.Columns}\n");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(Output(matrix,builder));
                Console.ResetColor();
            }
        }
        
        
        /// <summary>
        /// Saves matrix to html or markdown.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="template">config for creates html or markdown.</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>Saves matrix to html or markdown.</returns>
        public static async Task SaveAsync<T>(this Matrix<T> matrix,Template template) where T : unmanaged
        {
            try 
            {
                using var stream = new StreamWriter(template.Path,false,Encoding.UTF8);
                await stream.WriteLineAsync(template.Save(matrix));
                stream.Close();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e);
                Console.ResetColor();
                throw;
            }
        }

        public static async Task SaveAndOpenAsync<T>(this Matrix<T> matrix,Template template) where T : unmanaged
        {
            await SaveAsync(matrix, template);
            using var stream = new StreamReader(template.Path,Encoding.UTF8);
            await template.Open();
            stream.Close();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\n\\\\*** File {template.Title} opened");
            Console.ResetColor();
        }
        
        internal static string Output<T>(Matrix<T> matrix,StringBuilder builder) where T : unmanaged
        {
            int[] output = Template.InitColumnSize(matrix);
            builder.AppendLine();
            
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

            return builder.ToString();
        }
    }
}