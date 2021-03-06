﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using EmissorNFe.Produto;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NFe.Core.Cadastro.Imposto;
using NFe.Core.Cadastro.Produto;
using NFe.Core.Entitities;
using NFe.WPF.ViewModel.Services;

namespace NFe.WPF.ViewModel
{
    public delegate void ProdutoAdicionadoEventHandler();

    public class ProdutoViewModel : ViewModelBase
    {
        private GrupoImpostos _imposto;
        private readonly ImpostoService _impostoService;
        private readonly IProdutoService _produtoService;

        public event ProdutoAdicionadoEventHandler ProdutoAdicionadoEvent = delegate { };
        public int Id { get; set; }
        public string CodigoItem { get; set; }
        public string Descricao { get; set; }
        public string UnidadeComercial { get; set; }
        public GrupoImpostos Imposto
        {
            get { return _imposto; }
            set
            {
                _imposto = value;
                RaisePropertyChanged("Imposto");
            }
        }
        public double ValorUnitario { get; set; }
        public string NCM { get; set; }
        public List<string> UnidadesComerciais { get; set; }
        public ObservableCollection<GrupoImpostos> Impostos { get; set; }

        public ICommand AlterarProdutoCmd { get; set; }
        public ICommand LoadedCmd { get; set; }
        public ICommand SalvarCmd { get; set; }
        public ICommand CancelarCmd { set; get; }

        public ProdutoViewModel(IProdutoService produtoService, ImpostoService impostoService)
        {
            UnidadesComerciais = new List<string>() { "UN" };

            AlterarProdutoCmd = new RelayCommand<string>(AlterarProduto_Execute, null);
            SalvarCmd = new RelayCommand<IClosable>(SalvarCmd_Execute, null);
            CancelarCmd = new RelayCommand<object>(CancelarCmd_Execute, null);
            LoadedCmd = new RelayCommand(LoadedCmd_Execute, null);
            _produtoService = produtoService;
            _impostoService = impostoService;
        }

        private void AlterarProduto_Execute(string produtoCodigo)
        {
            var produto = _produtoService.GetByCodigo(produtoCodigo);

            Id = produto.Id;
            CodigoItem = produto.Codigo;
            Descricao = produto.Descricao;
            UnidadeComercial = produto.UnidadeComercial;
            Imposto = produto.GrupoImpostos;
            ValorUnitario = produto.ValorUnitario;
            NCM = produto.NCM;

            var app = Application.Current;
            var mainWindow = app.MainWindow;

            new CadastroProdutoWindow() { Owner = mainWindow }.ShowDialog();
        }

        private void LoadedCmd_Execute()
        {
            var impostosDb = _impostoService.GetAll();
            Impostos = new ObservableCollection<GrupoImpostos>();

            foreach (var impostoDb in impostosDb)
            {
                var impostoModel = impostoDb;
                Impostos.Add(impostoModel);
            }

            RaisePropertyChanged("Impostos");

            if(Imposto != null)
            {
                Imposto = Impostos.FirstOrDefault(i => i.Id.Equals(Imposto.Id));
            }
        }

        private void CancelarCmd_Execute(object obj)
        {
            throw new NotImplementedException();
        }

        private void SalvarCmd_Execute(IClosable window)
        {
            var produto = _produtoService.GetByCodigo(CodigoItem) ?? new ProdutoEntity();

            produto.Id = Id;
            produto.Codigo = CodigoItem;
            produto.Descricao = Descricao;
            produto.NCM = NCM;
            produto.UnidadeComercial = UnidadeComercial;
            produto.ValorUnitario = ValorUnitario;
            produto.GrupoImpostosId = Imposto.Id;

            _produtoService.Salvar(produto);
            ProdutoAdicionadoEvent();
            window.Close();
        }
    }
}
