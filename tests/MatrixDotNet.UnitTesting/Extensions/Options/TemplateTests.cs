using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Options;
using Xunit;

namespace MatrixDotNetTests.Extensions.Options
{
    public class OptionsBinaryWriteReads
    {
        [Fact]
        public async void BinaryWriteReadInt()
        {
            var matrix = BuildMatrix.BuildRandom<int>(15, 12);
            var html = new TemplateHtml("test");
            
            await html.BinarySaveAsync(matrix);
            var resultMatrix = await html.BinaryOpenAsync<int>();

            Assert.Equal(matrix, resultMatrix);
        }

        [Fact]
        public async void BinaryWriteReadFloat()
        {
            var matrix = BuildMatrix.BuildRandom<float>(15, 12);
            var html = new TemplateHtml("test");

            await html.BinarySaveAsync(matrix);
            var resultMatrix = await html.BinaryOpenAsync<float>();

            Assert.Equal(matrix, resultMatrix);
        }

        [Fact]
        public async void BinaryWriteReadByte()
        {
            var matrix = BuildMatrix.BuildRandom<byte>(15, 12);
            var html = new TemplateHtml("test");

            await html.BinarySaveAsync(matrix);

            var resultMatrix = await html.BinaryOpenAsync<byte>();

            Assert.Equal(matrix, resultMatrix);
        }

        [Fact]
        public async void BinaryReadThrowsFileNotFoundException()
        {
            var html = new TemplateHtml("file_not_found");

            await Assert.ThrowsAsync<MatrixDotNetException>(() => html.BinaryOpenAsync<int>());
        }

        [Fact]
        public async void BinaryReadThrowsUnsupportedTypeException()
        {
            var matrix = BuildMatrix.BuildRandom<byte>(15, 12);
            var html = new TemplateHtml("test");

            await html.BinarySaveAsync(matrix);
            await Assert.ThrowsAsync<NotSupportedTypeException>(() => html.BinaryOpenAsync<char>());
        }
    }
}
