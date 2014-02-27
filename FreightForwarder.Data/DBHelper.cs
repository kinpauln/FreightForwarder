using FreightForwarder.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using FreightForwarder.Common;
using System.Data.Common;
using System.Data.Entity.Infrastructure;

namespace FreightForwarder.Data
{
    public class DBHelper
    {
        #region Company
        public static IEnumerable<Company> GetAllCompanies()
        {
            using (FFDBContext context = new FFDBContext())
            {
                try
                {
                    return context.Companies.ToList();
                }
                catch (Exception ex)
                {
                    Logger.Error("获取所有货代公司失败", ex);
                    return null;
                }
            }
        }

        public static bool AddCompany(string companyName, string companyCode)
        {

            using (FFDBContext context = new FFDBContext())
            {
                try
                {
                    context.Companies.Add(new Company()
                    {
                        Code = companyCode,
                        Name = companyName
                    });
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Error("添加货代公司失败", ex);
                    return false;
                }
            }
        }
        #endregion

        #region RouteInformationItems
        public static bool AddRouteInformationItems(IEnumerable<RouteInformationItem> rlist)
        {

            using (FFDBContext context = new FFDBContext())
            {
                try
                {
                    foreach (RouteInformationItem item in rlist)
                    {
                        context.RouteItems.Add(item);
                    }
                    context.SaveChanges();

                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Error("向数据库中添加新纪录失败", ex);
                    return false;
                }
            }
        }

        public static bool ImportRouteInformationItems(IEnumerable<RouteInformationItem> importlist)
        {
            // insert列表
            IList<RouteInformationItem> insertlist = importlist.Where(ri => ri.Id == 0).ToList();
            // update列表
            IList<RouteInformationItem> updatelist = importlist.Where(ri => ri.Id != 0).ToList();

            using (FFDBContext context = new FFDBContext())
            {
                DbTransaction tran = null;
                try
                {
                    using (DbConnection conn = ((IObjectContextAdapter)context).ObjectContext.Connection)
                    {
                        conn.Open();
                        using (tran = conn.BeginTransaction())
                        {
                            // insert
                            foreach (RouteInformationItem item in insertlist)
                            {
                                item.CreateDate = DateTime.Now;
                                item.UpdateDate = DateTime.Now;
                                item.IsDeleted = false;

                                context.RouteItems.Add(item);
                            }

                            // 要更新的所有记录ID
                            int[] idInFiles = importlist.Where(ri => ri.Id != 0).Select(ri => ri.Id).ToArray();

                            // 数据库中要更新的数据
                            IEnumerable<RouteInformationItem> db_updatelist = context.RouteItems.Where(ri => idInFiles.Contains(ri.Id)).ToList();
                            foreach (RouteInformationItem item in updatelist)
                            {
                                RouteInformationItem dbitem = db_updatelist.Single(ri => ri.Id == item.Id);
                                dbitem.ShipName = item.ShipName;
                                dbitem.StartPort = item.StartPort;
                                dbitem.DestinationPort = item.DestinationPort;
                                dbitem.StartDay = item.StartDay;
                                dbitem.Price_20GP = item.Price_20GP;
                                dbitem.Price_40GP = item.Price_40GP;
                                dbitem.Price_40HQ = item.Price_40HQ;
                                dbitem.Nonstop = item.Nonstop;
                                dbitem.SailDayLength = item.SailDayLength;
                                dbitem.ValidDate = item.ValidDate;
                                dbitem.Remarks = item.Remarks;
                                dbitem.IsSingleContainer = item.IsSingleContainer;

                                dbitem.UpdateDate = DateTime.Now;
                            }

                            // 参与重新导入的所有企业ID
                            int[] companyIDs = importlist.Select(ri => ri.CompanyId).Distinct().ToArray<int>();
                            // 数据库中现存的所有要参与重新导入企业的旧的信息
                            int[] oldDestinationIDs = context.RouteItems.Where(ri => companyIDs.Contains<int>(ri.CompanyId)).Select(ri => ri.Id).ToArray<int>();
                            // 要删除的 数据库ID
                            int[] deleteIDs = oldDestinationIDs.Except(idInFiles).ToArray();

                            IEnumerable<RouteInformationItem> deletelist = context.RouteItems.Where(ri => deleteIDs.Contains(ri.Id));
                            // delete
                            foreach (RouteInformationItem item in deletelist)
                            {
                                // context.RouteItems.Remove(item);
                                item.IsDeleted = true;
                                item.DeleteDate = DateTime.Now;
                            }

                            context.SaveChanges();

                            // 提交事务
                            tran.Commit();
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    // 回滚事务
                    tran.Rollback();
                    Logger.Error("批量更新数据失败", ex);
                    return false;
                }
            }
        }

        public static IList<RouteInformationItem> GetRouteInformationItems(int? companyId)
        {
            using (FFDBContext context = new FFDBContext())
            {
                try
                {
                    var query = context.RouteItems
                        .Include(ri => ri.Company) //预先加载
                        .Where(ri => !ri.IsDeleted);
                    if (companyId.HasValue)
                    {
                        query = query.Where(ri => ri.CompanyId == companyId.Value);
                    }
                    return query.ToList();
                }
                catch (Exception ex)
                {
                    Logger.Error("根据货代公司ID检索记录失败", ex);
                    return null;
                }
            }
        }

        public static IList<RouteInformationItem> GetRouteInformationItems(string shipName, string startPort, string destinationPort, bool? isSingleContainer)
        {

            using (FFDBContext context = new FFDBContext())
            {
                try
                {
                    var rlist = context.RouteItems
                        .Include(ri => ri.Company) //预先加载
                        .Where(ri => !ri.IsDeleted);

                    // 船名
                    if (!string.IsNullOrEmpty(shipName))
                    {
                        //importlist = importlist.WhereOrLike(ri => ri.ShipName, new string[] { shipName });
                        rlist = rlist.Where(ri => ri.ShipName.Contains(shipName));
                    }

                    // 起始港
                    if (!string.IsNullOrEmpty(startPort))
                    {
                        //importlist = importlist.WhereOrLike(ri => ri.StartPort, new string[] { startPort });
                        rlist = rlist.Where(ri => ri.StartPort.Contains(startPort));
                    }

                    // 目的港
                    if (!string.IsNullOrEmpty(destinationPort))
                    {
                        //importlist = importlist.WhereOrLike(ri => ri.DestinationPort, new string[] { destinationPort });
                        rlist = rlist.Where(ri => ri.DestinationPort.Contains(destinationPort));
                    }

                    // 整柜/拼箱
                    if (isSingleContainer.HasValue && isSingleContainer.Value)
                    {
                        rlist = rlist.Where(ri => ri.IsSingleContainer == (int)IsSingleContainerValues.Yes);
                    }
                    else if (isSingleContainer.HasValue && !isSingleContainer.Value)
                    {
                        rlist = rlist.Where(ri => ri.IsSingleContainer == (int)IsSingleContainerValues.No);
                    }

                    return rlist
                        .ToList();
                }
                catch (Exception ex)
                {
                    Logger.Error("模糊查询失败", ex);
                    return null;
                }
            }
        }
        #endregion

        #region RegCodes

        public static bool AssociatMachineAndRegCode(string machineCode, string regcode, int companyId)
        {
            using (FFDBContext context = new FFDBContext())
            {
                try
                {
                    RegisterCode entity = context.RegisterCodes.Where(rc => rc.MachineCode.ToLower().Equals(machineCode.ToLower())).FirstOrDefault();
                    if (entity != null)
                    {
                        entity.RegCode = regcode;
                        entity.CompanyId = companyId;
                        entity.State = (int)RegCodeStates.Actived;

                        context.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    Logger.Error("服务端关联公司ID和机器码失败", ex);
                    return false;
                }
            }
        }

        public static bool ExistedEntity(string machineCode, int companyId)
        {
            using (FFDBContext context = new FFDBContext())
            {
                try
                {
                    return context.RegisterCodes.Where(rc => rc.MachineCode.Equals(machineCode) && rc.CompanyId.Equals(companyId)).Count() > 0 ? true : false;
                }
                catch (Exception ex)
                {
                    Logger.Error("根据ID和机器码判断是否已存在失败", ex);
                    return true;
                }
            }
        }

        public static bool AddMachineCode(string machineCode, int companyId)
        {
            using (FFDBContext context = new FFDBContext())
            {
                try
                {
                    context.RegisterCodes.Add(new RegisterCode()
                    {
                        MachineCode = machineCode,
                        CreatedDate = DateTime.Now,
                        CompanyId = companyId,
                        State = (int)RegCodeStates.UnActived
                    });
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Error("关联机器码和货代公司ID失败", ex);
                    return false;
                }
            }
        }

        public static bool AddRegCode(string machineCode, string regCode, int companyId)
        {
            using (FFDBContext context = new FFDBContext())
            {
                try
                {
                    context.RegisterCodes.Add(new RegisterCode()
                    {
                        MachineCode = machineCode,
                        RegCode = regCode,
                        CompanyId = companyId,
                        CreatedDate = DateTime.Now,
                        State = (int)RegCodeStates.Actived
                    });
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    Logger.Error("添加注册码失败", ex);
                    return false;
                }
            }
        }
        #endregion

        public static RegisterCode SoftwareIsRegistered(string machineCode)
        {
            using (FFDBContext context = new FFDBContext())
            {
                try
                {
                    return context.RegisterCodes
                        .Include(ri => ri.Company) //预先加载
                        .Where(rc => rc.MachineCode.Trim().Equals(machineCode)).SingleOrDefault();
                }
                catch (Exception ex)
                {
                    Logger.Error("判断机器码是否在数据库中已经注册失败", ex);
                    return null;
                }
            }
        }
    }
}
