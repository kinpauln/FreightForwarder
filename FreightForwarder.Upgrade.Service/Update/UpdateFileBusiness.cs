using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using FreightForwarder.Domain.Entities;
using FreightForwarder.Business;
using System.Xml;

namespace FreightForwarder.Upgrade.Service
{
    public static class UpgradePackageBusiness
    {
        public static IEnumerable<UpgradePackage> Select(int? startRowIndex, int? maximumRows, int? SavingType)
        {
            try
            {
                if (SavingType.HasValue && ((int)FreightForwarder.Common.PackageSavingType.DB == SavingType.Value))
                {
                    return (new PackageBusinesses()).GetUpgradePackages();
                }
                else {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int SelectCount(int? SavingType)
        {
            try
            {
                return (new PackageBusinesses()).GetUpgradePackageCount();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Insert(UpgradePackage UpgradePackage)
        {
            try
            {
                // 存DB
                if ((int)FreightForwarder.Common.PackageSavingType.DB == UpgradePackage.SavingType)
                {
                    (new PackageBusinesses()).AddUpgradePackage(UpgradePackage);
                }
                else
                {
                    string folder = System.Web.HttpContext.Current.Server.MapPath("~/packages");
                    string filename = System.IO.Path.Combine(folder, UpgradePackage.FileName);

                    System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                    fs.Write(UpgradePackage.FileBytes, 0, UpgradePackage.FileBytes.Length);
                    fs.Flush();
                    fs.Close();

                    string configfile = System.Web.HttpContext.Current.Server.MapPath("~/upgrade-packages.xml");
                    //XmlDocument doc = XMLHelper.xmlDoc(configfile);

                    XmlDocument doc = new XmlDocument();
                    doc.Load(configfile);
                    XmlNode pcnode = doc.SelectSingleNode("Packages/PackageCollection");
                    pcnode.AppendChild(UpgradePackage.ToXmlElement(doc));
                    doc.Save(configfile);
                    //XmlNodeList partlistNode = assistXml.SelectNodes("Assist/PartCollection/Part");
                    
                    //foreach (XmlNode node in partlistNode)
                    //{
                    //    Part part = new Part();
                    //    part.FromXmlNode(node);
                    //    Parts.Add(part);
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Delete(UpgradePackage UpgradePackage)
        {
            try
            {
                bool result = (new PackageBusinesses()).DeletePackage(UpgradePackage.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
