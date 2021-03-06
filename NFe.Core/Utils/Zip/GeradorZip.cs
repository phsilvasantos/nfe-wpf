﻿using NFe.Core.Utils.PDF;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using NFe.Core.Cadastro.Configuracoes;
using NFe.Core.NotasFiscais;
using NFe.Core.NotasFiscais.Services;
using NFe.Core.Utils.Xml;
using NFe.Core.Interfaces;


//TODO: Adicionar try catch para hd cheio ou para quando não é possível criar o diretório e etc

namespace NFe.Core.Utils.Zip
{
   public class GeradorZip
   {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IConfiguracaoService _configuracaoService;
        private INotaFiscalRepository _notaFiscalRepository;
        private IEventoService _eventoService;
        private INotaInutilizadaService _notaInutilizadaService;
        private GeradorPDF _geradorPdf;

        public GeradorZip(IConfiguracaoService configuracaoService, IEventoService eventoService, INotaInutilizadaService notaInutilizadaService,
            GeradorPDF geradorPdf, INotaFiscalRepository notaFiscalRepository)
       {
           _configuracaoService = configuracaoService;
            _notaFiscalRepository = notaFiscalRepository;
           _eventoService = eventoService;
           _notaInutilizadaService = notaInutilizadaService;
           _geradorPdf = geradorPdf;
       }

      public Task<string> GerarZipEnvioContabilidadeAsync(DateTime periodo)
      {
         return Task.Run(() =>
         {
            var config = _configuracaoService.GetConfiguracao();

            var notasNoPeriodo = _notaFiscalRepository.GetNotasFiscaisPorMesAno(periodo, config.IsProducao, true);
            var nfeNoPeriodo = notasNoPeriodo.Where(n => n.Modelo.Equals("55")).ToList();
            var nfceNoPeriodo = notasNoPeriodo.Where(n => n.Modelo.Equals("65")).ToList();

            var eventosCancelamentoNFe = _eventoService.GetEventosPorNotasId(nfeNoPeriodo.Select(n => n.Id), true);
            var eventosCancelamentoNFCe = _eventoService.GetEventosPorNotasId(nfceNoPeriodo.Select(n => n.Id), true);

            var nfeXMLs = nfeNoPeriodo.Select(n => new NotaXml(n.Chave, n.LoadXml())).ToList();
            var eventoNfeXMLs = eventosCancelamentoNFe.Select(e => new EventoCancelamentoXml(e.ChaveIdEvento, e.Xml)).ToList();
            var nfceXMLs = nfceNoPeriodo.Select(n => new NotaXml(n.Chave, n.LoadXml())).ToList();
            var eventoNfceXMLs = eventosCancelamentoNFCe.Select(e => new EventoCancelamentoXml(e.ChaveIdEvento, e.Xml)).ToList();

            var notasZip = notasNoPeriodo.ToList();

            var notasInutilizadas = _notaInutilizadaService.GetNotasFiscaisPorMesAno(periodo, config.IsProducao).ToList();

            string startPath = Path.Combine(Path.GetTempPath(), "EmissorNFe");

            try
            {
               if (!Directory.Exists(startPath))
               {
                  Directory.CreateDirectory(startPath);
               }
               else
               {
                  Directory.Delete(startPath, true);
                  Directory.CreateDirectory(startPath);
               }

               string nfeDir = Path.Combine(startPath, "NFe");
               string nfceDir = Path.Combine(startPath, "NFCe");

               string zipPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), @"Notas Fiscais " + periodo.ToString("MM_yyyy") + ".zip");

               if (File.Exists(zipPath))
               {
                  File.Delete(zipPath);
               }

               _geradorPdf.GerarRelatorioGerencial(notasZip, notasInutilizadas, periodo, startPath);
               //Gerar arquivos de notas inutilizadas e adicioná-las ao relatório

               if (GravarXmlsNfe(nfeXMLs, eventoNfeXMLs, nfeDir)
                   && GravarXmlsNfce(nfceXMLs, eventoNfceXMLs, nfceDir)
                   && GerarXmlsNotasInutilizadas(notasInutilizadas, startPath))
               {
                  ZipFile.CreateFromDirectory(startPath, zipPath);
               }

               return zipPath;
            }
            catch (Exception e)
            {
                log.Error(e);
                throw;
            }
             finally
            {
               Directory.Delete(startPath, true);
            }
         });
      }

      private bool GerarXmlsNotasInutilizadas(List<NotaInutilizadaTO> notasInutilizadas, string startPath)
      {
         try
         {
            string inutDir = Path.Combine(startPath, "Inutilizadas");

            if (!Directory.Exists(inutDir))
            {
               Directory.CreateDirectory(inutDir);
            }

            string nfeDir = Path.Combine(inutDir, "NFe");

            if (!Directory.Exists(nfeDir))
            {
               Directory.CreateDirectory(nfeDir);
            }

            string nfceDir = Path.Combine(inutDir, "NFCe");

            if (!Directory.Exists(nfceDir))
            {
               Directory.CreateDirectory(nfceDir);
            }

            var nfeInutilizadas = notasInutilizadas.Where(n => n.Modelo == 55);
            var nfceInutilizadas = notasInutilizadas.Where(n => n.Modelo == 65);

            foreach (var inutilizada in nfeInutilizadas)
            {
               using (FileStream stream = File.Create(Path.Combine(nfeDir, inutilizada.IdInutilizacao + "-procInutNFe.xml")))
               {
                  using (StreamWriter writer = new StreamWriter(stream))
                  {
                     writer.WriteLine(inutilizada.LoadXml());
                  }
               }
            }

            foreach (var inutilizada in nfceInutilizadas)
            {
               using (FileStream stream = File.Create(Path.Combine(nfceDir, inutilizada.IdInutilizacao + "-procInutNFe.xml")))
               {
                  using (StreamWriter writer = new StreamWriter(stream))
                  {
                     writer.WriteLine(inutilizada.LoadXml());
                  }
               }
            }

            return true;
         }
         catch (Exception e)
         {
            log.Error(e);
            return false;
         }
      }

      private bool GravarXmlsNfe(List<NotaXml> xmlNfeEnviadas, List<EventoCancelamentoXml> xmlNfeEventoCancelamento, string nfeDir)
      {
         if (!Directory.Exists(nfeDir))
         {
            Directory.CreateDirectory(nfeDir);
         }

         foreach (var nfe in xmlNfeEnviadas)
         {
            using (FileStream stream = File.Create(Path.Combine(nfeDir, nfe.Chave + "-nfe.xml")))
            {
               using (StreamWriter writer = new StreamWriter(stream))
               {
                  writer.WriteLine(nfe.Xml);
               }
            }
         }

         foreach (var evento in xmlNfeEventoCancelamento)
         {
            using (FileStream stream = File.Create(Path.Combine(nfeDir, evento.Id + "-procEventoNFe.xml")))
            {
               using (StreamWriter writer = new StreamWriter(stream))
               {
                  writer.WriteLine(evento.Xml);
               }
            }
         }

         return true;
      }

      private bool GravarXmlsNfce(List<NotaXml> xmlNfceEnviadas, List<EventoCancelamentoXml> xmlNfceEventoCancelamento, string nfceDir)
      {
         if (!Directory.Exists(nfceDir))
         {
            Directory.CreateDirectory(nfceDir);
         }

         foreach (var p in xmlNfceEnviadas)
         {
            using (FileStream stream = File.Create(Path.Combine(nfceDir, p.Chave + "-nfce.xml")))
            {
               using (StreamWriter writer = new StreamWriter(stream))
               {
                  writer.WriteLine(p.Xml);
               }
            }
         }

         foreach (var evento in xmlNfceEventoCancelamento)
         {
            using (FileStream stream = File.Create(Path.Combine(nfceDir, evento.Id + "-procEventoNFe.xml")))
            {
               using (StreamWriter writer = new StreamWriter(stream))
               {
                  writer.WriteLine(evento.Xml);
               }
            }
         }

         return true;
      }
   }

   public class NotaXml
   {
      public NotaXml(string chave, string xml)
      {
         Chave = chave;
         Xml = xml;
      }

      public string Chave { get; set; }
      public string Xml { get; set; }
   }

   public class EventoCancelamentoXml
   {
      public EventoCancelamentoXml(string id, string xml)
      {
         Id = id;
         Xml = xml;
      }

      public string Id { get; set; }
      public string Xml { get; set; }
   }
}
