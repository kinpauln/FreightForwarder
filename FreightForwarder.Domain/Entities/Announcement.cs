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
    [Table("Announcement")]
    [Serializable]
    [DataContract]
    public class Announcement
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [DataMember]
        public string InfoContent { get; set; }

        [DataMember]
        public DateTime? CreateDate { get; set; }
    }
}
