﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NFe.Core.NFeRecepcaoEvento4 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4", ConfigurationName="NFeRecepcaoEvento4.NFeRecepcaoEvento4Soap")]
    public interface NFeRecepcaoEvento4Soap {
        
        // CODEGEN: Generating message contract since the operation nfeRecepcaoEvento is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(Action="http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4/nfeRecepcaoEvento", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        NFe.Core.NFeRecepcaoEvento4.nfeRecepcaoEventoResponse nfeRecepcaoEvento(NFe.Core.NFeRecepcaoEvento4.nfeRecepcaoEventoRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4/nfeRecepcaoEvento", ReplyAction="*")]
        System.Threading.Tasks.Task<NFe.Core.NFeRecepcaoEvento4.nfeRecepcaoEventoResponse> nfeRecepcaoEventoAsync(NFe.Core.NFeRecepcaoEvento4.nfeRecepcaoEventoRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class nfeRecepcaoEventoRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4", Order=0)]
        public System.Xml.XmlNode nfeDadosMsg;
        
        public nfeRecepcaoEventoRequest() {
        }
        
        public nfeRecepcaoEventoRequest(System.Xml.XmlNode nfeDadosMsg) {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class nfeRecepcaoEventoResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.portalfiscal.inf.br/nfe/wsdl/NFeRecepcaoEvento4", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true)]
        public System.Xml.XmlNode nfeResultMsg;
        
        public nfeRecepcaoEventoResponse() {
        }
        
        public nfeRecepcaoEventoResponse(System.Xml.XmlNode nfeResultMsg) {
            this.nfeResultMsg = nfeResultMsg;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface NFeRecepcaoEvento4SoapChannel : NFe.Core.NFeRecepcaoEvento4.NFeRecepcaoEvento4Soap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class NFeRecepcaoEvento4SoapClient : System.ServiceModel.ClientBase<NFe.Core.NFeRecepcaoEvento4.NFeRecepcaoEvento4Soap>, NFe.Core.NFeRecepcaoEvento4.NFeRecepcaoEvento4Soap {
        
        public NFeRecepcaoEvento4SoapClient() {
        }
        
        public NFeRecepcaoEvento4SoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public NFeRecepcaoEvento4SoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NFeRecepcaoEvento4SoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NFeRecepcaoEvento4SoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        NFe.Core.NFeRecepcaoEvento4.nfeRecepcaoEventoResponse NFe.Core.NFeRecepcaoEvento4.NFeRecepcaoEvento4Soap.nfeRecepcaoEvento(NFe.Core.NFeRecepcaoEvento4.nfeRecepcaoEventoRequest request) {
            return base.Channel.nfeRecepcaoEvento(request);
        }
        
        public System.Xml.XmlNode nfeRecepcaoEvento(System.Xml.XmlNode nfeDadosMsg) {
            NFe.Core.NFeRecepcaoEvento4.nfeRecepcaoEventoRequest inValue = new NFe.Core.NFeRecepcaoEvento4.nfeRecepcaoEventoRequest();
            inValue.nfeDadosMsg = nfeDadosMsg;
            NFe.Core.NFeRecepcaoEvento4.nfeRecepcaoEventoResponse retVal = ((NFe.Core.NFeRecepcaoEvento4.NFeRecepcaoEvento4Soap)(this)).nfeRecepcaoEvento(inValue);
            return retVal.nfeResultMsg;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<NFe.Core.NFeRecepcaoEvento4.nfeRecepcaoEventoResponse> NFe.Core.NFeRecepcaoEvento4.NFeRecepcaoEvento4Soap.nfeRecepcaoEventoAsync(NFe.Core.NFeRecepcaoEvento4.nfeRecepcaoEventoRequest request) {
            return base.Channel.nfeRecepcaoEventoAsync(request);
        }
        
        public System.Threading.Tasks.Task<NFe.Core.NFeRecepcaoEvento4.nfeRecepcaoEventoResponse> nfeRecepcaoEventoAsync(System.Xml.XmlNode nfeDadosMsg) {
            NFe.Core.NFeRecepcaoEvento4.nfeRecepcaoEventoRequest inValue = new NFe.Core.NFeRecepcaoEvento4.nfeRecepcaoEventoRequest();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return ((NFe.Core.NFeRecepcaoEvento4.NFeRecepcaoEvento4Soap)(this)).nfeRecepcaoEventoAsync(inValue);
        }
    }
}
