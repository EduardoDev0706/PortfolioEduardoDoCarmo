# AgendaEletronica

Projeto simples de agenda eletrônica desenvolvido em C# com Avalonia UI.  
Permite adicionar, listar e remover compromissos com título, descrição e data.

---

## Tecnologias Utilizadas

- **C#** (.NET 6+)
- **Avalonia UI** para interface gráfica multiplataforma
- **MVVM** (Model-View-ViewModel) para arquitetura do projeto
- **ObservableCollection** para gerenciamento dinâmico da lista de compromissos
- **Comandos (ICommand)** para ações na interface

---

## Funcionalidades

- Adicionar compromissos com título, descrição e data.
- Listar compromissos cadastrados.
- Remover compromissos diretamente na lista.
- Persistência simples dos dados via serviço local (arquivo JSON).

---

## Estrutura do Projeto

- `Views/` — arquivos .axaml e code-behind da interface.
- `ViewModels/` — lógica da aplicação, propriedades para data binding e comandos.
- `Models/` — classes que representam dados (Compromisso).
- `Data/` — serviço para carregar e salvar compromissos.
- `Commands/` — implementação de comandos para a interface (RelayCommand).