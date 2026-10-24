using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace PainelOtimizacao
{
    public static class ScriptRunner
    {
        private static readonly string BaseBinPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");

        public static async Task<bool> RunScriptAsync(string relativePath, string arguments = "")
        {
            return await Task.Run(() =>
            {
                string? cleanRelativePath = relativePath?.Replace('/', Path.DirectorySeparatorChar);
                if (string.IsNullOrEmpty(cleanRelativePath)) return false;

                string scriptFullPath = Path.Combine(BaseBinPath, cleanRelativePath);

                if (!File.Exists(scriptFullPath)) return false;

                try
                {
                    string? workingDir = Path.GetDirectoryName(scriptFullPath);
                    if (string.IsNullOrEmpty(workingDir)) return false;

                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/c \"\"{scriptFullPath}\" {arguments}\"",
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        WorkingDirectory = workingDir
                    };

                    using (Process? process = Process.Start(psi))
                    {
                        if (process == null) return false;
                        process.WaitForExit();
                        return process.ExitCode == 0;
                    }
                }
                catch
                {
                    return false;
                }
            });
        }
    }
}