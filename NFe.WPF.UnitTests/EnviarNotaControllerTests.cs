﻿using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Views;
using Moq;
using NFe.Core.Cadastro.Configuracoes;
using NFe.Core.Cadastro.Emissor;
using NFe.Core.Cadastro.Produto;
using NFe.Core.Entitities;
using NFe.Core.NotasFiscais;
using NFe.Core.NotasFiscais.Services;
using Xunit;
using Emissor = NFe.Core.NotasFiscais.Emissor;

namespace NFe.WPF.UnitTests
{
    public class EnviarNotaControllerTests : IClassFixture<NotaFiscalFixture>
    {
        private readonly NotaFiscalFixture _notaFiscalFixture;

        public EnviarNotaControllerTests(NotaFiscalFixture notaFiscalFixture)
        {
            _notaFiscalFixture = notaFiscalFixture;
        }

        [Fact]
        public void NFCeModel_EnviarNota_Sucesso()
        {
            // Arrange

            var configuracaoServiceMock = new Mock<IConfiguracaoService>();
            configuracaoServiceMock
                .Setup(m => m.GetConfiguracao())
                .Returns(new ConfiguracaoEntity());

            var emissorServiceMock = new Mock<IEmissorService>();
            emissorServiceMock
                .Setup(m => m.GetEmissor())
                .Returns(new Emissor(string.Empty, string.Empty, "98586321444578", string.Empty, string.Empty, string.Empty,
                    "Regime Normal",
                    new Endereco(string.Empty, string.Empty, string.Empty, "BRASILIA", string.Empty, "DF"),
                    string.Empty));

            var produtoServiceMock = new Mock<IProdutoService>();
            produtoServiceMock
                .Setup(m => m.GetAll())
                .Returns(new List<ProdutoEntity>()
                {
                    new ProdutoEntity()
                    {
                        Id = 1,
                        ValorUnitario = 65,
                        Codigo = "0001",
                        Descricao = "Botijão P13",
                        GrupoImpostos = new GrupoImpostos()
                        {
                            Id = 1,
                            CFOP = "5656",
                            Descricao = "Gás Venda",
                            Impostos = _notaFiscalFixture.Impostos
                        },
                        GrupoImpostosId = 1,
                        NCM = "27111910",
                        UnidadeComercial = "UN"
                    }
                });

            var dialogService = new Mock<IDialogService>().Object;
            var notaFiscalService = new Mock<IEnviaNotaFiscalService>().Object;
            var configuracaoService = configuracaoServiceMock.Object;
            var emissorService = emissorServiceMock.Object;
            var produtoService = produtoServiceMock.Object;



            var enviarNotaController = new NotaFiscal.ViewModel.EnviarNotaController(dialogService, notaFiscalService,
                configuracaoService, emissorService, produtoService);

            // Act

            enviarNotaController.EnviarNota(_notaFiscalFixture.NFCeModel, Modelo.Modelo65).Wait();
        }

        [Fact]
        public void NFeModel_EnviarNota_Sucesso()
        {
            // Arrange

            var configuracaoServiceMock = new Mock<IConfiguracaoService>();
            configuracaoServiceMock
                .Setup(m => m.GetConfiguracao())
                .Returns(new ConfiguracaoEntity());

            var emissorServiceMock = new Mock<IEmissorService>();
            emissorServiceMock
                .Setup(m => m.GetEmissor())
                .Returns(new Emissor(string.Empty, string.Empty, "98586321444578", string.Empty, string.Empty, string.Empty,
                    "Regime Normal",
                    new Endereco(string.Empty, string.Empty, string.Empty, "BRASILIA", string.Empty, "DF"),
                    string.Empty));

            var produtoServiceMock = new Mock<IProdutoService>();
            produtoServiceMock
                .Setup(m => m.GetAll())
                .Returns(new List<ProdutoEntity>()
                {
                    new ProdutoEntity()
                    {
                        Id = 1,
                        ValorUnitario = 65,
                        Codigo = "0001",
                        Descricao = "Botijão P13",
                        GrupoImpostos = new GrupoImpostos()
                        {
                            Id = 1,
                            CFOP = "5656",
                            Descricao = "Gás Venda",
                            Impostos = _notaFiscalFixture.Impostos
                        },
                        GrupoImpostosId = 1,
                        NCM = "27111910",
                        UnidadeComercial = "UN"
                    }
                });

            var dialogService = new Mock<IDialogService>().Object;
            var notaFiscalService = new Mock<IEnviaNotaFiscalService>().Object;
            var configuracaoService = configuracaoServiceMock.Object;
            var emissorService = emissorServiceMock.Object;
            var produtoService = produtoServiceMock.Object;

            var enviarNotaController = new NotaFiscal.ViewModel.EnviarNotaController(dialogService, notaFiscalService,
                configuracaoService, emissorService, produtoService);

            // Act

            enviarNotaController.EnviarNota(_notaFiscalFixture.NFeModel, Modelo.Modelo55).Wait();
        }

        [Fact]
        public async System.Threading.Tasks.Task NFeModel_EnviarNota_ArgumentExceptionValorTotalInválido()
        {
            // Arrange

            var configuracaoServiceMock = new Mock<IConfiguracaoService>();
            configuracaoServiceMock
                .Setup(m => m.GetConfiguracao())
                .Returns(new ConfiguracaoEntity());

            var emissorServiceMock = new Mock<IEmissorService>();
            emissorServiceMock
                .Setup(m => m.GetEmissor())
                .Returns(new Emissor(string.Empty, string.Empty, "98586321444578", string.Empty, string.Empty, string.Empty,
                    "Regime Normal",
                    new Endereco(string.Empty, string.Empty, string.Empty, "BRASILIA", string.Empty, "DF"),
                    string.Empty));

            var produtoServiceMock = new Mock<IProdutoService>();
            produtoServiceMock
                .Setup(m => m.GetAll())
                .Returns(new List<ProdutoEntity>()
                {
                    new ProdutoEntity()
                    {
                        Id = 1,
                        ValorUnitario = 65,
                        Codigo = "0001",
                        Descricao = "Botijão P13",
                        GrupoImpostos = new GrupoImpostos()
                        {
                            Id = 1,
                            CFOP = "5656",
                            Descricao = "Gás Venda",
                            Impostos = _notaFiscalFixture.Impostos
                        },
                        GrupoImpostosId = 1,
                        NCM = "27111910",
                        UnidadeComercial = "UN"
                    }
                });

            var dialogService = new Mock<IDialogService>().Object;
            var notaFiscalService = new Mock<IEnviaNotaFiscalService>().Object;
            var configuracaoService = configuracaoServiceMock.Object;
            var emissorService = emissorServiceMock.Object;
            var produtoService = produtoServiceMock.Object;

            var enviarNotaController = new NotaFiscal.ViewModel.EnviarNotaController(dialogService, notaFiscalService,
                configuracaoService, emissorService, produtoService);

            // Act

            await Assert.ThrowsAnyAsync<ArgumentException>(() => enviarNotaController.EnviarNota(_notaFiscalFixture.NFeTotalInvalido, Modelo.Modelo55));
        }
    }
}
