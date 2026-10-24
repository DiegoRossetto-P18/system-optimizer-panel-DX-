# System Optimizer & Performance Panel 🚀

Aplicação desktop moderna desenvolvida em **C# WPF** (.NET 8) para automação, otimização de desempenho do sistema operacional e ajustes avançados de periféricos, controlada por um painel centralizado.

## 📌 Sobre o Projeto

O **System Optimizer Panel** é um software modular de otimização para o Windows que combina um backend robusto em C# com rotinas de automação avançadas (scripts `.bat`/`.cmd` e modificações de sistema). 

O sistema executa scripts mestre em segundo plano com privilégios administrativos elevados, gerando relatórios de log detalhados em tempo real para garantir total transparência nas alterações de desempenho.

## 🛠️ Funcionalidades Principais

- **Painel Desktop (WPF):** Interface gráfica limpa e responsiva para acionamento de rotinas com 1-Clique.
- **Automação de Otimização Mestre:** Execução sequencial de scripts para limpeza de temporários, ajuste de rede/DNS, otimização de armazenamento e gerenciamento de serviços.
- **Sistema de Logs Dinâmico:** Gravação automática de eventos e progresso detalhado em `otimizacao_log.txt`.
- **Elevação de Privilégios (Admin):** Gerenciamento integrado de permissões do Windows para alteração segura de chaves de registro e políticas de rede.

## 🚀 Tecnologias Utilizadas

- **Backend & Interface:** C#, .NET 8, Windows Presentation Foundation (WPF)
- **Automação & Scripts:** Windows Batch Scripting (`.bat`/`.cmd`), Windows Registry
- **Controle de Versão:** Git & GitHub

## 📁 Estrutura do Repositório

```text
painel-otimizacao/
├── bin/
│   ├── cleanup/         # Scripts de limpeza de cache e logs do sistema
│   ├── network/         # Otimizações de TCP/IP, DNS e throttling de rede
│   ├── registry/        # Ajustes de registro para jogos e desempenho
│   ├── services/        # Gerenciamento de serviços e telemetria
│   ├── storage/         # Configurações de memória virtual e NTFS
│   ├── system/          # Planos de energia e timers do Windows
│   ├── otimizacao-completa.bat  # Script mestre de automação
│   └── otimizacao_log.txt       # Arquivo de log gerado pelas rotinas
├── App.xaml / MainWindow.xaml   # Arquitetura e telas da aplicação WPF
├── PainelOtimizacao.csproj      # Configuração do projeto .NET
└── README.md                    # Documentação do projeto
```

## ⚙️ Como Executar Localmente

1. Clone o repositório:
git clone https://github.com/DiegoRossetto-P18/system-optimizer-panel-DX-.git

2. Acesse a pasta do projeto:
cd system-optimizer-panel-DX-/painel-otimizacao

3. Execute a aplicação através do terminal utilizando o .NET SDK:
dotnet run

(Nota: Para que as otimizações do sistema funcionem corretamente, execute o terminal ou o aplicativo compilado como Administrador).

## 📄 Licença e Direitos Autorais

© Diego Rossetto Pereira. Todos os direitos reservados.

Este software possui restrições de uso comercial e redistribuição. O código-fonte presente neste repositório é disponibilizado 
exclusivamente para fins de exibição de portfólio técnico e arquitetura de software.
