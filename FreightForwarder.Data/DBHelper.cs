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
        public static IEnumerable<Company> GetAllCompanies() {
            using (FFDBContext context = new FFDBContext())
            {
                try
                {
                    return context.Companies.ToList();
                }
                catch (Exception ex)
                {
                    return null;
                }
            } 
        }

        public static bool AddRouteInformationItems(IEnumerable<RouteInformationItem> rlist){

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

                                item.UpdateDate = DateTime.Now;
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
                    return false;
                }
            }
        }

        public static IList<RouteInformationItem> GetRouteInformationItems(string companyCode) {

            using (FFDBContext context = new FFDBContext())
            {
                try
                {
                    return context.RouteItems
                        .Include(ri => ri.Company) //预先加载
                        .Where(ri=>!ri.IsDeleted)
                        .ToList();
                }
                catch (Exception ex)
                {
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
                    if (!string.IsNullOrEmpty(shipName)) {
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
                    else if(isSingleContainer.HasValue && !isSingleContainer.Value)
                    {
                        rlist = rlist.Where(ri => ri.IsSingleContainer == (int)IsSingleContainerValues.No);
                    }

                    return rlist
                        .ToList();
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
