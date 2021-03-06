﻿using NFe.Core.XmlSchemas.NfeAutorizacao.Envio;
using System;
using System.Collections.Generic;
using System.Text;
using NFe.Core.NotasFiscais;

namespace NFe.Core.Utils.Conversores.Enums.Autorizacao
{
    public static class TCodUfIBGEConversor
    {
        public static TCodUfIBGE ToTCodUfIBGE(CodigoUfIbge codigo)
        {
            switch (codigo)
            {
                case CodigoUfIbge.DF:
                    return TCodUfIBGE.Item53;

                default:
                    throw new ArgumentException();
            }
        }
    }
}
