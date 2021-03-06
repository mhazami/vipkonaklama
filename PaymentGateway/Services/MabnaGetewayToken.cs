﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

// 
// This source code was auto-generated by wsdl, Version=4.6.81.0.
// 

public class MabnaGetewayToken
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.81.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "TokenServicePortBinding",
        Namespace = "http://token.ws.web.cnpg/")]
    public partial class Token : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback reservationOperationCompleted;

        /// <remarks/>
        public Token()
        {
            this.Url = "https://mabna.shaparak.ir:443/TokenService";
        }

        /// <remarks/>
        public event reservationCompletedEventHandler reservationCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace = "http://token.ws.web.cnpg/",
            ResponseNamespace = "http://token.ws.web.cnpg/",
            Use = System.Web.Services.Description.SoapBindingUse.Literal,
            ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return:
            System.Xml.Serialization.XmlElementAttribute("return", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public tokenResponse reservation(
            [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)] tokenDTO
                Token_param)
        {
            object[] results = this.Invoke("reservation", new object[]
            {
                Token_param
            });
            return ((tokenResponse) (results[0]));
        }

        /// <remarks/>
        public System.IAsyncResult Beginreservation(tokenDTO Token_param, System.AsyncCallback callback,
            object asyncState)
        {
            return this.BeginInvoke("reservation", new object[]
            {
                Token_param
            }, callback, asyncState);
        }

        /// <remarks/>
        public tokenResponse Endreservation(System.IAsyncResult asyncResult)
        {
            object[] results = this.EndInvoke(asyncResult);
            return ((tokenResponse) (results[0]));
        }

        /// <remarks/>
        public void reservationAsync(tokenDTO Token_param)
        {
            this.reservationAsync(Token_param, null);
        }

        /// <remarks/>
        public void reservationAsync(tokenDTO Token_param, object userState)
        {
            if ((this.reservationOperationCompleted == null))
            {
                this.reservationOperationCompleted =
                    new System.Threading.SendOrPostCallback(this.OnreservationOperationCompleted);
            }
            this.InvokeAsync("reservation", new object[]
            {
                Token_param
            }, this.reservationOperationCompleted, userState);
        }

        private void OnreservationOperationCompleted(object arg)
        {
            if ((this.reservationCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs =
                    ((System.Web.Services.Protocols.InvokeCompletedEventArgs) (arg));
                this.reservationCompleted(this,
                    new reservationCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled,
                        invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.81.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://token.ws.web.cnpg/")]
    public partial class tokenDTO
    {

        private string aMOUNTField;

        private string cRNField;

        private string mIDField;

        private string rEFERALADRESSField;

        private string sIGNATUREField;

        private string tIDField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AMOUNT
        {
            get { return this.aMOUNTField; }
            set { this.aMOUNTField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CRN
        {
            get { return this.cRNField; }
            set { this.cRNField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string MID
        {
            get { return this.mIDField; }
            set { this.mIDField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string REFERALADRESS
        {
            get { return this.rEFERALADRESSField; }
            set { this.rEFERALADRESSField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string SIGNATURE
        {
            get { return this.sIGNATUREField; }
            set { this.sIGNATUREField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TID
        {
            get { return this.tIDField; }
            set { this.tIDField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.81.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://token.ws.web.cnpg/")]
    public partial class tokenResponse
    {

        private int resultField;

        private string signatureField;

        private string tokenField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public int result
        {
            get { return this.resultField; }
            set { this.resultField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string signature
        {
            get { return this.signatureField; }
            set { this.signatureField = value; }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string token
        {
            get { return this.tokenField; }
            set { this.tokenField = value; }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.81.0")]
    public delegate void reservationCompletedEventHandler(object sender, reservationCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.81.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class reservationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal reservationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled,
            object userState) :
                base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public tokenResponse Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((tokenResponse) (this.results[0]));
            }
        }
    }
}