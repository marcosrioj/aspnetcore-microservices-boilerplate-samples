using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Tool.CreateNewMicroservice.Extensions
{
    public static class ShellHelper
    {
        public static string ExecCommand(this string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");
            var isWindowsPlataform = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = isWindowsPlataform ? "cmd.exe" : "/bin/bash",
                    Arguments = isWindowsPlataform ? $"/c \"{escapedArgs}\"" : $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;

        }
    }
}
