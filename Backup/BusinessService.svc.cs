using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Linq;
using System.Web.Configuration;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace BigzoneBusinessCenterService {
    // 注意: 如果更改此处的类名“Service1”，也必须更新 Web.config 和关联的 .svc 文件中对“Service1”的引用。

    #region Enum

    //行车证类型
    public enum DriveTypeEnum
    {
        BigCar = 1,
        SmallCar = 2,
        GuaCar = 15
    }

    //用户组
    public enum UserGroupEnum
    {
        Administrator = 1,
        CustomerService = 2,
        Filer = 3,
        Maker = 4
    }

    //状态
    public enum WorkState
    {
        Applied = 1,
        Registered = 2,
        FilerAccepted = 3,
        FilerPosted = 4,
        MakerAcceptedFiles = 5,
        MakerCompleted = 6,
        MakerPosted = 7,
        ApplyConfirmed = 8
    }

    //业务类型
    public enum BusinessTypeEnum
    {
        Drive = 1,
        Steer = 2,
        Card = 3
    }

    //业务来源
    public enum BusinessFrom
    {
        Internet = 1,
        Tel = 2,
        Sms = 3,
        ATM = 4
    }

    #endregion

    public class BusinessService : IBusinessService {
        public string GetData(int value) {
            return string.Format("You entered: {0}", value);
        }

        public string Constr = ConfigurationManager.ConnectionStrings["TransPad"].ConnectionString;
        public CompositeType GetDataUsingDataContract(CompositeType composite) {
            if (composite.BoolValue) {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string CheckService(string message) {
            return message;
        }

        public Boolean DelOrder(int orderid, int busid,int btype) {

            using (DataContext dc = new DataContext(Constr))
            {
                if (dc.Connection != null) dc.Connection.Open();
                DbTransaction tran = dc.Connection.BeginTransaction();
                dc.Transaction = tran;

                //登记行驶证信息
                try
                {
                    //登记车牌信息
                    Order odr = (from d in dc.GetTable<Order>()
                                where d.Id == orderid
                                select d)
                               .FirstOrDefault<Order>();
                    if (odr != null)
                    {
                        dc.GetTable<Order>().DeleteOnSubmit(odr);
                        dc.SubmitChanges();
                    }
                    else
                    {
                        tran.Rollback();
                        return false;
                    }

                    switch (btype) { 
                            //drive
                        case 1:
                            Drive dv = (from d in dc.GetTable<Drive>()
                                         where d.Id == busid
                                         select d)
                                       .FirstOrDefault<Drive>();
                            if (dv != null)
                            {
                                dc.GetTable<Drive>().DeleteOnSubmit(dv);
                                dc.SubmitChanges();
                            }
                            else
                            {
                                tran.Rollback();
                                return false;
                            }
                            break;
                            //steer
                        case 2:
                            Steer str = (from d in dc.GetTable<Steer>()
                                         where d.Id == busid
                                        select d)
                                       .FirstOrDefault<Steer>();
                            if (str != null)
                            {
                                dc.GetTable<Steer>().DeleteOnSubmit(str);
                                dc.SubmitChanges();
                            }
                            else
                            {
                                tran.Rollback();
                                return false;
                            }
                            break;
                            //card
                        case 3:
                            Card cd = (from d in dc.GetTable<Card>()
                                       where d.Id == busid
                                         select d)
                                       .FirstOrDefault<Card>();
                            if (cd != null)
                            {
                                dc.GetTable<Card>().DeleteOnSubmit(cd);
                                dc.SubmitChanges();
                            }
                            else
                            {
                                tran.Rollback();
                                return false;
                            }
                            break;
                    }

                    IEnumerable<WorkList> wlist = (from d in dc.GetTable<WorkList>()
                                where d.OrderId == orderid
                                select d)
                               .AsEnumerable<WorkList>();
                    if (wlist.Count() >0)
                    {
                        dc.GetTable<WorkList>().DeleteAllOnSubmit(wlist);
                        dc.SubmitChanges();
                    }
                    else
                    {
                        tran.Rollback();
                        return false;
                    }

                    tran.Commit();
                    return true;
                }
                catch (SqlException ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        #region User
        //用户登录 
        public string UserLogin(string UserName, string UserPwd)
        {
            try
            {
                string sRet = "1:登录成功！";
                using (DataContext dc = new DataContext(Constr))
                {
                    var users = from u in dc.GetTable<User>()
                                where u.UserName.ToLower().Trim() == UserName.ToLower().Trim() && u.UserPwd == UserPwd
                                select u;
                    User usr = null;
                    if (users.ToList().Count > 0)
                    {
                        usr = users.FirstOrDefault<User>();
                        usr.LogTimes += 1;
                        usr.LastLogin = DateTime.Now;
                        dc.SubmitChanges();
                        sRet = string.Format("1:{0}", usr.UserId);
                    }
                    else
                    {
                        sRet = "0:用户名或密码错误，登录失败！";
                    }
                }
                return sRet;
            }
            catch (Exception ex) { return string.Format("0:{0}", ex.Message); }
        }

        //增加用户
        public string AddUser(string UserName, string UserPwd, int GroupId, string[] BusNames)
        {
            try
            {
                string sRet = "1:用户添加成功！";
                using (DataContext dc = new DataContext(Constr))
                {
                    var users = from u in dc.GetTable<User>()
                                where u.UserName.Trim().ToLower() == UserName.Trim().ToLower()
                                select u;
                    if (users.ToList().Count > 0)
                    {
                        sRet = "0:已经存在该用户，请更换用户名！";
                        return sRet;
                    }

                    //添加用户
                    User usr = new User()
                    {
                        GroupId = GroupId,
                        UserName = UserName,
                        UserPwd = UserPwd,
                        LogTimes = 0
                    };
                    dc.GetTable<User>().InsertOnSubmit(usr);
                    dc.SubmitChanges();

                    //设置该用户所属业务
                    foreach (string s in BusNames)
                    {
                        dc.GetTable<UserBelong>().InsertOnSubmit(new UserBelong()
                        {
                            UserId = usr.UserId,
                            BusName = s
                        });
                    }
                    dc.SubmitChanges();
                }
                return sRet;
            }
            catch (Exception ex) { return string.Format("0:{0}", ex.Message); }
        }

        //修改密码
        public bool AltPassword(int UserId, string NewPwd)
        {
            using (DataContext dc = new DataContext(Constr))
            {
                var users = from u in dc.GetTable<User>()
                            where u.UserId == UserId
                            select u;
                List<User> list = users.ToList();
                if (list.Count > 0)
                {
                    list[0].UserPwd = NewPwd;
                    dc.SubmitChanges();
                    return true;
                }
                return false;
            }
        }

        //取用户信息
        public User GetUser(int UserId)
        {
            Predicate<User> pre = delegate(User usr)
            {
                return usr.UserId == UserId;
            };
            return GetUsers().Find(pre);
        }

        //获取用户详细信息
        public List<UserDetailInfo> GetUserDetails(int[] UserId)
        {
            List<UserDetailInfo> userdetails = new List<UserDetailInfo>();
            using (DataContext dc = new DataContext(Constr))
            {
                var users = from u in dc.GetTable<User>()
                            where UserId.Contains(u.UserId)
                            select u;
                if (users.ToList().Count > 0)
                {
                    UserDetailInfo detail = null;
                    foreach (User usr in users.ToList())
                    {
                        //用户信息
                        detail = new UserDetailInfo()
                        {
                            User = usr
                        };
                        
                        //用户组
                        var groups = from g in dc.GetTable<UserGroup>()
                                     where g.GroupId == usr.GroupId
                                     select g;
                        if (groups.ToList().Count > 0)
                        {
                            detail.UserGroup = groups.FirstOrDefault<UserGroup>();
                        }

                        //用户业务信息
                        if (usr != null)
                        {
                            var belongs = from b in dc.GetTable<UserBelong>()
                                          where b.UserId == usr.UserId
                                          select b;
                            if (belongs.ToList().Count > 0)
                            {
                                detail.UserBelongs = belongs.ToList();
                            }
                        }

                        //业务类型
                        var bs = from b in dc.GetTable<BusinessType>()
                                 select b;
                        detail.BusinessType = bs.ToList();

                        userdetails.Add(detail);
                    }
                }
            }
            return userdetails;
        }

        //取所有用户信息
        public List<User> GetUsers()
        {
            using (DataContext dc = new DataContext(Constr))
            {
                var users = from user in dc.GetTable<User>()
                            select user;
                List<User> list = users.ToList();
                return list;
            }
        }

        //删除用户
        public bool DeleteUser(int id)
        {
            try
            {
                using (DataContext dc = new DataContext(Constr))
                {
                    var users = from user in dc.GetTable<User>()
                                where user.UserId == id
                                select user;
                    List<User> list = users.ToList();
                    if (list.Count > 0)
                    {
                        dc.GetTable<User>().DeleteOnSubmit(list[0]);
                        dc.SubmitChanges();
                        return true;
                    }
                }
                return false;
            }
            catch { return false; }
        }

        #endregion

        #region UserGroup

        //取所有组信息
        public List<UserGroup> GetUserGroups()
        {
            using (DataContext dc = new DataContext(Constr))
            {
                var groups = from g in dc.GetTable<UserGroup>()
                             select g;
                List<UserGroup> list = groups.ToList();
                return list;
            }
        }

        //加载某个用户的用户组信息
        public UserGroup GetUserGroup(int UserId)
        {
            using (DataContext dc = new DataContext(Constr))
            {
                var groups = from u in dc.GetTable<User>()
                             join g in dc.GetTable<UserGroup>() on u.GroupId equals g.Id
                             where u.UserId == UserId
                             select g;
                List<UserGroup> list = groups.ToList();
                if (list.Count > 0)
                {
                    return list[0];
                }
            }
            return null;
            /*List<UserGroup> groups = GetUserGroups();
            Predicate<UserGroup> pre = delegate(UserGroup group)
            {
                return group.Id == UserId;
            };
            return groups.Find(pre);*/
        }

        //修改某个用户所属的用户组
        public bool SaveUserGroup(int UserId, int GroupId, string[] BusNames)
        {
            try
            {
                using (DataContext dc = new DataContext(Constr))
                {
                    var users = from u in dc.GetTable<User>()
                                where u.UserId == UserId
                                select u;
                    List<User> list = users.ToList();
                    User usr = null;
                    if (list.Count > 0)
                    {
                        //修改用户信息
                        usr = list[0];
                        usr.GroupId = GroupId;
                        dc.SubmitChanges();

                        //设置该用户所属的业务
                        var belongs = from b in dc.GetTable<UserBelong>()
                                      where b.UserId == usr.UserId
                                      select b;
                        foreach (UserBelong b in belongs.ToList())
                        {
                            dc.GetTable<UserBelong>().DeleteOnSubmit(b);
                        }
                        dc.SubmitChanges();

                        //增加业务所属
                        foreach (string s in BusNames)
                        {
                            dc.GetTable<UserBelong>().InsertOnSubmit(new UserBelong()
                            {
                                UserId = usr.UserId,
                                BusName = s
                            });
                        }
                        dc.SubmitChanges();

                        return true;
                    }
                }
                return false;
            }
            catch { return false; }
        }

        #endregion

        #region Drive

        //获取行车证类型
        public List<DriveType> GetDriveTypes()
        {
            using (DataContext dc = new DataContext(Constr))
            {
                var types = from t in dc.GetTable<DriveType>()
                            select t;
                return types.ToList();
            }
        }

        //添加车驶证记录
        public string AddDrive(Drive drive, int userid, BusinessFrom from, string BarCode)
        {
            string sRet = "01:登记成功！";
            if (drive == null)
            {
                sRet = "00:提交的数据集为空，登记失败！";
                return sRet;
            }

            using (DataContext dc = new DataContext(Constr))
            {
                //登记行驶证信息
                dc.GetTable<Drive>().InsertOnSubmit(drive);
                dc.SubmitChanges();

                //加入Order记录
                Order order = new Order()
                {
                    State = "0000000011",          //初始状态为外网已经登记
                    BusinessType = (int)BusinessTypeEnum.Drive,
                    BusinessId = drive.Id,
                    BarCode = string.IsNullOrEmpty(BarCode) ? BuildBarCode(from, BusinessTypeEnum.Drive) : BarCode,
                    ApplyDenial = false,
                    Complete = false,
                    FileCorrect = true,
                    ImageExists = true
                };
                dc.GetTable<Order>().InsertOnSubmit(order);
                dc.SubmitChanges();

                //登记写日志
                AddLog(new Log()
                {
                    UserId = userid,
                    OrderId = order.Id,
                    StartState = (int)WorkState.Applied,
                    LastState = (int)WorkState.Registered
                });

                //为档案管理员分配任务
                string sAssFiler = AssTask(order.Id, UserGroupEnum.Filer, "Drive");

                //为制证管理员分配任务
                //string sAssMaker = AssTask(order.Id, UserGroupEnum.Maker);

                if (!string.IsNullOrEmpty(sAssFiler) && sAssFiler.StartsWith("1"))
                {
                    return sRet;
                }
                else
                {
                    sRet = sAssFiler;
                }
            }
            return sRet;
        }

        
        //修改车驶证记录
        public string UpdateDrive(Drive drive, Order odr)
        {
            string sRet = "01:更新成功！";
            if (drive == null)
            {
                sRet = "00:提交的数据集为空，更新失败！";
                return sRet;
            }

            using (DataContext dc = new DataContext(Constr))
            {
                if (dc.Connection != null) dc.Connection.Open();
                DbTransaction tran = dc.Connection.BeginTransaction();
                dc.Transaction = tran;

                //登记行驶证信息
                try
                {
                    //登记车牌信息
                    Drive dv = (from d in dc.GetTable<Drive>()
                                where d.Id == drive.Id
                               select d)
                               .FirstOrDefault<Drive>();
                    if (dv == null)
                    {
                        sRet = "00:数据库中未找到对应的行驶证信息记录！";
                        return sRet;
                    }

                    dv.CardNo = drive.CardNo;
                    dv.ViewNo = drive.ViewNo;
                    dv.ApplyName = drive.ApplyName;
                    dv.IdCard = drive.IdCard;
                    dv.Tel = drive.Tel;
                    dv.OfficeTel = drive.OfficeTel;
                    dv.FamilyTel = drive.FamilyTel;
                    dv.Code = drive.Code;
                    dv.Mail = drive.Mail;
                    dv.Addr = drive.Addr;
                    dv.Cause = drive.Cause;

                    dc.SubmitChanges(); 
                    
                    Order od = (from o in dc.GetTable<Order>()
                                                    where o.BusinessType == odr.BusinessType && o.BusinessId == odr.BusinessId
                                                    select o)
                                .FirstOrDefault();
                    if (od != null)
                    {
                        od.BarCode = odr.BarCode;
                        dc.SubmitChanges();
                        tran.Commit();
                    }
                    else
                    {
                        sRet = "00:没有找到指定的业务";
                        tran.Rollback();
                    }
                }
                catch (SqlException ex)
                {
                    sRet = "00:" + ex.ToString();
                    return sRet;
                }
            }
            return sRet;
        }
        
        //取驾驶证信息
        public List<Drive> GetDrives(int[] Id)
        {
            using (DataContext dc = new DataContext(Constr))
            {
                var drives = from d in dc.GetTable<Drive>()
                             where Id.Contains(d.Id)
                             select d;
                return drives.ToList();
            }
        }

        #endregion

        #region Card

        //获取车牌 号牌种类
        public List<CardType> GetCardTypes()
        {
            using (DataContext dc = new DataContext(Constr))
            {
                var cards = from c in dc.GetTable<CardType>()
                            select c;
                return cards.ToList();
            }
        }

        //登记车牌
        public string AddCard(Card card, int userid, BusinessFrom from, string BarCode)
        {
            string sRet = "01:登记成功！";
            if (card == null)
            {
                sRet = "00:提交的数据集为空，登记失败！";
                return sRet;
            }

            using (DataContext dc = new DataContext(Constr))
            {
                //登记车牌信息
                dc.GetTable<Card>().InsertOnSubmit(card);
                dc.SubmitChanges();

                //加入Order记录
                Order order = new Order()
                {
                    State = "0000000011",
                    BusinessType = (int)BusinessTypeEnum.Card,
                    BusinessId = card.Id,
                    BarCode = string.IsNullOrEmpty(BarCode) ? BuildBarCode(from, BusinessTypeEnum.Card) : BarCode,
                    ApplyDenial = false,
                    Complete = false,
                    FileCorrect = true,
                    ImageExists = true
                };
                dc.GetTable<Order>().InsertOnSubmit(order);
                dc.SubmitChanges();

                //登记写日志
                AddLog(new Log()
                {
                    UserId = userid,
                    OrderId = order.Id,
                    StartState = (int)WorkState.Applied,
                    LastState = (int)WorkState.Registered
                });

                //为制证管理员分配任务
                string sAssFiler = AssTask(order.Id, UserGroupEnum.Filer,"Card");
                if (!string.IsNullOrEmpty(sAssFiler) && sAssFiler.StartsWith("1"))
                {
                    return sRet;
                }
                else
                {
                    sRet = sAssFiler;
                }
            }
            return sRet;
        }
        
        //更新车牌
        public string UpdateCard(Card card,Order odr)
        {
            string sRet = "01:更新成功！";
            if (card == null)
            {
                sRet = "00:提交的数据集为空，更新失败！";
                return sRet;
            }

            using (DataContext dc = new DataContext(Constr))
            {
                if (dc.Connection != null) dc.Connection.Open();
                DbTransaction tran = dc.Connection.BeginTransaction();
                dc.Transaction = tran;

                try
                {
                    //登记车牌信息
                    Card cd = (from c in dc.GetTable<Card>()
                               where c.Id == card.Id
                               select c)
                               .FirstOrDefault<Card>();
                    if (cd == null)
                    {
                        sRet = "00:数据库中未找到对应的车牌信息记录！";
                        return sRet;
                    }
                    //cd.Type = card.Type;
                    cd.CardNo = card.CardNo;
                    cd.ViewNo = card.ViewNo;
                    cd.ApplyName = card.ApplyName;
                    cd.IdCard = card.IdCard;
                    cd.Tel = card.Tel;
                    cd.OfficeTel = card.OfficeTel;
                    cd.FamilyTel = card.FamilyTel;
                    cd.Code = card.Code;
                    cd.Mail = card.Mail;
                    cd.Addr = card.Addr;
                    cd.Cause = card.Cause;

                    dc.SubmitChanges();

                    Order od = (from o in dc.GetTable<Order>()
                                where o.BusinessType == odr.BusinessType && o.BusinessId == odr.BusinessId
                                select o)
                                .FirstOrDefault();
                    if (od != null)
                    {
                        od.BarCode = odr.BarCode;
                        dc.SubmitChanges();
                        tran.Commit();
                    }
                    else
                    {
                        sRet = "00:没有找到指定的业务";
                        tran.Rollback();
                    }
                }
                catch (SqlException ex)
                {
                    tran.Rollback();
                    sRet = "00:" + ex.ToString();
                    return sRet;
                }
            }
            return sRet;
        }
        
        //取车牌信息
        public List<Card> GetCards(int[] Id)
        {
            using (DataContext dc = new DataContext(Constr))
            {
                var cards = from c in dc.GetTable<Card>()
                            where Id.Contains(c.Id)
                            select c;
                return cards.ToList();
            }
        }

        #endregion

        #region Steer

        //获取标驾驶证标识类型
        public List<IdentType> GetIdentTypes()
        {
            using (DataContext dc = new DataContext(Constr))
            {
                var idents = from c in dc.GetTable<IdentType>()
                            select c;
                return idents.ToList();
            }
        }

        //登记驾驶证信息
        public string AddSteer(Steer steer, int userid, BusinessFrom from, string BarCode)
        {
            string sRet = "01:登记成功！";
            if (steer == null)
            {
                sRet = "00:提交的数据集为空，登记失败！";
                return sRet;
            }

            using (DataContext dc = new DataContext(Constr))
            {
                //登记车牌信息
                dc.GetTable<Steer>().InsertOnSubmit(steer);
                dc.SubmitChanges();

                //加入Order记录
                Order order = new Order()
                {
                    State = "0000000011",          //初始状态为外网已经登记
                    BusinessType = (int)BusinessTypeEnum.Steer,
                    BusinessId = steer.Id,
                    BarCode = string.IsNullOrEmpty(BarCode) ? BuildBarCode(from, BusinessTypeEnum.Steer) : BarCode,
                    ApplyDenial = false,
                    Complete = false,
                    FileCorrect = true,
                    ImageExists = true
                };
                dc.GetTable<Order>().InsertOnSubmit(order);
                dc.SubmitChanges();

                //登记写日志
                AddLog(new Log()
                {
                    UserId = userid,
                    OrderId = order.Id,
                    StartState = (int)WorkState.Applied,
                    LastState = (int)WorkState.Registered
                });

                //为档案管理员分配任务
                string sAssFiler = AssTask(order.Id, UserGroupEnum.Filer,"Steer");

                //为制证管理员分配任务
                //string sAssMaker = AssTask(order.Id, UserGroupEnum.Maker);

                if (!string.IsNullOrEmpty(sAssFiler) && sAssFiler.StartsWith("1"))
                {
                    return sRet;
                }
                else
                {
                    sRet = sAssFiler;
                }
            }
            return sRet;
        }
        
        //更新驾驶证信息
        public string UpdateSteer(Steer steer,Order odr)
        {
            string sRet = "01:更新成功！";
            if (steer == null)
            {
                sRet = "00:提交的数据集为空，更新失败！";
                return sRet;
            }

            using (DataContext dc = new DataContext(Constr))
            {
                if (dc.Connection != null) dc.Connection.Open();
                DbTransaction tran = dc.Connection.BeginTransaction();
                dc.Transaction = tran;

                //登记行驶证信息
                try
                {
                    //登记车牌信息
                    Steer st = (from s in dc.GetTable<Steer>()
                                where s.Id == steer.Id
                                select s)
                               .FirstOrDefault<Steer>();
                    if (st == null)
                    {
                        sRet = "00:数据库中未找到对应的驾驶证信息记录！";
                        return sRet;
                    }

                    st.IndentNo = steer.IndentNo;
                    st.FileId = steer.FileId;
                    st.ApplyName = steer.ApplyName;
                    st.IdCard = steer.IdCard;
                    st.Tel = steer.Tel;
                    st.OfficeTel = steer.OfficeTel;
                    st.FamilyTel = steer.FamilyTel;
                    st.Code = steer.Code;
                    st.Mail = steer.Mail;
                    st.Addr = steer.Addr;
                    st.Cause = steer.Cause;
                    st.CardInnerCode = steer.CardInnerCode;

                    dc.SubmitChanges(); 
                    
                    Order od = (from o in dc.GetTable<Order>()
                                                    where o.BusinessType == odr.BusinessType && o.BusinessId == odr.BusinessId
                                                    select o)
                                 .FirstOrDefault();
                    if (od != null)
                    {
                        od.BarCode = odr.BarCode;
                        dc.SubmitChanges();
                        tran.Commit();
                    }
                    else
                    {
                        sRet = "00:没有找到指定的业务";
                        tran.Rollback();
                    }
                }
                catch (SqlException ex)
                {
                    tran.Rollback();
                    sRet = "00:" + ex.ToString();
                    return sRet;
                }
            }
            return sRet;
        }
        

        //取驾驶证信息
        public List<Steer> GetSteers(int[] Id)
        {
            using (DataContext dc = new DataContext(Constr))
            {
                var steers = from s in dc.GetTable<Steer>()
                             where Id.Contains(s.Id)
                             select s;
                return steers.ToList();
            }
        }

        #endregion

        #region Order

        //根据OrderId取去业务视图中返回记录
        public DataSet GetOrderDetail(int OrderId, BusinessTypeEnum type)
        {
            DataSet ds = new DataSet();
            string sql = string.Format("select * from ");
            switch (type)
            {
                case BusinessTypeEnum.Card:
                    sql += " v_cardinfo ";
                    break;
                case BusinessTypeEnum.Drive:
                    sql += " v_driveinfo ";
                    break;
                case BusinessTypeEnum.Steer:
                    sql += " v_steerinfo ";
                    break;
            }
            sql += string.Format(" where orderid = '{0}'", OrderId);
            return GetDataSet(sql);
        }

        //根据Order取号牌种类
        public CardType GetOrderCardType(int OrderId, BusinessTypeEnum type)
        {
            CardType ct = null;
            string sql = string.Format("select * from ");
            switch (type)
            {
                case BusinessTypeEnum.Card:
                    sql += " v_cardinfo ";
                    break;
                case BusinessTypeEnum.Drive:
                    sql += " v_driveinfo ";
                    break;
            }
            sql += string.Format(" where orderid = '{0}'", OrderId);
            DataTable dt = GetTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                using (DataContext dc = new DataContext(Constr))
                {
                    var types = from t in dc.GetTable<CardType>()
                                where t.Code.Trim() == dt.Rows[0]["CardTypeId"].ToString().Trim()
                                select t;
                    ct = types.FirstOrDefault<CardType>();
                }
            }
            return ct;
        }

        //判断是不是应该填写原因
        /*
         * 1.与档案不符，并且没有写原因应该显示
         * 
         * 2.被申请人拒收，并且没有写原因应该显示
         * 
         */
        public bool IsNeedSetCause(int OrderId)
        {
            bool bCause = false;
            using (DataContext dc = new DataContext(Constr))
            {
                var orders = from o in dc.GetTable<Order>()
                             where o.Id == OrderId
                             select o;
                if (orders.ToList().Count > 0)
                {
                    Order order = orders.FirstOrDefault<Order>();

                    //先取有没有原因
                    var causes = from c in dc.GetTable<Cause>()
                                 where c.OrderId == order.Id
                                 select c;
                    if (causes.ToList().Count > 0)
                    {
                        //有原因
                        bCause = true;
                    }

                    //再取是不是与档案不符或被申请人拒收
                    bool bError = (!order.FileCorrect || order.ApplyDenial);

                    if (bError && !bCause)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }   

        //查询
        public DataSet ExecSQL(string Sql)
        {
            DataSet ds = new DataSet();
            if (Sql.ToLower().StartsWith("select"))
            {
                using (SqlConnection con = new SqlConnection(Constr))
                {
                    SqlDataAdapter da = new SqlDataAdapter(Sql, con);
                    da.Fill(ds);
                }
            }
            else
            {
                using (SqlConnection con = new SqlConnection(Constr))
                {
                    SqlCommand cmd = new SqlCommand(Sql, con);
                    cmd.ExecuteNonQuery();
                }
            }
            return ds;
        }

        //是否有了原因 
        public string HasCause(int OrderId)
        {
            string sRet = string.Empty;
            using (DataContext dc = new DataContext(Constr))
            {
                var causes = from c in dc.GetTable<Cause>()
                             where c.OrderId == OrderId
                             select c;
                if (causes.ToList().Count > 0)
                {
                    sRet = causes.FirstOrDefault<Cause>().Memo;
                    return sRet;
                }
            }
            return sRet;
        }

        //写失败原因
        public bool SetCause(int OrderId, string Text)
        {
            using (DataContext dc = new DataContext(Constr))
            {
                //提交原因
                dc.GetTable<Cause>().InsertOnSubmit(new Cause()
                {
                    OrderId = OrderId,
                    Memo = Text
                });

                //设置order完成标志为失败
                var orders = from o in dc.GetTable<Order>()
                             where o.Id == OrderId
                             select o;
                if (orders.ToList().Count > 0)
                {
                    orders.FirstOrDefault<Order>().Failed = true;
                }
                dc.SubmitChanges();
            }
            return true;
        }

        //取业务类型
        public List<BusinessType> GetBusinessTypes()
        {
            using (DataContext dc = new DataContext(Constr))
            {
                var buses = from b in dc.GetTable<BusinessType>()
                            select b;
                return buses.ToList();
            }
        }

        //生成条形码
        public string BuildBarCode(BusinessFrom from, BusinessTypeEnum type)
        {
            //日期部分
            string code = string.Empty;
            string PartDate = DateTime.Now.ToString("yyyyMMdd");
            code += PartDate;

            //来源部分
            int fm = (int)from;
            code += fm.ToString();

            //业务类型
            string sql = string.Empty;
            int BusId = (int)type;

            code += string.Format("0{0}", BusId);

            //业务流水号
            string id = "01";
            using (DataContext dc = new DataContext(Constr))
            {
                lock (dc)
                {
                    var orders = from o in dc.GetTable<Order>()
                                 where o.BusinessType == BusId && Convert.ToDateTime(o.Entry).Date == DateTime.Now.Date
                                 select o;
                    List<Order> list = orders.ToList();

                    if (list.Count == 0)
                    {
                        id = "01";
                    }
                    else if (list.Count < 9)
                    {
                        id = string.Format("0{0}", list.Count + 1);
                    }
                    else
                    {
                        id = (list.Count + 1).ToString();
                    }
                }
            }

            code += id;
            return code;
        }

        //分配任务
        private string AssTask(int OrderId, UserGroupEnum UserGp, string BusName)
        {
            string sRet = "为用户分配任务时失败！";
            try
            {
                //为UserGp所在的用户分配任务
                string sql = string.Format(@"select u.userid,count(w.id) workcount 
                        from users as u left join worklist as w 
                        on u.userid=w.userid where u.groupid = {0} 
                        and u.userid in (select userid from userbelong where busname='{1}') 
                        group by u.userid 
                        order by workcount asc", (int)UserGp, BusName.Trim());

                DataTable dt = GetTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    int iLeastUserId = (int)dt.Rows[0]["userid"];
                    using (DataContext dc = new DataContext(Constr))
                    {
                        dc.GetTable<WorkList>().InsertOnSubmit(new WorkList()
                        {
                            OrderId = OrderId,
                            UserId = iLeastUserId
                        });
                        dc.SubmitChanges();

                        sRet = "1";
                        return sRet;
                    }
                }
                return sRet;
            }
            catch (Exception ex) { return ex.Message; }
        }

        //档案提交资料，转交任务给制证
        private string AssTask2Maker(int OrderId)
        {
            string sRet = "为用户分配任务时失败！";
            try
            {
                //为制证用户分配任务
                string sql = string.Format(@"select u.userid,count(w.id) workcount 
                        from users as u left join worklist as w 
                        on u.userid=w.userid where u.groupid = {0} 
                        group by u.userid 
                        order by workcount asc", (int)UserGroupEnum.Maker);

                DataTable dt = GetTable(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    int iLeastUserId = (int)dt.Rows[0]["userid"];
                    using (DataContext dc = new DataContext(Constr))
                    {
                        var wls = from w in dc.GetTable<WorkList>()
                                  where w.OrderId == OrderId
                                  select w;
                        wls.FirstOrDefault<WorkList>().UserId = iLeastUserId;
                        dc.SubmitChanges();

                        sRet = "1";
                        return sRet;
                    }
                }
                return sRet;
            }
            catch (Exception ex) { return ex.Message; }
        }

        //取业务列表
        public List<Order> GetOrderList()
        {
            using (DataContext dc = new DataContext(Constr))
            {
                var orders = from o in dc.GetTable<Order>()
                             select o;
                return orders.ToList();
            }
        }

        //取我的工作列表
        public DataSet GetMyWorkList(int UserId, string PerWhere, string BusinessType)
        {
            string sPerWhere = " 1 = 1 ";
            if (!string.IsNullOrEmpty(PerWhere))
                sPerWhere = PerWhere;

            string sBusWhere = " 1 = 1 ";
            if (!string.IsNullOrEmpty(BusinessType))
                sBusWhere = string.Format(" BusTypeName  = '{0}' ", BusinessType);

            bool bAdmin = false;
            using (DataContext dc = new DataContext(Constr))
            {
                var users = from u in dc.GetTable<User>()
                            where u.UserId == UserId
                            select u;
                if (users.ToList().Count > 0)
                {
                    User usr = users.ToList()[0];
                    bAdmin = (usr.GroupId == (int)UserGroupEnum.Administrator ||
                              usr.GroupId == (int)UserGroupEnum.CustomerService);
                }
            }

            using (SqlConnection con = new SqlConnection(Constr))
            {
                DataSet ds = new DataSet();
                string sql = string.Empty;

                if (!bAdmin)
                {
                    //自己的业务，并且与档案信息相符
                    sql = string.Format(
                        @"select *, convert(varchar(30),orderentry,120) as ShortEntryDate from v_worklist where {0} and {1} 
                            and userid={2} 
                            and filecorrect <> 0 
                            and applydenial <> 1 
                            and imageexists <> 0
                            order by orderentry desc", sPerWhere, sBusWhere, UserId);
                }
                else
                {
                    sql = string.Format(
                        @"select *, convert(varchar(30),orderentry,120) as ShortEntryDate from v_worklist as v,users as u
                        where v.userid=u.userid and u.userid in (select userid from v_userdetail where groupname = '档案员' or groupname = '客服')
                        and {0} and {1}", sPerWhere, sBusWhere);
                }

                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(ds);
                
                //添加申请人姓名列
                DataColumn col = new DataColumn("ApplyName", Type.GetType("System.String"));
                DataColumn colProcesser = new DataColumn("Processer", Type.GetType("System.String"));

                ds.Tables[0].Columns.Add(col);
                ds.Tables[0].Columns.Add(colProcesser);

                string sId = string.Empty;
                string sTypeName = string.Empty;
                string sSql = string.Empty;
                SqlDataAdapter daC = null;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    sId = Convert.ToString(row["BusId"]);
                    sTypeName = (string)row["BusTypeName"];
                    sSql = string.Format("select * from {0} where Id = {1}", sTypeName.ToLower().Trim(), sId);
                    daC = new SqlDataAdapter(sSql, con);
                    using (DataTable dtC = new DataTable())
                    {
                        daC.Fill(dtC);
                        if (dtC != null && dtC.Rows.Count > 0)
                        {
                            row["ApplyName"] = dtC.Rows[0]["ApplyName"];
                        }
                    }

                    //找出该业务的处理人
                    using (DataContext dc = new DataContext(Constr))
                    {
                        var wls = from w in dc.GetTable<WorkList>()
                                     where w.OrderId == (int)row["OrderId"]
                                     select w;
                        if (wls.ToList().Count == 1)
                        {
                            //现在在档案处，直接找出用户填上
                            var users = from u in dc.GetTable<User>()
                                        where u.UserId == wls.FirstOrDefault<WorkList>().UserId
                                        select u;
                            if (users.ToList().Count > 0)
                            {
                                var groups = from g in dc.GetTable<UserGroup>()
                                             where g.GroupId == users.FirstOrDefault<User>().GroupId
                                             select g;
                                row["Processer"] = string.Format("{0}({1})", 
                                    users.FirstOrDefault<User>().UserName.Trim(), 
                                    groups.FirstOrDefault<UserGroup>().GroupName);
                            }
                        }
                        else if (wls.ToList().Count == 2)
                        { 
                            /*  档案提交了材料后，就会有两个记录  */
                            string sPer = (string)row["State"];
                            string s = string.Empty;
                            string sd = string.Empty;

                            //用户id
                            foreach (WorkList wl in wls.ToList())
                            {
                                sd += string.Format("{0},", wl.UserId);
                            }

                            if (sd.Length > 0 && sd.Substring(sd.Length - 1, 1) == ",")
                            {
                                sd = sd.Substring(0, sd.Length - 1);
                            }

                            //确定是显示档案还是制证姓名
                            if (sPer.ToLower().Trim() == "0001111111" || sPer.ToLower().Trim() == "0011111111")
                            {
                                //应该显示档案员的姓名
                                s = string.Format("select * from users as u,usergroup as g where u.groupid = g.groupid and userid in ({0}) and g.GroupId = 3", sd);
                            }
                            else
                            {
                                //显示制证员姓名
                                s = string.Format("select * from users as u,usergroup as g where u.groupid = g.groupid and userid in ({0}) and g.GroupId = 4", sd);
                            }
                            DataTable dtS = new DataTable();
                            SqlDataAdapter daS = new SqlDataAdapter(s, con);
                            daS.Fill(dtS);

                            if (dtS != null && dtS.Rows.Count > 0)
                            {
                                row["Processer"] = string.Format("{0}({1})", dtS.Rows[0]["UserName"], dtS.Rows[0]["GroupName"]);
                            }
                        }
                    }

                }

                return ds;
            }
        }

        //检查档案管理员有无需要确认的业务记录，返回DataSet
        public DataSet GetMyUnReadOrder(int UserId)
        {
            int UserGroupId = (int)UserGroupEnum.Filer;    //默认是档案管理员
            using (DataContext dc = new DataContext(Constr))
            {
                var users = from u in dc.GetTable<User>()
                            where u.UserId == UserId
                            select u;
                List<User> list = users.ToList();
                if (list.Count > 0)
                {
                    UserGroupId = list[0].GroupId;
                }
            }

            //以下SQL语句使用位运算计算Order状态
            string sql = string.Empty;
            if (UserGroupId == (int)UserGroupEnum.Filer)
            {
                //filer
                sql = string.Format(@"select * 
                                from v_worklist where (state = '{0}' or ImageReload = 1) and userid={1}", "0000000011", UserId);
            }
            else if(UserGroupId==(int)UserGroupEnum.Maker)
            {
                //maker
                sql = string.Format(@"select * 
                                from v_worklist where state = '{0}' and userid={1}", "0000001111", UserId);
            }

            DataSet ds = GetDataSet(sql);
            return ds;
        }

        /// <summary>
        ///  确认照片已重新上传
        /// </summary>
        /// <param name="barcode">业务编码</param>
        public Boolean ConfirmImgReloaded(string barcode)
        {
            using (DataContext dc = new DataContext(Constr))
            {
                try
                {
                    //更新照片状态
                    var orders = from o in dc.GetTable<Order>()
                                 where o.BarCode.Trim().ToLower() == barcode.Trim().ToLower()
                                 select o;
                    Order order = orders.FirstOrDefault<Order>();
                    if (order != null)
                    {
                        order.ImageReload = false;
                    }
                    dc.SubmitChanges();
                    return true;
                }
                catch (SqlException ex) {
                    return false;
                }
            }
        }

        //确认Order状态
        public string ConfirmOrder(int UserId, int[] OrderId, WorkState State)
        {
            string sRet = "1:状态确认成功！";
            using (DataContext dc = new DataContext(Constr))
            {
                var orders = from o in dc.GetTable<Order>()
                             where OrderId.Contains(o.Id)
                             select o;
                List<Order> list = orders.ToList();
                string NowState = string.Empty;

                //确认状态的时候，如果状态变为档案已经提交材料，则找出当前业务数最少的用户分配给他。
                foreach (Order o in orders.ToList())
                {
                    NowState = o.State;
                    o.State = GetState(o.State, State);       //将当前OrderId的记录状态改为State

                    string BusName = string.Empty;
                    var buses = from b in dc.GetTable<BusinessType>()
                                where b.Id == o.BusinessType
                                select b;
                    if (buses.ToList().Count > 0)
                    {
                        BusName = buses.FirstOrDefault<BusinessType>().Type.Trim();
                    }
                    else
                    {
                        sRet = "0:确认状态时，未能找到该业务的类型，确认失败！";
                        return sRet;
                    }

                    //如果档案提交材料，则转交给制证去做
                    if (State == WorkState.FilerPosted)
                    {
                        AssTask(o.Id, UserGroupEnum.Maker, BusName);
                    }

                    //如果状态为申请人已确认，则将业务完成状态置为真
                    if (State == WorkState.ApplyConfirmed)
                    {
                        o.Complete = true;
                    }

                    /*如果是Maker邮寄了，则删除其在worklist中的任务，因为这时，Maker的任务已经完成
                    if (State == WorkState.MakerPosted)
                    {
                        using (DataContext dc2 = new DataContext(Constr))
                        {
                            var wls = from w in dc2.GetTable<WorkList>()
                                      where w.OrderId == o.Id && w.UserId == UserId
                                      select w;
                            dc2.GetTable<WorkList>().DeleteOnSubmit(wls.FirstOrDefault<WorkList>());
                            dc2.SubmitChanges();
                        }
                    }*/

                    //写日志
                    AddLog(new Log()
                    {
                        UserId = UserId,
                        OrderId = o.Id,
                        StartState = GetStateId(NowState),
                        LastState = GetStateId(o.State)
                    });
                }
                dc.SubmitChanges();
            }
            return sRet;
        }

        //根据权限枚举设置权限位
        private string GetState(string Now, WorkState State)
        {
            string state = string.Empty;
            switch (State)
            {
                case WorkState.Applied:
                    state = "0000000001";
                    break;
                case WorkState.Registered:
                    state = "0000000011";
                    break;
                case WorkState.FilerAccepted:
                    state = "0000000111";
                    break;
                case WorkState.FilerPosted:
                    state = "0000001111";
                    break;
                case WorkState.MakerAcceptedFiles:
                    state = "0000011111";
                    break;
                case WorkState.MakerCompleted:
                    state = "0000111111";
                    break;
                case WorkState.MakerPosted:
                    state = "0001111111";
                    break;
                case WorkState.ApplyConfirmed:
                    state = "0011111111";
                    break;
            }
            return state;
        }

        //根据权限码，取状态对象
        private int GetStateId(string Per)
        {
            int iPerId = -1;
            using (DataContext dc = new DataContext(Constr))
            {
                var states = from s in dc.GetTable<StateItem>()
                             where s.Per.Trim() == Per.Trim()
                             select s;
                List<StateItem> list = states.ToList();
                if (list.Count > 0)
                {
                    iPerId = list[0].State;
                }
            }
            return iPerId;
        }

        //与档案不符
        public bool FileNotCorrect(int OrderId)
        {
            using (DataContext dc = new DataContext(Constr))
            {
                var orders = from o in dc.GetTable<Order>()
                             where o.Id == OrderId
                             select o;
                //List<Order> list = orders.ToList();
                if (orders.Count<Order>() > 0)
                {
                    orders.FirstOrDefault<Order>().FileCorrect = false;
                    dc.SubmitChanges();
                }
            }
            return true;
        }

        //缺少照片
        public bool SetImageNotExists(int OrderId)
        {
            using (DataContext dc = new DataContext(Constr))
            {
                var orders = from o in dc.GetTable<Order>()
                             where o.Id == OrderId
                             select o;
                List<Order> list = orders.ToList();
                if (list.Count > 0)
                {
                    list[0].ImageExists = false;
                    dc.SubmitChanges();
                }
            }
            return true;
        }

        //申请人拒收
        public bool SetApplyDenial(int OrderId)
        {
            using (DataContext dc = new DataContext(Constr))
            {
                var orders = from o in dc.GetTable<Order>()
                             where o.Id == OrderId
                             select o;
                List<Order> list = orders.ToList();
                if (list.Count > 0)
                {
                    list[0].ApplyDenial = true;
                    dc.SubmitChanges();
                }
            }
            return true;
        }

        //取满足条件的order
        public List<Order> GetOrders(int[] OrderIds)
        {
            using (DataContext dc = new DataContext(Constr))
            {
                var orders = from o in dc.GetTable<Order>()
                             where OrderIds.Contains(o.Id)
                             select o;
                List<Order> list = orders.ToList();
                return list;
            }
        }

        //检查是不是有了照片
        public bool OrderPhotoExists(string BarCode)
        {
            using (DataContext dc = new DataContext(Constr))
            {
                var images = from i in dc.GetTable<Images>()
                             where i.BarCode.Trim().ToLower() == BarCode.Trim().ToLower()
                             select i;
                if (images.ToList().Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //下载照片
        public Images DownloadImage(string BarCode)
        {
            using (DataContext dc = new DataContext(Constr))
            {
                var images = from i in dc.GetTable<Images>()
                             where i.BarCode.Trim().ToLower() == BarCode.Trim().ToLower()
                             select i;
                return images.FirstOrDefault<Images>();
            }
        }
        #endregion

        #region Comm

        //取DataSet
        private DataSet GetDataSet(string sql)
        {
            using (SqlConnection con = new SqlConnection(Constr))
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }

        //写日志
        private bool AddLog(Log log)
        {
            if (log == null) return false;
            using (DataContext dc = new DataContext(Constr))
            {
                dc.GetTable<Log>().InsertOnSubmit(log);
                dc.SubmitChanges();
            }
            return true;
        }

        //根据SQL语句，返回一张表
        private DataTable GetTable(string sql)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(Constr))
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                da.Fill(dt);
                return dt;
            }
        }

        #endregion

        #region Use Enum

        public string doUserGroupEnum(UserGroupEnum group)
        {
            return string.Empty;
        }

        public string doBusinessTypeEnum(BusinessTypeEnum busType)
        {
            return string.Empty;
        }

        public string doDriveTypeEnum(DriveTypeEnum driveType)
        {
            return string.Empty;
        }
        #endregion

        #region InnerManage

        //设置用户名和密码
        public bool SaveDataUser(DataUser User)
        {
            using (DataContext dc = new DataContext(Constr))
            {
                var users = from u in dc.GetTable<DataUser>()
                            select u;
                if (users.ToList().Count > 0)
                {
                    //修改
                    users.FirstOrDefault<DataUser>().UserName = User.UserName;
                    users.FirstOrDefault<DataUser>().UserPwd = User.UserPwd;
                    dc.SubmitChanges();
                }
                else
                {
                    //增加
                    dc.GetTable<DataUser>().InsertOnSubmit(new DataUser()
                    {
                        UserName = User.UserName,
                        UserPwd = User.UserPwd
                    });
                    dc.SubmitChanges();
                }
                return true;
            }
        }

        //取用户信息
        public DataUser GetDataUserInfo()
        {
            using (DataContext dc = new DataContext(Constr))
            {
                var users = from u in dc.GetTable<DataUser>()
                            select u;
                return users.FirstOrDefault<DataUser>();
            }
        }

        #endregion

        #region Terminal App

        /// <summary>
        ///  撤销客服超过10分钟未处理的任务
        /// </summary>
        /// <param name="timeLong">超时未处理任务的分钟数</param>
        public ServiceRetnEntity RevokeTasks(int timeLong)
        {             
            ServiceRetnEntity retnEnty = new ServiceRetnEntity();
            using (DataContext dc = new DataContext(Constr))
            {
                Table<CustomerServiceWorkListEntity> workListTb = dc.GetTable<CustomerServiceWorkListEntity>();
                Table<TerminalReapplyBusinessEntity> reapplyTb = dc.GetTable<TerminalReapplyBusinessEntity>();

                try { 

                    //检索服务器是否存在已分配但超过10分钟未处理的业务
                    var outDateBusinessList = (from apply in reapplyTb
                                   join work in workListTb
                                   on apply.Id equals work.BusinessId
                                   where (apply.State ?? -1) == (int)AtmBusinessState.Handling
                                   orderby work.EntryTime ascending
                                   select work)
                                   .AsEnumerable().Where(w=>{
                                       if (w.EntryTime == null)
                                           return false;

                                       //分配任务的时间
                                       DateTime taskEntryTime = (DateTime)w.EntryTime;
                                       //现在时间
                                       DateTime nowTime = DateTime.Now;
                                       //时间差 
                                       TimeSpan ts = nowTime.Subtract(taskEntryTime);
                                       //已分配任务的时长(秒为单位) 
                                       int taskTimeLength = ts.Days * 24 * 3600 + ts.Hours * 3600 + ts.Minutes * 60 + ts.Seconds;
                                       return taskTimeLength > timeLong*60 ? true : false;
                                   });

                    if (outDateBusinessList.Count() > 0)
                    {
                        DbTransaction tran = null;
                        dc.Connection.Open();
                        tran = dc.Connection.BeginTransaction();
                        dc.Transaction = tran;

                        // 计数器
                        int count = 0;
                        string errorCode = string.Empty;
                        string errorMsg = string.Empty;
                        foreach (var item in outDateBusinessList) {
                            // 业务Id
                            int currBusinessId = item.BusinessId??0;

                            var workEnty = (from workitem in workListTb
                                            where workitem.BusinessId == currBusinessId
                                            select workitem)
                                           .FirstOrDefault();
                            if (workEnty != null)
                            {
                                // 从工作列表中删除当前业务
                                workListTb.DeleteOnSubmit(workEnty);

                                // 更新当前要移交的业务的状态为"未处理"
                                TerminalReapplyBusinessEntity businessEnty = (from reapply in dc.GetTable<TerminalReapplyBusinessEntity>()
                                                                              where reapply.Id == currBusinessId
                                                                              select reapply)
                                                  .FirstOrDefault();
                                if (businessEnty != null)
                                {
                                    businessEnty.State = (int)AtmBusinessState.UnHandled;
                                    businessEnty.UpdateTime = DateTime.Now;
                                }
                                else
                                {
                                    errorCode = "05";
                                    errorMsg = string.Format("更新业务状态时，为找到指定业务Id为 {0} 的业务记录！", currBusinessId.ToString());
                                    break;
                                }
                            }
                            else
                            {
                                errorCode = "04";
                                errorMsg = string.Format("删除工作任务时，未找到对应该业务Id为 {0} 的业务！",currBusinessId.ToString());
                                break;
                            }
                            count++;
                        }
                        // 计数器的值不小于集合长度则提交事务
                        if (count == outDateBusinessList.Count())
                        {
                            dc.SubmitChanges();
                            // 事务提交
                            tran.Commit();
                            retnEnty.Success = true;
                        }
                        else {
                            retnEnty.Success = false;
                            retnEnty.ErrorCode = errorCode;
                            retnEnty.Message = errorMsg;
                            // 事务回滚
                            tran.Rollback();
                        }
                    }
                    else
                    {
                        retnEnty.Success = false;
                        retnEnty.ErrorCode = "03";
                        retnEnty.Message = "没有超时未处理的业务！";
                    }
                }
                catch (SqlException ex)
                {
                    retnEnty.Success = false;
                    retnEnty.ErrorCode = "01";
                    retnEnty.Message = ex.Message;
                }
                catch (Exception ex)
                {
                    retnEnty.Success = false;
                    retnEnty.ErrorCode = "02";
                    retnEnty.Message = ex.Message;
                }
            }
            return retnEnty;
        }

        /// <summary>
        ///  移交任务
        /// </summary>
        public ServiceRetnEntity TransferBusiness(int businessId)
        {
            ServiceRetnEntity retnEnty = new ServiceRetnEntity();
            using (DataContext dc = new DataContext(Constr))
            {
                try
                {
                    Table<CustomerServiceWorkListEntity> workListTb = dc.GetTable<CustomerServiceWorkListEntity>();
                    Table<TerminalReapplyBusinessEntity> reapplyTb = dc.GetTable<TerminalReapplyBusinessEntity>();
                    DbTransaction tran = null;

                    dc.Connection.Open();
                    tran = dc.Connection.BeginTransaction();
                    dc.Transaction = tran;
                    var workEnty = (from item in workListTb
                                    where item.BusinessId == businessId
                                    select item)
                                   .FirstOrDefault();
                    if (workEnty != null)
                    {
                        // 从工作列表中删除当前业务
                        workListTb.DeleteOnSubmit(workEnty);

                        // 更新当前要移交的业务的状态为"未处理"
                        TerminalReapplyBusinessEntity businessEnty = (from reapply in dc.GetTable<TerminalReapplyBusinessEntity>()
                                                                      where reapply.Id == businessId
                                                                      select reapply)
                                          .FirstOrDefault();
                        if (businessEnty != null)
                        {
                            businessEnty.State = (int)AtmBusinessState.UnHandled;
                            businessEnty.UpdateTime = DateTime.Now;
                            dc.SubmitChanges();
                            tran.Commit();
                            retnEnty.Success = true;
                        }
                        else
                        {
                            retnEnty.Success = false;
                            retnEnty.ErrorCode = "04";
                            retnEnty.Message = "更新业务状态时，为找到指定业务Id的业务记录！";
                        }
                    }
                    else
                    {
                        retnEnty.Success = false;
                        retnEnty.ErrorCode = "03";
                        //retnEnty.Message = "工作列表中没有对应该业务Id的业务！";
                        retnEnty.Message = string.Format("工作列表中未找到您要处理的业务Id为 {0} 的业务，可能是您超过10分钟未处理该业务，导致此业务已移交。请尝试关闭业务提醒框等待其他业务！",
                                      businessId);
                    }
                }
                catch (SqlException ex)
                {
                    retnEnty.Success = false;
                    retnEnty.ErrorCode = "01";
                    retnEnty.Message = ex.Message;
                }
                catch (Exception ex)
                {
                    retnEnty.Success = false;
                    retnEnty.ErrorCode = "02";
                    retnEnty.Message = ex.Message;
                }
            }
            return retnEnty;
        }

        /// <summary>
        ///  主动获取任务
        /// </summary>
        public ServiceRetnEntity GetOneTaskActively(int userId) {
            ServiceRetnEntity retnEnty = null;
            using (DataContext dc = new DataContext(Constr)) {
                retnEnty = new ServiceRetnEntity();
                try
                {
                    Table<CustomerServiceWorkListEntity> workListTb = dc.GetTable<CustomerServiceWorkListEntity>();
                    Table<TerminalReapplyBusinessEntity> reapplyTb = dc.GetTable<TerminalReapplyBusinessEntity>();

                    // 获取当前用户 已分配且还未完成的任务量
                    int taskCount = (from item in workListTb
                                     join reapply in reapplyTb
                                     on item.BusinessId equals reapply.Id
                                     where (reapply.State ?? -1) == (int)AtmBusinessState.Handling
                                     select item)
                                    .Count();

                    // 如果当前用户没任务，则主动获取一个任务
                    if (taskCount == 0)
                    {
                        // 检查数据库中有无尚未处理的业务（按业务提交时间，有早到晚）
                        TerminalReapplyBusinessEntity businessEnty = (from reapply in dc.GetTable<TerminalReapplyBusinessEntity>()
                                          where (reapply.State ?? -1) == (int)AtmBusinessState.UnHandled
                                          orderby reapply.EntryTime ascending
                                                                      select reapply)
                                          .FirstOrDefault();
                        // 有待处理的新业务
                        if (businessEnty != null) {
                            DbTransaction tran = null;
                            try 
                            {
                                dc.Connection.Open();
                                tran = dc.Connection.BeginTransaction();
                                dc.Transaction = tran;

                                var workEnty = (from wk in workListTb
                                                where wk.BusinessId == businessEnty.Id
                                                select wk)
                                                .FirstOrDefault();
                                if (workEnty != null)
                                {
                                    tran.Rollback();
                                    retnEnty.Success = false;
                                    retnEnty.ErrorCode = "04";
                                    retnEnty.Message = string.Format("业务Id为 {0} 的任务已于 {1} 被分配给了用户Id为 {2} 了，您不能再次获取该业务。",
                                                                    workEnty.BusinessId.ToString(),
                                                                    workEnty.EntryTime==null?"[未知时间]":(((DateTime)workEnty.EntryTime).ToString("yyyy-MM-dd HH:mm:ss")),
                                                                    workEnty.UserId.ToString()
                                    );
                                }
                                else
                                {
                                    // 向工作列表中插入一条新的业务记录
                                    workListTb.InsertOnSubmit(new CustomerServiceWorkListEntity()
                                    {
                                        BusinessId = businessEnty.Id,
                                        UserId = userId,
                                        EntryTime = DateTime.Now
                                    });
                                    // 更新当前业务的状态为“处理中”
                                    businessEnty.State = (int)AtmBusinessState.Handling;
                                    businessEnty.UpdateTime = DateTime.Now;

                                    dc.SubmitChanges();
                                    tran.Commit();
                                    retnEnty.Success = true;
                                }
                            }
                            catch (SqlException ex) {
                                tran.Rollback();
                                retnEnty.Success = false;
                                retnEnty.ErrorCode = "04";
                                retnEnty.Message = ex.Message;
                            }
                            catch (Exception ex)
                            {
                                tran.Rollback();
                                retnEnty.Success = false;
                                retnEnty.ErrorCode = "05";
                                retnEnty.Message = ex.Message;
                            }
                        }
                        else
                        {
                            retnEnty.Success = false;
                            retnEnty.ErrorCode = "03";
                            retnEnty.Message = "当前没有待处理的新业务！";
                        }
                    }
                    else
                    {
                        retnEnty.Success = false;
                        retnEnty.ErrorCode = "01";
                        retnEnty.Message = string.Format("用户Id为 {0} 的当前用户已有正在处理的业务，在此业务受理完毕前不能获取新业务！",
                                                         userId.ToString());
                    }
                }
                catch (Exception ex)
                {
                    retnEnty.Success = false;
                    retnEnty.ErrorCode = "02";
                    retnEnty.Message = ex.Message;
                }
            }
            return retnEnty;
        }

        /// <summary>
        ///  在线用户定时通知服务器更新自己的在线状态
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool NoticeServerOnline(int userid)
        {
            using (DataContext dc = new DataContext(Constr))
            {
                try
                {
                    var enty = (from item in dc.GetTable<User>()
                                where item.UserId == userid
                                select item)
                                .FirstOrDefault();

                    if (enty != null) {
                        enty.LastLogin = DateTime.Now;
                        dc.SubmitChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        /// <summary>
        ///  获取所有当前在线的用户集合
        /// </summary>
        public List<User> GetAllOnLineCustomerServiceUsers() {
            using (DataContext dc = new DataContext(Constr)) {
                try {
                    var list = (from item in dc.GetTable<User>()
                                where item.GroupId == (int)UserGroupEnum.CustomerService
                                select item)
                                .AsEnumerable<User>().Where(user =>
                                {
                                    if (user.LastLogin == null)
                                        return false;
                                    DateTime lastLoginTime = (DateTime)user.LastLogin;//最后一次的登录时间
                                    DateTime nowTime = DateTime.Now;//现在在时间
                                    TimeSpan ts = nowTime.Subtract(lastLoginTime);//时间差    
                                    int lastLoginTimeLength = ts.Days * 24 * 3600 + ts.Hours * 3600 + ts.Minutes * 60 + ts.Seconds;//最后一次的在线时长(秒为单位) 
                                    return lastLoginTimeLength <= 60 ? true : false;
                                })
                                .ToList();
                    return list;

                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        class InnerEnty
        {
            public string BusTypeName { get; set; }
            public DateTime? ApplyDate { get; set; }
            public string LinkTel { get; set; }

            public string Name { get; set; }
            public bool? Sex { get; set; }
            public DateTime? BirthDate { get; set; }
            public string IdcardNo { get; set; }
            public byte[] Photo { get; set; }
        }
        /// <summary>
        /// 检查客服人员有无需要确认的终端机业务记录，返回DataSet
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public DataSet GetCustomerServiceRemindList(int UserId)
        {
            using (SqlConnection conn = new SqlConnection(Constr))
            {
                StringBuilder sbd = new StringBuilder();
                sbd.Append("SELECT top 1");
                sbd.Append("(CASE ");
                sbd.Append("WHEN ([t1].[BusinessType]) = 4 THEN '行驶证' ");
                sbd.Append("WHEN ([t1].[BusinessType]) = 6 THEN '驾驶证' ");
                sbd.Append("WHEN ([t1].[BusinessType]) = 5 THEN '车牌' ");
                sbd.Append("ELSE CONVERT(VarChar(50),'未知') ");
                sbd.Append("END) AS [BusTypeName], [t1].[EntryTime] AS [ApplyDate], ");
                sbd.Append("[t1].[LinkTel], ");
                sbd.Append("[t1].[IdCardName] AS [Name],  ");
                sbd.Append("[t1].[IdCardSex] AS [Sex],  ");
                sbd.Append("[t1].[IdCardBirth] AS [BirthDate],  ");
                sbd.Append("[t1].[IdCardNo] AS [IdcardNo],  ");
                sbd.Append("[t1].[IdCardPhoto], ");
                sbd.Append("[t1].[Id], ");
                sbd.Append("[t1].[IdCardIssuedBy], ");
                sbd.Append("[t1].[IdCardValidPeriod], ");
                sbd.Append("[t1].[IdCardAddr] ");
                sbd.Append("FROM [dbo].[CustomerServiceWorkList] AS [t0] ");
                sbd.Append("INNER JOIN [dbo].[TerminalReapplyBusiness] AS [t1] ON ([t0].[BusinessId]) = [t1].[Id] ");
                sbd.Append("WHERE [t0].[UserId] = @UserId and ");
                sbd.Append("[t1].state=@state ");
                sbd.Append("Order by [t1].[EntryTime] asc ");


                SqlCommand cmd = new SqlCommand(sbd.ToString(), conn);
                cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
                cmd.Parameters.Add(new SqlParameter("@state", (int)AtmBusinessState.Handling));
                conn.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable("WorkList");
                    dt.Columns.Add("BusTypeName", typeof(string));
                    dt.Columns.Add("ApplyDate", typeof(DateTime));
                    dt.Columns.Add("LinkTel", typeof(string));
                    dt.Columns.Add("Name", typeof(string));
                    dt.Columns.Add("Sex", typeof(bool));
                    dt.Columns.Add("BirthDate", typeof(DateTime));
                    dt.Columns.Add("IdcardNo", typeof(string));
                    dt.Columns.Add("Photo", typeof(byte[]));
                    dt.Columns.Add("Id", typeof(int));
                    dt.Columns.Add("IssuedBy", typeof(string));
                    dt.Columns.Add("ValidPeriod", typeof(string));
                    dt.Columns.Add("Addr", typeof(string));

                    while (sdr.Read())
                    {
                        DataRow row = dt.NewRow();
                        row["BusTypeName"] = sdr.GetString(0);
                        try
                        {
                            row["ApplyDate"] = sdr.GetDateTime(1);
                        }
                        catch (Exception) { }
                        row["LinkTel"] = sdr.GetString(2);
                        row["Name"] = sdr.GetString(3);
                        row["Sex"] = sdr.GetBoolean(4);
                        try
                        {
                            row["BirthDate"] = sdr.GetDateTime(5);
                        }
                        catch (Exception) { }
                        row["IdcardNo"] = sdr.GetString(6);
                        if (sdr.GetValue(7) != System.DBNull.Value)
                            row["Photo"] = (byte[])sdr.GetValue(7);
                        row["Id"] = sdr.GetInt32(8);
                        row["IssuedBy"] = sdr.GetString(9);
                        row["ValidPeriod"] = sdr.GetString(10);
                        row["Addr"] = sdr.GetString(11);
                        dt.Rows.Add(row);

                    }
                    ds.Tables.Add(dt);
                    return ds;
                }
                else
                    return null;
            }
            //using (DataContext dc = new DataContext(Constr))
            //{

            //    var bbb = (from u in dc.GetTable<CustomerServiceWorkListEntity>()
            //               join b in dc.GetTable<TerminalReapplyBusinessEntity>()
            //               on (int)u.BusinessId equals b.Id
            //               where u.UserId == UserId
            //               select new InnerEnty
            //              {
            //                  BusTypeName = (int)b.BusinessType == 1 ? "行驶证" : ((int)b.BusinessType == 2 ? "驾驶证" : "车牌"),
            //                  ApplyDate = b.EntryTime,
            //                  LinkTel = b.LinkTel,

            //                  Name = b.IdCardName,
            //                  Sex = b.IdCardSex,
            //                  BirthDate = b.IdCardBirth,
            //                  IdcardNo = b.IdCardNo,
            //                  Photo = b.IdCardPhoto.ToArray()
            //              });
            //    string aaaa = dc.GetCommand(bbb).CommandText;

            //    List<InnerEnty> worklist = (from u in dc.GetTable<CustomerServiceWorkListEntity>()
            //                                join b in dc.GetTable<TerminalReapplyBusinessEntity>()
            //                                on (int)u.BusinessId equals b.Id
            //                                where u.UserId == UserId
            //                                select new InnerEnty
            //                               {
            //                                   BusTypeName = (int)b.BusinessType == 1 ? "行驶证" : ((int)b.BusinessType == 2 ? "驾驶证" : "车牌"),
            //                                   ApplyDate = b.EntryTime,
            //                                   LinkTel = b.LinkTel,

            //                                   Name = b.IdCardName,
            //                                   Sex = b.IdCardSex,
            //                                   BirthDate = b.IdCardBirth,
            //                                   IdcardNo = b.IdCardNo,
            //                                   Photo = b.IdCardPhoto.ToArray()
            //                               })
            //                   .ToList<InnerEnty>();
            //    if (worklist != null)
            //    {
            //        DataSet ds = new DataSet();
            //        DataTable dt = new DataTable("WorkList");
            //        dt.Columns.Add("BusTypeName", typeof(string));
            //        dt.Columns.Add("ApplyDate", typeof(DateTime));
            //        dt.Columns.Add("LinkTel", typeof(string));
            //        dt.Columns.Add("Name", typeof(string));
            //        dt.Columns.Add("Sex", typeof(bool));
            //        dt.Columns.Add("BirthDate", typeof(DateTime));
            //        dt.Columns.Add("IdcardNo", typeof(string));
            //        dt.Columns.Add("Photo", typeof(byte[]));

            //        foreach (var obj in worklist)
            //        {
            //            DataRow row = dt.NewRow();
            //            row["BusTypeName"] = obj.BusTypeName;
            //            try
            //            {
            //                row["ApplyDate"] = obj.ApplyDate;
            //            }
            //            catch (Exception) { }
            //            row["LinkTel"] = obj.LinkTel;
            //            row["Name"] = obj.Name;
            //            row["Sex"] = obj.Sex;
            //            try
            //            {
            //                row["BirthDate"] = obj.ApplyDate;
            //            }
            //            catch (Exception) { }
            //            row["IdcardNo"] = obj.IdcardNo;
            //            row["Photo"] = obj.Photo;
            //            dt.Rows.Add(row);
            //        }
            //        ds.Tables.Add(dt);
            //        return ds;
            //    }
            //    return null;
            //}
        }

        /// <summary>
        ///  更新某个业务的状态
        /// </summary>
        /// <param name="busid">业务Id</param>
        /// <param name="state">业务状态</param>
        /// <returns></returns>
        public ServiceRetnEntity UpdateTerminalBusinessState(int busid, AtmBusinessState state, string remarkContent)
        {
            ServiceRetnEntity retnEnty = null;
            using (DataContext dc = new DataContext(Constr))
            {
                retnEnty = new ServiceRetnEntity();
                try
                {
                    var enty = (from item in dc.GetTable<TerminalReapplyBusinessEntity>()
                                where item.Id == busid
                                select item)
                               .FirstOrDefault();
                    if (enty != null)
                    {
                        enty.State = (int)state;
                        enty.UpdateTime = DateTime.Now;
                        enty.Remark = remarkContent;
                        dc.SubmitChanges();
                        retnEnty.Success = true;
                    }
                    else
                    {
                        retnEnty.Success = false;
                        retnEnty.ErrorCode = "01";
                        retnEnty.Message = string.Format("未找到您要处理的业务Id为 {0} 的业务，可能是您超过10分钟未处理该业务，导致此业务已移交。请关闭业务提醒框重试！",
                                                          busid.ToString());
                    }
                }
                catch (SqlException ex)
                {
                    retnEnty.Success = false;
                    retnEnty.ErrorCode = "02";
                    retnEnty.Message = ex.Message;
                }
                catch (Exception ex)
                {
                    retnEnty.Success = false;
                    retnEnty.ErrorCode = "03";
                    retnEnty.Message = ex.Message;
                }
            }
            return retnEnty;
        }

        //public IEnumerable<TerminalAppBusinessEntity> GetTerminalAppBusinessByCondition(AtmBusinessState state,BusinessTypeEnum type) {

        //    using (DataContext dc = new DataContext(Constr)) {
        //        try
        //        {
        //            return (from item in dc.GetTable<TerminalAppBusinessEntity>()
        //                        select item
        //                        ).ToList<TerminalAppBusinessEntity>();
        //        }
        //        catch (SqlException ex) {
        //            return null;
        //        }
        //    }
        //}


        //public IEnumerable<TerminalAppBusinessEntity> GetTerminalAppBusinessAll()
        //{

        //    using (DataContext dc = new DataContext(Constr))
        //    {
        //        try
        //        {
        //            return (from item in dc.GetTable<TerminalAppBusinessEntity>()
        //                    select item
        //                        ).ToList<TerminalAppBusinessEntity>();
        //        }
        //        catch (SqlException ex)
        //        {
        //            return null;
        //        }
        //    }
        //}

        //public Boolean UpDateTerminalAppBusiness(TerminalAppBusinessEntity enty)
        //{

        //    using (DataContext dc = new DataContext(Constr))
        //    {
        //        try
        //        {
        //            var resultEnty = (from item in dc.GetTable<TerminalAppBusinessEntity>()
        //                    where item.ID == enty.ID
        //                    select item
        //                        ).FirstOrDefault();
        //            if (resultEnty != null)
        //            {
        //                resultEnty.State = enty.State;
        //                resultEnty.OpUser = enty.OpUser;
        //                resultEnty.AcceptDate = enty.AcceptDate;
        //                dc.SubmitChanges();

        //                return true;
        //            }
        //            return false;
        //        }
        //        catch (SqlException ex)
        //        {
        //            return false;
        //        }
        //    }
        //}

        #endregion
    }
}
