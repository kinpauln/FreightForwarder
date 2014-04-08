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
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="FrightForwarder", ConfigurationName="FFWCF.IFFService")]
    public interface IFFService {
        
        [System.ServiceModel.OperationContractAttribute(Action="FrightForwarder/IFFService/GetRouteInformationItems", ReplyAction="FrightForwarder/IFFService/GetRouteInformationItemsResponse")]
        [FreightForwarder.MessageCompression.CompressionOperationBehavior]
        FreightForwarder.Domain.Entities.RouteInformationItem[] GetRouteInformationItems(System.Nullable<int> companyId);
        
        [System.ServiceModel.OperationContractAttribute(Action="FrightForwarder/IFFService/GetRouteInformationItems", ReplyAction="FrightForwarder/IFFService/GetRouteInformationItemsResponse")]
        System.Threading.Tasks.Task<FreightForwarder.Domain.Entities.RouteInformationItem[]> GetRouteInformationItemsAsync(System.Nullable<int> companyId);
        
        [System.ServiceModel.OperationContractAttribute(Action="FrightForwarder/IFFService/GetAllCompanyList", ReplyAction="FrightForwarder/IFFService/GetAllCompanyListResponse")]
        [FreightForwarder.MessageCompression.CompressionOperationBehavior]
        FreightForwarder.Domain.Entities.Company[] GetAllCompanyList();
        
        [System.ServiceModel.OperationContractAttribute(Action="FrightForwarder/IFFService/GetAllCompanyList", ReplyAction="FrightForwarder/IFFService/GetAllCompanyListResponse")]
        System.Threading.Tasks.Task<FreightForwarder.Domain.Entities.Company[]> GetAllCompanyListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="FrightForwarder/IFFService/GetAllCompanies", ReplyAction="FrightForwarder/IFFService/GetAllCompaniesResponse")]
        [FreightForwarder.MessageCompression.CompressionOperationBehavior]
        System.Collections.Generic.Dictionary<string, int> GetAllCompanies();
        
        [System.ServiceModel.OperationContractAttribute(Action="FrightForwarder/IFFService/GetAllCompanies", ReplyAction="FrightForwarder/IFFService/GetAllCompaniesResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.Dictionary<string, int>> GetAllCompaniesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="FrightForwarder/IFFService/ImportRouteInformationItems", ReplyAction="FrightForwarder/IFFService/ImportRouteInformationItemsResponse")]
        bool ImportRouteInformationItems(FreightForwarder.Domain.Entities.RouteInformationItem[] rlist);
        
        [System.ServiceModel.OperationContractAttribute(Action="FrightForwarder/IFFService/ImportRouteInformationItems", ReplyAction="FrightForwarder/IFFService/ImportRouteInformationItemsResponse")]
        System.Threading.Tasks.Task<bool> ImportRouteInformationItemsAsync(FreightForwarder.Domain.Entities.RouteInformationItem[] rlist);
        
        [System.ServiceModel.OperationContractAttribute(Action="FrightForwarder/IFFService/GetRoutItems", ReplyAction="FrightForwarder/IFFService/GetRoutItemsResponse")]
        [FreightForwarder.MessageCompression.CompressionOperationBehavior]
        FreightForwarder.Domain.Entities.RouteInformationItem[] GetRoutItems(string shipName, string startPort, string destinationPort, System.Nullable<bool> isSingleContainer);
        
        [System.ServiceModel.OperationContractAttribute(Action="FrightForwarder/IFFService/GetRoutItems", ReplyAction="FrightForwarder/IFFService/GetRoutItemsResponse")]
        System.Threading.Tasks.Task<FreightForwarder.Domain.Entities.RouteInformationItem[]> GetRoutItemsAsync(string shipName, string startPort, string destinationPort, System.Nullable<bool> isSingleContainer);
        
        [System.ServiceModel.OperationContractAttribute(Action="FrightForwarder/IFFService/AssociatMachineAndRegCode", ReplyAction="FrightForwarder/IFFService/AssociatMachineAndRegCodeResponse")]
        bool AssociatMachineAndRegCode(string machineCode, string regcode, int companyId);
        
        [System.ServiceModel.OperationContractAttribute(Action="FrightForwarder/IFFService/AssociatMachineAndRegCode", ReplyAction="FrightForwarder/IFFService/AssociatMachineAndRegCodeResponse")]
        System.Threading.Tasks.Task<bool> AssociatMachineAndRegCodeAsync(string machineCode, string regcode, int companyId);
        
        [System.ServiceModel.OperationContractAttribute(Action="FrightForwarder/IFFService/AddMachineCode", ReplyAction="FrightForwarder/IFFService/AddMachineCodeResponse")]
        bool AddMachineCode(string machineCode, string description, int companyId);
        
        [System.ServiceModel.OperationContractAttribute(Action="FrightForwarder/IFFService/AddMachineCode", ReplyAction="FrightForwarder/IFFService/AddMachineCodeResponse")]
        System.Threading.Tasks.Task<bool> AddMachineCodeAsync(string machineCode, string description, int companyId);
        
        [System.ServiceModel.OperationContractAttribute(Action="FrightForwarder/IFFService/IsRegistered", ReplyAction="FrightForwarder/IFFService/IsRegisteredResponse")]
        FreightForwarder.Domain.Entities.RegisterCode IsRegistered(string machineCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="FrightForwarder/IFFService/IsRegistered", ReplyAction="FrightForwarder/IFFService/IsRegisteredResponse")]
        System.Threading.Tasks.Task<FreightForwarder.Domain.Entities.RegisterCode> IsRegisteredAsync(string machineCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="FrightForwarder/IFFService/ValidateMachineCode", ReplyAction="FrightForwarder/IFFService/ValidateMachineCodeResponse")]
        bool ValidateMachineCode(string machineCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="FrightForwarder/IFFService/ValidateMachineCode", ReplyAction="FrightForwarder/IFFService/ValidateMachineCodeResponse")]
        System.Threading.Tasks.Task<bool> ValidateMachineCodeAsync(string machineCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="FrightForwarder/IFFService/AddCompany", ReplyAction="FrightForwarder/IFFService/AddCompanyResponse")]
        bool AddCompany(string companyName, string companyCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="FrightForwarder/IFFService/AddCompany", ReplyAction="FrightForwarder/IFFService/AddCompanyResponse")]
        System.Threading.Tasks.Task<bool> AddCompanyAsync(string companyName, string companyCode);
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
        
        public bool AddMachineCode(string machineCode, string description, int companyId) {
            return base.Channel.AddMachineCode(machineCode, description, companyId);
        }
        
        public System.Threading.Tasks.Task<bool> AddMachineCodeAsync(string machineCode, string description, int companyId) {
            return base.Channel.AddMachineCodeAsync(machineCode, description, companyId);
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
    }
}
