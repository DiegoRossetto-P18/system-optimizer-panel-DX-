using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using IOPath = System.IO.Path;

namespace PainelOtimizacaoDx;

public partial class MainWindow : Window
{
    private readonly Dictionary<string, ObservableCollection<OptimizationOption>> _optionsByTab;
    private Button? _activeInfoButton;
    private int _themeIndex;

    public ObservableCollection<TabItemModel> Tabs { get; } = new();
    public ObservableCollection<OptimizationOption> ActiveOptions { get; set; } = new();

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
        _optionsByTab = CreateOptions();
        CreateTabs();
        Loaded += (_, _) => ApplyTheme(0);
        SetActiveTab(Tabs[0]);
    }

    private void CreateTabs()
    {
        Tabs.Add(new TabItemModel("GAMES", "🎮", true));
        Tabs.Add(new TabItemModel("INPUT", "🖱️", false));
        Tabs.Add(new TabItemModel("NETWORK", "🌐", false));
        Tabs.Add(new TabItemModel("WINDOWS", "🛡️", false));
        Tabs.Add(new TabItemModel("STORAGE", "⚡", false));
    }

    private Dictionary<string, ObservableCollection<OptimizationOption>> CreateOptions()
    {
        return new Dictionary<string, ObservableCollection<OptimizationOption>>
        {
            ["GAMES"] = new(
            [
                new OptimizationOption("Prioridade e Agendamento para Jogos", "Define uma prioridade maior para o jogo ativo e ajusta o agendamento do sistema para reduzir interrupções durante a partida."),
                new OptimizationOption("Desativar GameDVR e Capturas", "Desliga gravações e capturas automáticas em segundo plano para liberar recursos do processador, memória e disco."),
                new OptimizationOption("Desativar Mitigações de CPU (Spectre/Meltdown)", "Ajusta as mitigações de segurança do processador que podem reduzir desempenho. Use apenas se compreender o impacto de segurança."),
                new OptimizationOption("Aceleração de GPU (HAGS)", "Ativa o agendamento de GPU acelerado por hardware quando o Windows e o driver instalados oferecem suporte."),
                new OptimizationOption("Modo de Alta Performance de Energia", "Seleciona um perfil de energia voltado para desempenho, reduzindo economia agressiva de energia durante o uso.")
            ]),
            ["INPUT"] = new(
            [
                new OptimizationOption("Remover Aceleração e Curva do mouse", "Reduz o processamento adicional do ponteiro para deixar o movimento do mouse mais previsível e consistente."),
                new OptimizationOption("Ajustar Fila de Periféricos", "Ajusta o tratamento da fila de entrada para priorizar respostas de teclado, mouse e outros periféricos.")
            ]),
            ["NETWORK"] = new(
            [
                new OptimizationOption("Otimização Avançada TCP/IP e DNS", "Aplica configurações avançadas de pilha TCP/IP e resolução de nomes para conexões mais rápidas."),
                new OptimizationOption("Bloquear Throttling de Rede", "Remove limitações impostas pelo Windows na entrega de pacotes em cenários de baixa latência."),
                new OptimizationOption("Otimizar Parâmetros TCP/IP", "Ajusta janelas de recepção e algoritmos de congestionamento para reduzir atrasos."),
                new OptimizationOption("Otimizar Rede e DNS", "Configura rotas e cache DNS para melhorar o tempo de resposta em servidores.")
            ]),
            ["WINDOWS"] = new(
            [
                new OptimizationOption("Desativar Telemetria", "Bloqueia a coleta e envio de dados de diagnóstico em segundo plano do Windows."),
                new OptimizationOption("Otimizar Serviços de Telemetria e Uso Geral", "Desativa serviços secundários desnecessários para liberar memória RAM e ciclos de CPU."),
                new OptimizationOption("Gerenciar Serviços Opcionais", "Ajusta recursos e serviços opcionais do sistema que consomem recursos continuamente."),
                new OptimizationOption("Gerenciar Apps em Segundo Plano", "Impede que aplicativos da loja rodem ocultos consumindo bateria e processamento."),
                new OptimizationOption("Desativar Timers do Windows", "Ajusta os temporizadores do kernel para melhorar a precisão e consistência de latência."),
                new OptimizationOption("Gerenciar Serviço Gráfico", "Otimiza o gerenciamento de composição de janelas e aceleração gráfica do desktop."),
                new OptimizationOption("Reinicialização Rápida de Processos", "Acelera o encerramento e reinício de processos travados do sistema.")
            ]),
            ["STORAGE"] = new(
            [
                new OptimizationOption("Aumentar Memória Virtual (BCD)", "Configura os parâmetros de paginação e memória virtual no boot para evitar gargalos."),
                new OptimizationOption("Configurar Uso de Memória NTFS", "Aumenta o cache do sistema de arquivos NTFS para acelerar leitura e gravação em disco."),
                new OptimizationOption("Consultar Uso de Memória NTFS", "Exibe o status atual de alocação de memória do sistema de arquivos.")
            ])
        };
    }

    private void SetActiveTab(TabItemModel selectedTab)
    {
        foreach (TabItemModel tab in Tabs)
        {
            tab.IsActive = ReferenceEquals(tab, selectedTab);
        }

        ActiveOptions = _optionsByTab[selectedTab.Name];
        DataContext = null;
        DataContext = this;

        // Atualiza o estado do toggle global com base se todas as opções de todas as abas estão ativadas
        CheckGlobalToggleState();
    }

    private void CheckGlobalToggleState()
    {
        bool allEnabled = _optionsByTab.Values.All(list => list.All(opt => opt.IsEnabled));
        GlobalOptimizationToggle.IsChecked = allEnabled;
    }

    private void ApplyTheme(int themeIndex)
    {
        _themeIndex = themeIndex;
        ThemeSelectorButton.ApplyTemplate();
        Border? track = ThemeSelectorButton.Template.FindName("ThemeTrack", ThemeSelectorButton) as Border;
        Ellipse? thumb = ThemeSelectorButton.Template.FindName("ThemeThumb", ThemeSelectorButton) as Ellipse;

        if (themeIndex == 0)
        {
            LinearGradientBrush fullSpectrumGradient = new()
            {
                MappingMode = BrushMappingMode.Absolute,
                StartPoint = new Point(20, 0),
                EndPoint = new Point(Math.Max(ActualWidth - 20, 900), 0),
                GradientStops = new GradientStopCollection
                {
                    new((Color)ColorConverter.ConvertFromString("#56F0EA"), 0.00),
                    new((Color)ColorConverter.ConvertFromString("#83ED8E"), 0.28),
                    new((Color)ColorConverter.ConvertFromString("#F2EA79"), 0.52),
                    new((Color)ColorConverter.ConvertFromString("#B99BF8"), 0.75),
                    new((Color)ColorConverter.ConvertFromString("#E98DF7"), 1.00)
                }
            };

            LinearGradientBrush subtleFrameGradient = fullSpectrumGradient.Clone();
            subtleFrameGradient.Opacity = 0.25;

            LinearGradientBrush tabBorderGradient = fullSpectrumGradient.Clone();
            tabBorderGradient.Opacity = 0.72;

            Resources["TextAccentBrush"] = fullSpectrumGradient;
            Resources["LogoAccentBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#73F5E4"));
            Resources["AccentBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#5CE1CE"));
            Resources["BorderAccentBrush"] = tabBorderGradient;
            Resources["SubtleFrameBrush"] = subtleFrameGradient;
            Resources["SecondaryTextBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#DED3EC"));

            if (track != null && thumb != null)
            {
                track.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#26343A"));
                thumb.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#69A39B"));
                thumb.HorizontalAlignment = HorizontalAlignment.Left;
            }
        }
        else if (themeIndex == 1)
        {
            SolidColorBrush whiteBrush = new(Colors.White);
            Resources["TextAccentBrush"] = whiteBrush;
            Resources["LogoAccentBrush"] = whiteBrush;
            Resources["AccentBrush"] = whiteBrush;
            Resources["BorderAccentBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B3FFFFFF"));
            Resources["SubtleFrameBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#38FFFFFF"));
            Resources["SecondaryTextBrush"] = whiteBrush;

            if (track != null && thumb != null)
            {
                track.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E8E8EC"));
                thumb.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#111115"));
                thumb.HorizontalAlignment = HorizontalAlignment.Center;
            }
        }
        else
        {
            SolidColorBrush whiteBrush = new(Colors.White);
            Resources["TextAccentBrush"] = whiteBrush;
            Resources["LogoAccentBrush"] = whiteBrush;
            Resources["AccentBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8C92A4"));
            Resources["BorderAccentBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#99FFFFFF"));
            Resources["SubtleFrameBrush"] = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#22FFFFFF"));
            Resources["SecondaryTextBrush"] = whiteBrush;

            if (track != null && thumb != null)
            {
                track.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2E2D3D"));
                thumb.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0C0C10"));
                thumb.HorizontalAlignment = HorizontalAlignment.Right;
            }
        }

        LoadBackgroundForTheme();
    }

    private string? ResolveAssetPath(string fileName)
    {
        string[] candidateFolders =
        [
            IOPath.Combine(AppContext.BaseDirectory, "assets", "imagens"),
            IOPath.Combine(AppContext.BaseDirectory, "Assets", "Images"),
            IOPath.GetFullPath(IOPath.Combine(AppContext.BaseDirectory, @"..\..\..\", "assets", "imagens")),
            IOPath.GetFullPath(IOPath.Combine(AppContext.BaseDirectory, @"..\..\..\", "Assets", "Images"))
        ];

        foreach (string folder in candidateFolders)
        {
            string fullPath = IOPath.Combine(folder, fileName);
            if (File.Exists(fullPath))
            {
                return fullPath;
            }
        }

        return null;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        LoadLogo();
        ApplyTheme(_themeIndex);
    }

    private void LoadLogo()
    {
        string? logoPath = ResolveAssetPath("Logo.png");
        if (string.IsNullOrEmpty(logoPath))
        {
            LogoMaskRect.Visibility = Visibility.Collapsed;
            LogoFallback.Visibility = Visibility.Visible;
            return;
        }

        BitmapImage source = new(new Uri(logoPath, UriKind.Absolute));
        FormatConvertedBitmap formatted = new(source, PixelFormats.Bgra32, null, 0);
        int width = formatted.PixelWidth;
        int height = formatted.PixelHeight;
        int stride = width * 4;
        byte[] pixels = new byte[height * stride];
        formatted.CopyPixels(pixels, stride, 0);

        for (int i = 0; i < pixels.Length; i += 4)
        {
            byte b = pixels[i];
            byte g = pixels[i + 1];
            byte r = pixels[i + 2];
            byte brightness = (byte)((r + g + b) / 3);
            pixels[i + 3] = (byte)(255 - brightness);
        }

        WriteableBitmap maskBitmap = new(width, height, 96, 96, PixelFormats.Bgra32, null);
        maskBitmap.WritePixels(new Int32Rect(0, 0, width, height), pixels, stride, 0);

        LogoMaskRect.OpacityMask = new ImageBrush(maskBitmap) { Stretch = Stretch.Uniform };
        LogoMaskRect.Visibility = Visibility.Visible;
        LogoFallback.Visibility = Visibility.Collapsed;
    }

    private void LoadBackgroundForTheme()
    {
        string targetFile = _themeIndex == 2 ? "fundo_3.png" : "fundo_1e2.png";
        string? imagePath = ResolveAssetPath(targetFile);

        if (string.IsNullOrEmpty(imagePath))
        {
            BackgroundImage.Source = null;
            return;
        }

        BackgroundImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
    }

    private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ButtonState == MouseButtonState.Pressed)
        {
            DragMove();
        }
    }

    private void MinimizeButton_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
    private void CloseButton_Click(object sender, RoutedEventArgs e) => Close();

    private void TabButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is ToggleButton { Tag: TabItemModel tab })
        {
            SetActiveTab(tab);
        }
    }

    private void ThemeToggleButton_Click(object sender, RoutedEventArgs e)
    {
        ApplyTheme((_themeIndex + 1) % 3);
    }

    // LÓGICA CORRIGIDA DA OTIMIZAÇÃO COMPLETA GLOBAL (Marca/Desmarca TUDO em TODAS AS ABAS)
    private void GlobalOptimizationToggle_Click(object sender, RoutedEventArgs e)
    {
        bool enabled = GlobalOptimizationToggle.IsChecked == true;
        foreach (var tabOptions in _optionsByTab.Values)
        {
            foreach (OptimizationOption option in tabOptions)
            {
                option.IsEnabled = enabled;
            }
        }
    }

    private void MarkAllButton_Click(object sender, RoutedEventArgs e)
    {
        foreach (OptimizationOption option in ActiveOptions)
        {
            option.IsEnabled = true;
        }
        CheckGlobalToggleState();
    }

    private void ExecuteButton_Click(object sender, RoutedEventArgs e)
    {
        int totalSelected = _optionsByTab.Values.Sum(list => list.Count(opt => opt.IsEnabled));
        MessageBox.Show(
            totalSelected == 0
                ? "Nenhuma otimização marcada em nenhuma aba."
                : $"{totalSelected} otimização(ões) selecionada(s) no total pronta(s) para execução em cadeia.",
            "Painel DX",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }

    private void InfoButton_MouseEnter(object sender, MouseEventArgs e) => ShowInfo(sender);
    private void InfoButton_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e) => ShowInfo(sender);
    private void InfoButton_MouseLeave(object sender, MouseEventArgs e) => HideInfoWhenPointerLeaves();
    private void InfoButton_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e) => HideInfoWhenPointerLeaves();

    private void ShowInfo(object sender)
    {
        if (sender is not Button button || button.Tag is not OptimizationOption option)
        {
            return;
        }

        _activeInfoButton = button;
        InfoPopupText.Text = option.Description;
        InfoPopup.PlacementTarget = button;
        InfoPopup.IsOpen = true;
    }

    private void HideInfoWhenPointerLeaves()
    {
        Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
        {
            bool buttonStillActive = _activeInfoButton?.IsMouseOver == true || _activeInfoButton?.IsKeyboardFocusWithin == true;
            if (!buttonStillActive && !InfoPopupBorder.IsMouseOver)
            {
                InfoPopup.IsOpen = false;
            }
        }));
    }

    private void InfoPopupBorder_MouseEnter(object sender, MouseEventArgs e) => InfoPopup.IsOpen = true;
    private void InfoPopupBorder_MouseLeave(object sender, MouseEventArgs e) => HideInfoWhenPointerLeaves();

    public sealed class TabItemModel : INotifyPropertyChanged
    {
        private bool _isActive;

        public TabItemModel(string name, string icon, bool isActive)
        {
            Name = name;
            Icon = icon;
            _isActive = isActive;
        }

        public string Name { get; }
        public string Icon { get; }
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (_isActive == value) return;
                _isActive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsActive)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }

    public sealed class OptimizationOption : INotifyPropertyChanged
    {
        private bool _isEnabled;

        public OptimizationOption(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; }
        public string Description { get; }
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (_isEnabled == value) return;
                _isEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}