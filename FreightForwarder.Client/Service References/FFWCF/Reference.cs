﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18063
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace FreightForwarder.UI.Winform.FFWCF {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CompositeType", Namespace="http://schemas.datacontract.org/2004/07/FrightForwarder.WCF")]
    [System.SerializableAttribute()]
    public partial class CompositeType : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool BoolValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BoolValue {
            get {
                return this.BoolValueField;
            }
            set {
                if ((this.BoolValueField.Equals(value) != true)) {
                    this.BoolValueField = value;
                    this.RaisePropertyChanged("BoolValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
                }
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="FFWCF.IFFService")]
    public interface IFFService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFFService/GetRouteInformationItems", ReplyAction="http://tempuri.org/IFFService/GetRouteInformationItemsResponse")]
        FreightForwarder.Domain.Entities.RouteInformationItem[] GetRouteInformationItems(System.Nullable<int> companyId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFFService/GetRouteInformationItems", ReplyAction="http://tempuri.org/IFFService/GetRouteInformationItemsResponse")]
        System.Threading.Tasks.Task<FreightForwarder.Domain.Entities.RouteInformationItem[]> GetRouteInformationItemsAsync(System.Nullable<int> companyId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFFService/GetAllCompanyList", ReplyAction="http://tempuri.org/IFFService/GetAllCompanyListResponse")]
        FreightForwarder.Domain.Entities.Company[] GetAllCompanyList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFFService/GetAllCompanyList", ReplyAction="http://tempuri.org/IFFService/GetAllCompanyListResponse")]
        System.Threading.Tasks.Task<FreightForwarder.Domain.Entities.Company[]> GetAllCompanyListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFFService/GetAllCompanies", ReplyAction="http://tempuri.org/IFFService/GetAllCompaniesResponse")]
        System.Collections.Generic.Dictionary<string, int> GetAllCompanies();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFFService/GetAllCompanies", ReplyAction="http://tempuri.org/IFFService/GetAllCompaniesResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, int>> GetAllCompaniesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFFService/ImportRouteInformationItems", ReplyAction="http://tempuri.org/IFFService/ImportRouteInformationItemsResponse")]
        bool ImportRouteInformationItems(FreightForwarder.Domain.Entities.RouteInformationItem[] rlist);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFFService/ImportRouteInformationItems", ReplyAction="http://tempuri.org/IFFService/ImportRouteInformationItemsResponse")]
        System.Threading.Tasks.Task<bool> ImportRouteInformationItemsAsync(FreightForwarder.Domain.Entities.RouteInformationItem[] rlist);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFFService/GetRoutItems", ReplyAction="http://tempuri.org/IFFService/GetRoutItemsResponse")]
        FreightForwarder.Domain.Entities.RouteInformationItem[] GetRoutItems(string shipName, string startPort, string destinationPort, System.Nullable<bool> isSingleContainer);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFFService/GetRoutItems", ReplyAction="http://tempuri.org/IFFService/GetRoutItemsResponse")]
        System.Threading.Tasks.Task<FreightForwarder.Domain.Entities.RouteInformationItem[]> GetRoutItemsAsync(string shipName, string startPort, string destinationPort, System.Nullable<bool> isSingleContainer);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFFService/AssociatMachineAndRegCode", ReplyAction="http://tempuri.org/IFFService/AssociatMachineAndRegCodeResponse")]
        bool AssociatMachineAndRegCode(string machineCode, string regcode, int companyId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFFService/AssociatMachineAndRegCode", ReplyAction="http://tempuri.org/IFFService/AssociatMachineAndRegCodeResponse")]
        System.Threading.Tasks.Task<bool> AssociatMachineAndRegCodeAsync(string machineCode, string regcode, int companyId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFFService/AddMachineCode", ReplyAction="http://tempuri.org/IFFService/AddMachineCodeResponse")]
        bool AddMachineCode(string machineCode, int companyId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFFService/AddMachineCode", ReplyAction="http://tempuri.org/IFFService/AddMachineCodeResponse")]
        System.Threading.Tasks.Task<bool> AddMachineCodeAsync(string machineCode, int companyId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFFService/IsRegistered", ReplyAction="http://tempuri.org/IFFService/IsRegisteredResponse")]
        FreightForwarder.Domain.Entities.RegisterCode IsRegistered(string machineCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFFService/IsRegistered", ReplyAction="http://tempuri.org/IFFService/IsRegisteredResponse")]
        System.Threading.Tasks.Task<FreightForwarder.Domain.Entities.RegisterCode> IsRegisteredAsync(string machineCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFFService/ValidateMachineCode", ReplyAction="http://tempuri.org/IFFService/ValidateMachineCodeResponse")]
        bool ValidateMachineCode(string machineCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFFService/ValidateMachineCode", ReplyAction="http://tempuri.org/IFFService/ValidateMachineCodeResponse")]
        System.Threading.Tasks.Task<bool> ValidateMachineCodeAsync(string machineCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFFService/AddCompany", ReplyAction="http://tempuri.org/IFFService/AddCompanyResponse")]
        bool AddCompany(string companyName, string companyCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFFService/AddCompany", ReplyAction="http://tempuri.org/IFFService/AddCompanyResponse")]
        System.Threading.Tasks.Task<bool> AddCompanyAsync(string companyName, string companyCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFFService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IFFService/GetDataUsingDataContractResponse")]
        FreightForwarder.UI.Winform.FFWCF.CompositeType GetDataUsingDataContract(FreightForwarder.UI.Winform.FFWCF.CompositeType composite);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFFService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IFFService/GetDataUsingDataContractResponse")]
        System.Threading.Tasks.Task<FreightForwarder.UI.Winform.FFWCF.CompositeType> GetDataUsingDataContractAsync(FreightForwarder.UI.Winform.FFWCF.CompositeType composite);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFFServiceChannel : FreightForwarder.UI.Winform.FFWCF.IFFService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FFServiceClient : System.ServiceModel.ClientBase<FreightForwarder.UI.Winform.FFWCF.IFFService>, FreightForwarder.UI.Winform.FFWCF.IFFService {
        
        public FFServiceClient() {
        }
        
        public FFServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FFServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FFServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FFServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public FreightForwarder.Domain.Entities.RouteInformationItem[] GetRouteInformationItems(System.Nullable<int> companyId) {
            return base.Channel.GetRouteInformationItems(companyId);
        }
        
        public System.Threading.Tasks.Task<FreightForwarder.Domain.Entities.RouteInformationItem[]> GetRouteInformationItemsAsync(System.Nullable<int> companyId) {
            return base.Channel.GetRouteInformationItemsAsync(companyId);
        }
        
        public FreightForwarder.Domain.Entities.Company[] GetAllCompanyList() {
            return base.Channel.GetAllCompanyList();
        }
        
        public System.Threading.Tasks.Task<FreightForwarder.Domain.Entities.Company[]> GetAllCompanyListAsync() {
            return base.Channel.GetAllCompanyListAsync();
        }
        
        public System.Collections.Generic.Dictionary<string, int> GetAllCompanies() {
            return base.Channel.GetAllCompanies();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, int>> GetAllCompaniesAsync() {
            return base.Channel.GetAllCompaniesAsync();
        }
        
        public bool ImportRouteInformationItems(FreightForwarder.Domain.Entities.RouteInformationItem[] rlist) {
            return base.Channel.ImportRouteInformationItems(rlist);
        }
        
        public System.Threading.Tasks.Task<bool> ImportRouteInformationItemsAsync(FreightForwarder.Domain.Entities.RouteInformationItem[] rlist) {
            return base.Channel.ImportRouteInformationItemsAsync(rlist);
        }
        
        public FreightForwarder.Domain.Entities.RouteInformationItem[] GetRoutItems(string shipName, string startPort, string destinationPort, System.Nullable<bool> isSingleContainer) {
            return base.Channel.GetRoutItems(shipName, startPort, destinationPort, isSingleContainer);
        }
        
        public System.Threading.Tasks.Task<FreightForwarder.Domain.Entities.RouteInformationItem[]> GetRoutItemsAsync(string shipName, string startPort, string destinationPort, System.Nullable<bool> isSingleContainer) {
            return base.Channel.GetRoutItemsAsync(shipName, startPort, destinationPort, isSingleContainer);
        }
        
        public bool AssociatMachineAndRegCode(string machineCode, string regcode, int companyId) {
            return base.Channel.AssociatMachineAndRegCode(machineCode, regcode, companyId);
        }
        
        public System.Threading.Tasks.Task<bool> AssociatMachineAndRegCodeAsync(string machineCode, string regcode, int companyId) {
            return base.Channel.AssociatMachineAndRegCodeAsync(machineCode, regcode, companyId);
        }
        
        public bool AddMachineCode(string machineCode, int companyId) {
            return base.Channel.AddMachineCode(machineCode, companyId);
        }
        
        public System.Threading.Tasks.Task<bool> AddMachineCodeAsync(string machineCode, int companyId) {
            return base.Channel.AddMachineCodeAsync(machineCode, companyId);
        }
        
        public FreightForwarder.Domain.Entities.RegisterCode IsRegistered(string machineCode) {
            return base.Channel.IsRegistered(machineCode);
        }
        
        public System.Threading.Tasks.Task<FreightForwarder.Domain.Entities.RegisterCode> IsRegisteredAsync(string machineCode) {
            return base.Channel.IsRegisteredAsync(machineCode);
        }
        
        public bool ValidateMachineCode(string machineCode) {
            return base.Channel.ValidateMachineCode(machineCode);
        }
        
        public System.Threading.Tasks.Task<bool> ValidateMachineCodeAsync(string machineCode) {
            return base.Channel.ValidateMachineCodeAsync(machineCode);
        }
        
        public bool AddCompany(string companyName, string companyCode) {
            return base.Channel.AddCompany(companyName, companyCode);
        }
        
        public System.Threading.Tasks.Task<bool> AddCompanyAsync(string companyName, string companyCode) {
            return base.Channel.AddCompanyAsync(companyName, companyCode);
        }
        
        public FreightForwarder.UI.Winform.FFWCF.CompositeType GetDataUsingDataContract(FreightForwarder.UI.Winform.FFWCF.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public System.Threading.Tasks.Task<FreightForwarder.UI.Winform.FFWCF.CompositeType> GetDataUsingDataContractAsync(FreightForwarder.UI.Winform.FFWCF.CompositeType composite) {
            return base.Channel.GetDataUsingDataContractAsync(composite);
        }
    }
}
