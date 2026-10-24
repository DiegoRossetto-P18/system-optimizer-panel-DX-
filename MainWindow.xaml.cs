using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace PainelOtimizacao
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnOtimizacaoCompleta_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TxtStatus.Text = "Executando otimização completa...";

                // Localiza o diretório atual de execução
                string currentDir = Directory.GetCurrentDirectory();
                string scriptPath = System.IO.Path.Combine(currentDir, "bin", "otimizacao-completa.bat");

                // Se não encontrar na raiz atual, procura subindo as pastas da build
                if (!System.IO.File.Exists(scriptPath))
                {
                    string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                    DirectoryInfo? dir = new DirectoryInfo(baseDir);

                    // Subir até encontrar a pasta que contém a subpasta 'bin' com os scripts
                    while (dir != null && !System.IO.File.Exists(System.IO.Path.Combine(dir.FullName, "bin", "otimizacao-completa.bat")))
                    {
                        dir = dir.Parent;
                    }

                    if (dir != null)
                    {
                        scriptPath = System.IO.Path.Combine(dir.FullName, "bin", "otimizacao-completa.bat");
                    }
                }

                if (!System.IO.File.Exists(scriptPath))
                {
                    MessageBox.Show($"O script não foi encontrado no caminho:\n{scriptPath}", "Erro de Caminho", MessageBoxButton.OK, MessageBoxImage.Warning);
                    TxtStatus.Text = "Script não encontrado.";
                    return;
                }

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c \"{scriptPath}\"",
                    WorkingDirectory = System.IO.Path.GetDirectoryName(scriptPath) ?? string.Empty,
                    UseShellExecute = true,
                    Verb = "runas",
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process? proc = Process.Start(psi);
                if (proc != null)
                {
                    proc.WaitForExit();
                    TxtStatus.Text = "Otimização concluída! Verifique o log.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao executar otimização: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtStatus.Text = "Falha na execução.";
            }
        }
    }
}