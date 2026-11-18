using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace PainelOtimizacao
{
    public static class ScriptRunner
    {
        public static void Executar(string subpasta, string nomeArquivo)
        {
            try
            {
                string caminhoBase = AppDomain.CurrentDomain.BaseDirectory;
                string caminhoCompleto = Path.Combine(caminhoBase, "scripts", subpasta, nomeArquivo);

                if (File.Exists(caminhoCompleto))
                {
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/c \"{caminhoCompleto}\"",
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true
                    };

                    Process.Start(psi);
                }
                else
                {
                    MessageBox.Show($"Script não encontrado:\n{caminhoCompleto}", "Erro de Execução", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao tentar rodar o script: {ex.Message}", "Erro Crítico", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}