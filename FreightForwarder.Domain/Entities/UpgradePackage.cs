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
    [Table("UpgradePackage")]
    [Serializable]
    [DataContract]
    public class UpgradePackage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [DataMember]
        public string FileVersion { get; set; }

        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public byte[] FileBytes { get; set; }

        [DataMember]
        public DateTime PostTime { get; set; }

        [DataMember]
        public int SavingType { get; set; }
    }
}
