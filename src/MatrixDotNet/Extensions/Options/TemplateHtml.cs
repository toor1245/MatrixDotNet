using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace MatrixDotNet.Extensions.Options
{
    public sealed class TemplateHtml : Template 
    {
        protected override string Text
        {
            get
            {
                return @"<!DOCTYPE html>
<html>
<head>
<link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css' integrity='sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm' crossorigin='anonymous'>
</head>
<body>
<table class='table'>";
            }
        }

        internal override string Path
        {
            get
            {
#if OS_WINDOWS
                return @$"{Folder}\{Title}.html";
#elif OS_LINUX
                return @$"{Folder}/{Title}.html";
#endif
            }
        }

        internal override string FullPath
        {
            get
            {
#if OS_WINDOWS
                return @$"{RootPath}\{Title}.html";
#elif OS_LINUX
                return @$"{RootPath}/{Title}.html";
#endif
            }
        }


        public TemplateHtml(string title) : base(title)
        {
            
        }
        
        public override string Save<T>(Matrix<T> matrix)
        {
            StringBuilder builder = new StringBuilder();
            Rows = matrix.Rows;
            Columns = matrix.Columns;
            builder.AppendLine(Text);
            builder.AppendLine("\t<thead class='thead-dark'>");
            builder.AppendLine("\t\t<tr>");
            builder.AppendLine("\t\t<th scope='col'>#</th>");
            
            for (int i = 0; i < matrix.Columns; i++)
            {
                builder.AppendLine($"\t\t<th scope='col'>{i}</th>");
            }
            
            builder.AppendLine("\t\t<tr>");
            builder.AppendLine("\t</thead>");
            builder.AppendLine("\t<tbody>");
            for (int i = 0; i < matrix.Rows; i++)
            {
                builder.AppendLine("\t\t<tr>");
                builder.AppendLine($"\t\t\t<th scope='row'>{i}</th>");
                
                for (int j = 0; j < matrix.Columns; j++)
                {
                    builder.AppendLine($"\t\t\t<td>{matrix[i,j]}</td>");
                }

                builder.AppendLine("\t\t</tr>");
            }
            
            builder.AppendLine("\t</tbody>");
            builder.AppendLine("</table>");
            builder.AppendLine("</body>");
            
            IsFileExists(Title);

            return builder.ToString();
        }

        public override async Task Open()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
#if OS_WINDOWS
            Process.Start("explorer.exe",Path);
#elif OS_LINUX
            await ShellHelper.Bash($"firefox {Path}");
#endif
            Console.ResetColor();
        }
    }
}