﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IranSoftjo.Package.WebUi.Android.SmsBartarServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:SmsWebService", ConfigurationName="SmsBartarServiceReference.SmsWebServicePort")]
    public interface SmsWebServicePort {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:SmsWebServiceAction", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="sendResponse")]
        int send(string username, string password, string to, string msg, string from, int time);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:SmsWebServiceAction", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="sendResponse")]
        System.Threading.Tasks.Task<int> sendAsync(string username, string password, string to, string msg, string from, int time);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:SmsWebServiceAction", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="sendToManyResponse")]
        int[] sendToMany(string username, string password, string[] to, string msg, string from, int time);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:SmsWebServiceAction", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="sendToManyResponse")]
        System.Threading.Tasks.Task<int[]> sendToManyAsync(string username, string password, string[] to, string msg, string from, int time);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:SmsWebServiceAction", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="sendOneToOneResponse")]
        int[] sendOneToOne(string username, string password, string[] to, string[] msg, string[] from, int[] time);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:SmsWebServiceAction", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="sendOneToOneResponse")]
        System.Threading.Tasks.Task<int[]> sendOneToOneAsync(string username, string password, string[] to, string[] msg, string[] from, int[] time);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:SmsWebServiceAction", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="sendToAllResponse")]
        int[] sendToAll(string username, string password, string to, string msg, string from, int time);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:SmsWebServiceAction", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="sendToAllResponse")]
        System.Threading.Tasks.Task<int[]> sendToAllAsync(string username, string password, string to, string msg, string from, int time);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:SmsWebServiceAction", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="deliveryStatusResponse")]
        int deliveryStatus(string username, string password, int recipientId);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:SmsWebServiceAction", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="deliveryStatusResponse")]
        System.Threading.Tasks.Task<int> deliveryStatusAsync(string username, string password, int recipientId);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:SmsWebServiceAction", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="verifyReceiveResponse")]
        bool verifyReceive(string username, string password, string ticket);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:SmsWebServiceAction", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="verifyReceiveResponse")]
        System.Threading.Tasks.Task<bool> verifyReceiveAsync(string username, string password, string ticket);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:SmsWebServiceAction", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="accountInfoResponse")]
        IranSoftjo.Package.WebUi.Android.SmsBartarServiceReference.AccountInfo accountInfo(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:SmsWebServiceAction", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="accountInfoResponse")]
        System.Threading.Tasks.Task<IranSoftjo.Package.WebUi.Android.SmsBartarServiceReference.AccountInfo> accountInfoAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:SmsWebServiceAction", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="changePasswordResponse")]
        string changePassword(string username, string password, string newPassword);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:SmsWebServiceAction", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="changePasswordResponse")]
        System.Threading.Tasks.Task<string> changePasswordAsync(string username, string password, string newPassword);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:SmsWebServiceAction", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(Style=System.ServiceModel.OperationFormatStyle.Rpc, SupportFaults=true, Use=System.ServiceModel.OperationFormatUse.Encoded)]
        [return: System.ServiceModel.MessageParameterAttribute(Name="changeTrafficRelayResponse")]
        string changeTrafficRelay(string username, string password, string newURL);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:SmsWebServiceAction", ReplyAction="*")]
        [return: System.ServiceModel.MessageParameterAttribute(Name="changeTrafficRelayResponse")]
        System.Threading.Tasks.Task<string> changeTrafficRelayAsync(string username, string password, string newURL);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.81.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.SoapTypeAttribute(Namespace="urn:SmsWebService")]
    public partial class AccountInfo : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string[] numbersField;
        
        private string defaultNumberField;
        
        private string receiveUrlField;
        
        private int sentField;
        
        private int receivedField;
        
        private int creditField;
        
        private int remainingField;
        
        /// <remarks/>
        public string[] numbers {
            get {
                return this.numbersField;
            }
            set {
                this.numbersField = value;
                this.RaisePropertyChanged("numbers");
            }
        }
        
        /// <remarks/>
        public string defaultNumber {
            get {
                return this.defaultNumberField;
            }
            set {
                this.defaultNumberField = value;
                this.RaisePropertyChanged("defaultNumber");
            }
        }
        
        /// <remarks/>
        public string receiveUrl {
            get {
                return this.receiveUrlField;
            }
            set {
                this.receiveUrlField = value;
                this.RaisePropertyChanged("receiveUrl");
            }
        }
        
        /// <remarks/>
        public int sent {
            get {
                return this.sentField;
            }
            set {
                this.sentField = value;
                this.RaisePropertyChanged("sent");
            }
        }
        
        /// <remarks/>
        public int received {
            get {
                return this.receivedField;
            }
            set {
                this.receivedField = value;
                this.RaisePropertyChanged("received");
            }
        }
        
        /// <remarks/>
        public int credit {
            get {
                return this.creditField;
            }
            set {
                this.creditField = value;
                this.RaisePropertyChanged("credit");
            }
        }
        
        /// <remarks/>
        public int remaining {
            get {
                return this.remainingField;
            }
            set {
                this.remainingField = value;
                this.RaisePropertyChanged("remaining");
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SmsWebServicePortChannel : IranSoftjo.Package.WebUi.Android.SmsBartarServiceReference.SmsWebServicePort, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SmsWebServicePortClient : System.ServiceModel.ClientBase<IranSoftjo.Package.WebUi.Android.SmsBartarServiceReference.SmsWebServicePort>, IranSoftjo.Package.WebUi.Android.SmsBartarServiceReference.SmsWebServicePort {
        
        public SmsWebServicePortClient() {
        }
        
        public SmsWebServicePortClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SmsWebServicePortClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SmsWebServicePortClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SmsWebServicePortClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int send(string username, string password, string to, string msg, string from, int time) {
            return base.Channel.send(username, password, to, msg, from, time);
        }
        
        public System.Threading.Tasks.Task<int> sendAsync(string username, string password, string to, string msg, string from, int time) {
            return base.Channel.sendAsync(username, password, to, msg, from, time);
        }
        
        public int[] sendToMany(string username, string password, string[] to, string msg, string from, int time) {
            return base.Channel.sendToMany(username, password, to, msg, from, time);
        }
        
        public System.Threading.Tasks.Task<int[]> sendToManyAsync(string username, string password, string[] to, string msg, string from, int time) {
            return base.Channel.sendToManyAsync(username, password, to, msg, from, time);
        }
        
        public int[] sendOneToOne(string username, string password, string[] to, string[] msg, string[] from, int[] time) {
            return base.Channel.sendOneToOne(username, password, to, msg, from, time);
        }
        
        public System.Threading.Tasks.Task<int[]> sendOneToOneAsync(string username, string password, string[] to, string[] msg, string[] from, int[] time) {
            return base.Channel.sendOneToOneAsync(username, password, to, msg, from, time);
        }
        
        public int[] sendToAll(string username, string password, string to, string msg, string from, int time) {
            return base.Channel.sendToAll(username, password, to, msg, from, time);
        }
        
        public System.Threading.Tasks.Task<int[]> sendToAllAsync(string username, string password, string to, string msg, string from, int time) {
            return base.Channel.sendToAllAsync(username, password, to, msg, from, time);
        }
        
        public int deliveryStatus(string username, string password, int recipientId) {
            return base.Channel.deliveryStatus(username, password, recipientId);
        }
        
        public System.Threading.Tasks.Task<int> deliveryStatusAsync(string username, string password, int recipientId) {
            return base.Channel.deliveryStatusAsync(username, password, recipientId);
        }
        
        public bool verifyReceive(string username, string password, string ticket) {
            return base.Channel.verifyReceive(username, password, ticket);
        }
        
        public System.Threading.Tasks.Task<bool> verifyReceiveAsync(string username, string password, string ticket) {
            return base.Channel.verifyReceiveAsync(username, password, ticket);
        }
        
        public IranSoftjo.Package.WebUi.Android.SmsBartarServiceReference.AccountInfo accountInfo(string username, string password) {
            return base.Channel.accountInfo(username, password);
        }
        
        public System.Threading.Tasks.Task<IranSoftjo.Package.WebUi.Android.SmsBartarServiceReference.AccountInfo> accountInfoAsync(string username, string password) {
            return base.Channel.accountInfoAsync(username, password);
        }
        
        public string changePassword(string username, string password, string newPassword) {
            return base.Channel.changePassword(username, password, newPassword);
        }
        
        public System.Threading.Tasks.Task<string> changePasswordAsync(string username, string password, string newPassword) {
            return base.Channel.changePasswordAsync(username, password, newPassword);
        }
        
        public string changeTrafficRelay(string username, string password, string newURL) {
            return base.Channel.changeTrafficRelay(username, password, newURL);
        }
        
        public System.Threading.Tasks.Task<string> changeTrafficRelayAsync(string username, string password, string newURL) {
            return base.Channel.changeTrafficRelayAsync(username, password, newURL);
        }
    }
}
