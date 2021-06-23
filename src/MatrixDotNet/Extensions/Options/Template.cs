using System;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions.Options
{
    internal static class BinaryWriteExtensions<T>
    {
        private static Action<BinaryWriter, T> _writeFunc;

        public static Action<BinaryWriter, T> GetWriteFunc()
        {
            if (_writeFunc != null)
                return _writeFunc;

            var firstPar = Expression.Parameter(typeof(BinaryWriter), "value");
            var secondPar = Expression.Parameter(typeof(T), "value");

            var info = typeof(BinaryWriter).GetMethod("Write", new[] { secondPar.Type });
            if (info == null)
                throw new InvalidOperationException();

            var call = Expression.Call(firstPar, info, secondPar);
            var func = Expression.Lambda<Action<BinaryWriter, T>>(call, firstPar, secondPar).Compile();
            _writeFunc = func;

            return _writeFunc;
        }

        public static T Read(BinaryReader reader)
        {
            if (typeof(T) == typeof(byte))
            {
                var value = reader.ReadByte();
                return Unsafe.As<byte, T>(ref value);
            }
            if (typeof(T) == typeof(sbyte))
            {
                var value = reader.ReadSByte();
                return Unsafe.As<sbyte, T>(ref value);
            }
            if (typeof(T) == typeof(short))
            {
                var value = reader.ReadInt16();
                return Unsafe.As<short, T>(ref value);
            }
            if (typeof(T) == typeof(ushort))
            {
                var value = reader.ReadUInt16();
                return Unsafe.As<ushort, T>(ref value);
            }
            if (typeof(T) == typeof(int))
            {
                var value = reader.ReadInt32();
                return Unsafe.As<int, T>(ref value);
            }
            if (typeof(T) == typeof(uint))
            {
                var value = reader.ReadUInt32();
                return Unsafe.As<uint, T>(ref value);
            }
            if (typeof(T) == typeof(long))
            {
                var value = reader.ReadInt64();
                return Unsafe.As<long, T>(ref value);
            }
            if (typeof(T) == typeof(ulong))
            {
                var value = reader.ReadUInt64();
                return Unsafe.As<ulong, T>(ref value);
            }
            if (typeof(T) == typeof(float))
            {
                var value = reader.ReadSingle();
                return Unsafe.As<float, T>(ref value);
            }
            if (typeof(T) == typeof(double))
            {
                var value = reader.ReadDouble();
                return Unsafe.As<double, T>(ref value);
            }
            if (typeof(T) == typeof(decimal))
            {
                var value = reader.ReadDecimal();
                return Unsafe.As<decimal, T>(ref value);
            }

            throw new NotSupportedTypeException($"{typeof(T)} is unsupported");
        }
    }

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

        private string PathBin => Path.ChangeExtension(Path.Combine(Folder, Title), ".dat");

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
            using var stream = new FileStream(PathBin, FileMode.OpenOrCreate);
            using BinaryWriter writer = new BinaryWriter(stream);
            var writeFunc = BinaryWriteExtensions<T>.GetWriteFunc();
            var array = matrix.GetArray();

            writer.Write(matrix.Rows);
            writer.Write(matrix.Columns);
            for (int i = 0; i < array.Length; i++)
            {
                writeFunc(writer, array[i]);
            }

            return Task.CompletedTask;
        }

        public Task<Matrix<T>> BinaryOpenAsync<T>() where T : unmanaged
        {
            if (!File.Exists(PathBin))
            {
                throw new MatrixDotNetException($"File not found: \"{PathBin}\"");
            }

            using var stream = new FileStream(PathBin, FileMode.Open);

            using var reader = new BinaryReader(stream);

            int rows = reader.ReadInt32();
            int columns = reader.ReadInt32();

            var array = new T[rows * columns];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = BinaryWriteExtensions<T>.Read(reader);
            }
            var matrix = new Matrix<T>(array, rows, columns);
            return Task.FromResult(matrix);
        }

        #endregion
    }
}