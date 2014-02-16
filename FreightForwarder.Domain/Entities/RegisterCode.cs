using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreightForwarder.Domain.Entities
{
    [Table("RegisterCodes")]
    public partial class RegisterCode
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }
        
        /// <summary>
        /// 机器码
        /// </summary>
        [MaxLength(300)]
        [Unique]
        public string MachineCode { get; set; }

        /// <summary>
        /// 注册码
        /// </summary>
        [MaxLength(300)]
        [Unique]
        public string RegCode { get; set; }

        /// <summary>
        /// 隶属的货代公司
        /// </summary>
        public int CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public Company Company { get; set; }//且记，若加virtual关键字表示延迟加载

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// 有效期截止日
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
    }
}
