using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreightForwarder.Domain.Entities
{
    [Table("Company")]
    public partial class Company
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        //[Column("Password")]
        //[Display(Name = "Password")]
        //[Required(ErrorMessage = "Password is required")]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        /// <summary>
        /// Code
        /// </summary>
        [MaxLength(300)]
        [Unique]
        public string Code { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [MaxLength(300)]
        public string Name { get; set; }

    }
}
