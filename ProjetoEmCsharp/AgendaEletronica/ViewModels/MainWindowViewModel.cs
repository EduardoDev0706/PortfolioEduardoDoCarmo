using AgendaEletronica.Data;
using AgendaEletronica.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;


namespace AgendaEletronica.ViewModels
{

    public partial class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly AgendaService _servico = new();

        private string _titulo;
        public string Titulo
        {
            get => _titulo;
            set
            {
                _titulo = value;
                OnPropertyChanged();
            }
        }

        private string _descricao;
        public string Descricao
        {
            get => _descricao;
            set
            {
                _descricao = value;
                OnPropertyChanged();
            }
        }

        private DateTime _data = DateTime.Today;
        public DateTime Data
        {
            get => _data;
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Compromisso> Compromissos { get; set; }

        public ICommand AdicionarCommand { get; }
        public ICommand RemoverCommand { get; }

        public MainWindowViewModel()
        {
            Compromissos = new ObservableCollection<Compromisso>(_servico.Carregar());
            AdicionarCommand = new RelayCommand(Adicionar);
            RemoverCommand = new RelayCommand<Compromisso>(Remover);
        }

        private void Adicionar()
        {
            if (string.IsNullOrWhiteSpace(Titulo))
                return;

            var novo = new Compromisso
            {
                Titulo = this.Titulo,
                Descricao = this.Descricao,
                Data = this.Data
            };

            Compromissos.Add(novo);
            _servico.Salvar(new List<Compromisso>(Compromissos));

            Titulo = string.Empty;
            Descricao = string.Empty;
            Data = DateTime.Today;
        }

        private void Remover(Compromisso compromisso)
        {
            if (compromisso == null)
                return;

            Compromissos.Remove(compromisso);
            _servico.Salvar(new List<Compromisso>(Compromissos));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}








