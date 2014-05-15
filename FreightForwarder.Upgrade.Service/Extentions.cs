using FreightForwarder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace FreightForwarder.Upgrade.Service
{
    public static class Extentions
    {
        public static XmlElement ToXmlElement(this UpgradePackage packageobj, XmlDocument doc)
        {
            XmlElement pkElement = doc.CreateElement("Package");

            XmlElement fnElement = doc.CreateElement("FileName");
            fnElement.InnerText = packageobj.FileName;
            pkElement.AppendChild(fnElement);

            XmlElement fvElement = doc.CreateElement("FileVersion");
            fvElement.InnerText = packageobj.FileVersion;
            pkElement.AppendChild(fvElement);

            XmlElement stElement = doc.CreateElement("SavingType");
            stElement.InnerText = packageobj.SavingType.ToString();
            pkElement.AppendChild(stElement);

            XmlElement ptElement = doc.CreateElement("PostTime");
            ptElement.InnerText = packageobj.PostTime.ToString("yyyy-MM-dd HH:mm:ss");
            pkElement.AppendChild(ptElement);

            return pkElement;
        }

        /// <summary>
        /// 从XML节点初始化实体
        /// </summary>
        /// <param name="node"></param>
        public static UpgradePackage InitFromXmlNode(this UpgradePackage packageobj, XmlNode node)
        {
            XmlNode fnNode = node.SelectSingleNode("FileName");
            packageobj.FileName = fnNode.InnerText;

            XmlNode fvNode = node.SelectSingleNode("FileVersion");
            packageobj.FileVersion = fvNode.InnerText;

            XmlNode stNode = node.SelectSingleNode("SavingType");
            packageobj.SavingType = int.Parse(stNode.InnerText);

            XmlNode ptNode = node.SelectSingleNode("PostTime");
            packageobj.PostTime = DateTime.Parse(ptNode.InnerText);
            
            return packageobj;
        }
    }
}