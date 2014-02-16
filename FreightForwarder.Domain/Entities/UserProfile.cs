using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FreightForwarder.Domain.Entities
{
    [Table("UserProfile")]
    public partial class UserProfile
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Password")]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [Column("Email")]
        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        [MaxLength(300)]
        public string FirstName { get; set; }

        /// <summary>
        /// asfdsdf
        /// </summary>
        [MaxLength(300)]
        public string Surname { get; set; }

        /// <summary>
        /// Street/PO Box
        /// </summary>
        [Column("Street")]
        [MaxLength(300)]
        [Display(Name = "Street/PO Box")]
        public string StreetPOBox { get; set; }

        /// <summary>
        /// Town/City/Suburb
        /// </summary>
        [Column("City")]
        [MaxLength(300)]
        [Display(Name = "Town/City/Suburb")]
        public string TownCitySuburb { get; set; }

        /// <summary>
        /// State/Territory
        /// </summary>
        [MaxLength(300)]
        [Display(Name = "State/Territory")]
        public string StateTerritory { get; set; }

        /// <summary>
        /// Postcode
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "Postcode")]
        public string Postcode { get; set; }

        /// <summary>
        /// Phone: home
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "Phone: home")]
        public string PhoneHome { get; set; }

        /// <summary>
        /// Phone: business
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "Phone: business")]
        public string PhoneBusiness { get; set; }
    }
}
