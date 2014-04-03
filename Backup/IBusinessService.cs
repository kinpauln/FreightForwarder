using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;

namespace BigzoneBusinessCenterService {
    // 注意: 如果更改此处的接口名称 "IService1"，也必须更新 Web.config 中对 "IService1" 的引用。
    #region Enums

    /// <summary>
    ///  终端机提交上来的申请状态
    /// </summary>
    [DataContract]
    public enum AtmBusinessState
    {
        [EnumMember]
        UnHandled = 0,
        [EnumMember]
        Handled = 1,
        [EnumMember]
        Handling = 2,
        [EnumMember]
        Denied = 3
    }

    #endregion

    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceKnownType(typeof(AtmBusinessState))]
    [ServiceContract]
    public interface IBusinessService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        string CheckService(string message);

        [OperationContract]
        Boolean DelOrder(int orderid, int busid, int btype);

        #region User

        [OperationContract]
        string UserLogin(string UserName, string UserPwd);

        [OperationContract]
        string AddUser(string UserName, string UserPwd, int GroupId, string[] BusNames);

        [OperationContract]
        bool AltPassword(int UserId, string NewPwd);

        [OperationContract]
        User GetUser(int UserId);

        [OperationContract]
        List<User> GetUsers();

        [OperationContract]
        bool DeleteUser(int id);

        //获取用户详细信息
        [OperationContract]
        List<UserDetailInfo> GetUserDetails(int[] UserId);

        #endregion

        #region UserGroup

        [OperationContract]
        List<UserGroup> GetUserGroups();

        [OperationContract]
        UserGroup GetUserGroup(int UserId);

        [OperationContract]
        bool SaveUserGroup(int UserId, int GroupId, string[] BusNames);

        [OperationContract]
        List<BusinessType> GetBusinessTypes();

        [OperationContract]
        bool SetCause(int OrderId, string Text);
        

        #endregion

        #region Drive

        //获取行驶证类型
        [OperationContract]
        List<DriveType> GetDriveTypes();

        //登记行驶证
        [OperationContract]
        string AddDrive(Drive drive, int userid, BusinessFrom from, string BarCode);

        //修改行驶证
        [OperationContract]
        string UpdateDrive(Drive drive, Order odr);
        
        [OperationContract]
        List<Drive> GetDrives(int[] Id);

        #endregion

        #region Card

        [OperationContract]
        List<CardType> GetCardTypes();

        [OperationContract]
        string AddCard(Card card, int userid, BusinessFrom from, string BarCode);

        [OperationContract]
        string UpdateCard(Card card,Order odr);

        [OperationContract]
        List<Card> GetCards(int[] Id);

        #endregion

        #region Steer

        [OperationContract]
        List<IdentType> GetIdentTypes();

        [OperationContract]
        string AddSteer(Steer steer, int userid, BusinessFrom from, string BarCode);

        [OperationContract]
        string UpdateSteer(Steer steer, Order odr);

        [OperationContract]
        List<Steer> GetSteers(int[] Id);

        #endregion

        #region Order

         //根据OrderId取去业务视图中返回记录
        [OperationContract]
        DataSet GetOrderDetail(int OrderId, BusinessTypeEnum type);

        //根据Order取号牌种类
        [OperationContract]
        CardType GetOrderCardType(int OrderId, BusinessTypeEnum type);

        //判断是不是应该填写原因
        [OperationContract]
        bool IsNeedSetCause(int OrderId);

        //查询
        [OperationContract]
        DataSet ExecSQL(string Sql);

        //生成业务流水号
        [OperationContract]
        string BuildBarCode(BusinessFrom from, BusinessTypeEnum type);

        //取业务列表
        [OperationContract]
        List<Order> GetOrderList();

        //取自己的任务
        [OperationContract]
        DataSet GetMyWorkList(int UserId, string PerWhere, string BusinessType);

        //根据用户ID取需要提醒的记录
        [OperationContract]
        DataSet GetMyUnReadOrder(int UserId);

        //确认Order状态
        [OperationContract]
        string ConfirmOrder(int UserId, int[] OrderId, WorkState State);

        //确认照片已重新上传
        [OperationContract]
        Boolean ConfirmImgReloaded(string barcode);

        //与档案不符
        [OperationContract]
        bool FileNotCorrect(int OrderId);

        //缺少照片
        [OperationContract]
        bool SetImageNotExists(int OrderId);

        //申请人拒收
        [OperationContract]
        bool SetApplyDenial(int OrderId);

        //取满足条件的order
        [OperationContract]
        List<Order> GetOrders(int[] OrderIds);

        //检查是不是有了照片
        [OperationContract]
        bool OrderPhotoExists(string BarCode);

        //下载照片
        [OperationContract]
        Images DownloadImage(string BarCode);

        //是否有了原因 
        [OperationContract]
        string HasCause(int OrderId);

        #endregion

        #region Use Enum

        [OperationContract]
        string doUserGroupEnum(UserGroupEnum group);

        [OperationContract]
        string doBusinessTypeEnum(BusinessTypeEnum busType);

        [OperationContract]
        string doDriveTypeEnum(DriveTypeEnum driveType);

        #endregion

        #region InnerManage

        //设置用户名和密码
        [OperationContract]
        bool SaveDataUser(DataUser User);

        //取用户信息
        [OperationContract]
        DataUser GetDataUserInfo();

        #endregion

        #region Terminal App

        ///// <summary>
        /////  撤销客服长时间未处理的任务
        ///// </summary>
        ///// <param name="timeLong">超时未处理任务的分钟数</param>
        //[OperationContract]
        //ServiceRetnEntity RevokeTasks(int timeLong);

        ///// <summary>
        /////  主动获取任务
        ///// </summary>
        //[OperationContract]
        //ServiceRetnEntity TransferBusiness(int businessId);

        ///// <summary>
        /////  主动获取任务
        ///// </summary>
        //[OperationContract]
        //ServiceRetnEntity GetOneTaskActively(int userId);

        ///// <summary>
        /////  更新用户在线状态
        ///// </summary>
        //[OperationContract]
        //bool NoticeServerOnline(int userid);

        ///// <summary>
        /////  获取所有当前在线的用户集合
        ///// </summary>
        //[OperationContract]
        //List<User> GetAllOnLineCustomerServiceUsers();

        ///// <summary>
        ///// 检查客服人员有无需要确认的终端机业务记录，返回DataSet
        ///// </summary>
        ///// <param name="UserId"></param>
        ///// <returns></returns>
        //[OperationContract]
        //DataSet GetCustomerServiceRemindList(int UserId);

        //[OperationContract]
        //ServiceRetnEntity UpdateTerminalBusinessState(int busid, AtmBusinessState state, string remarkContent);

        ////[OperationContract]
        ////IEnumerable<TerminalAppBusinessEntity> GetTerminalAppBusinessByCondition(AtmBusinessState state, BusinessTypeEnum type);
                
        ////[OperationContract]
        ////IEnumerable<TerminalAppBusinessEntity> GetTerminalAppBusinessAll();

        ////[OperationContract]
        ////Boolean UpDateTerminalAppBusiness(TerminalAppBusinessEntity enty);
        #endregion
    }


    // 使用下面示例中说明的数据约定将复合类型添加到服务操作。
    [DataContract]
    public class CompositeType {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
