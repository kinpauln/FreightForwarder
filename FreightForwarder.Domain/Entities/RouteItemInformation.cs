using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace FreightForwarder.Domain.Entities
{
    [Table("RouteInformationItems")]
    [DataContract]
    public partial class RouteInformationItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 船名
        /// </summary>
        [DataMember]
        public string ShipName { get; set; }

        /// <summary>
        /// 起运港
        /// </summary>
        [DataMember]
        public string StartPort { get; set; }

        /// <summary>
        /// 目的港
        /// </summary>
        [DataMember]
        public string DestinationPort { get; set; }

        /// <summary>
        /// 船期
        /// </summary>
        [DataMember]
        public string StartDay { get; set; }

        /// <summary>
        /// 20GP
        /// </summary>
        [Column("20GP")]
        [DataMember]
        public float Price_20GP { get; set; }

        /// <summary>
        /// 40GP
        /// </summary>
        [Column("40GP")]
        [DataMember]
        public float Price_40GP { get; set; }

        /// <summary>
        /// 40HQ
        /// </summary>
        [Column("40HQ")]
        [DataMember]
        public float Price_40HQ { get; set; }

        /// <summary>
        /// 直达
        /// </summary>
        [DataMember]
        public int Nonstop { get; set; }

        /// <summary>
        /// 航程
        /// </summary>
        [DataMember]
        public string SailDayLength { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        [DataMember]
        public DateTime ValidDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remarks { get; set; }

        /// <summary>
        /// 整柜/拼箱
        /// </summary>
        [DataMember]
        public int IsSingleContainer { get; set; }

        /// <summary>
        /// 隶属的货代公司
        /// </summary>
        [DataMember]
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        [DataMember]
        public Company Company { get; set; }//且记，若加virtual关键字表示延迟加载

        /// <summary>
        ///  创建日期
        /// </summary>
        [DataMember]
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///  更新日期
        /// </summary>
        [DataMember]
        public DateTime UpdateDate { get; set; }

        /// <summary>
        ///  删除日期
        /// </summary>
        [DataMember]
        public DateTime? DeleteDate { get; set; }

        /// <summary>
        ///  删除日期
        /// </summary>
        [DataMember]
        public bool IsDeleted { get; set; }
    }
}
