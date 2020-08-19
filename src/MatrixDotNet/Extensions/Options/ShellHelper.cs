using System.Diagnostics;
using System.Threading.Tasks;

namespace MatrixDotNet.Extensions.Options
{
#if OS_LINUX
    
    public static class ShellHelper
    {
        public static async Task<string> Bash(string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");
            
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            var result = await process.StandardOutput.ReadToEndAsync();
            return result;
        }
    }
#endif
}