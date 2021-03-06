﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NFe.Core.Entitities;
using NFe.Core.NotasFiscais;
using NFe.Core.NotasFiscais.Sefaz.NfeAutorizacao;

namespace NFe.Core.Interfaces
{
    public interface INotaFiscalRepository
    {
        int Salvar(NotaFiscalEntity notafiscal);
        List<NotaFiscalEntity> GetAll();
        NotaFiscalEntity GetNotaFiscalById(int idNotaFiscalDb, bool isLoadXmlData);
        NotaFiscalEntity GetNotaFiscalByChave(string chave, int ambiente);
        List<NotaFiscalEntity> GetNotasContingencia();
        NotaFiscalEntity GetPrimeiraNotaEmitidaEmContingencia(DateTime dataHoraContingencia, DateTime now);
        NotaFiscalEntity GetNota(string numero, string serie, string modelo);
        IEnumerable<NotaFiscalEntity> Take(int quantity, int page);
        void ExcluirNota(string chave, Ambiente ambiente);
        int SalvarNotaFiscalPendente(NotaFiscal notaFiscal, string v, Ambiente ambiente);
        List<NotaFiscalEntity> GetNotasFiscaisPorMesAno(DateTime periodo, bool isProducao, bool v);
        NotaFiscalEntity GetNotaFiscalByChave(string chaveNFe);
        int Salvar(NotaFiscalEntity notaFiscalEntity, string p);
        NotaFiscal GetNotaFiscalFromNfeProcXml(string xml);
        List<NotaFiscal> GetNotasFiscaisPorPeriodo(DateTime periodoInicial, DateTime dateTime, bool v);
        List<NotaFiscalEntity> GetNotasPendentes(bool isLoadXmlData);
    }
}