using System.Text;

namespace MatrixDotNet.Extensions.Options
{
    internal struct Template
    {
        public static string Text
        {
            get
            {
                return @"<!DOCTYPE html>
<html>
<head>
<style>
table {
  width:100%;
}
table, th, td {
  border: 1px solid black;
  border-collapse: collapse;
}
th, td {
  padding: 12px;
  text-align: left;
}
#t01 tr:nth-child(even) {
  background-color: #eee;
}
#t01 tr:nth-child(odd) {
 background-color: #fff;
}
#t01 th {
  background-color: black;
  color: white;
}
</style>
</head>
<body>
<table id='t01'>
";
            }
        }

        public static string SetMatrixToHtml<T>(Matrix<T> matrix) where T : unmanaged
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("\t<tr>");
            for (int i = 0; i < matrix.Columns; i++)
            {
                builder.AppendLine($"\t\t<th>{i}</th>");
            }
            builder.AppendLine("\t<tr>");
            
            for (int i = 0; i < matrix.Rows; i++)
            {
                builder.AppendLine("\t<tr>");
                for (int j = 0; j < matrix.Columns; j++)
                {
                    builder.AppendLine($"\t\t<td>{matrix[i,j]}</td>");
                }

                builder.AppendLine("\t</tr>");
            }

            return builder.ToString();
        }
    }
}