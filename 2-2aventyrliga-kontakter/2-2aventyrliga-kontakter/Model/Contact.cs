using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _2_2aventyrliga_kontakter.Model
{
    public class Contact
    {
        public int ContactId { get; set; }

        [Required(ErrorMessage="En e-mailadress måste anges")]
        [StringLength(50)]
        [RegularExpression (@"^(?!\.)(\w|-|\.){1,64}(?!\.)@(?!\.)[-.a-zåäö0-9]{4,253}$")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage="Ett förnamn måste anges")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage="Ett efternamn måste anges")]
        [StringLength(50)]
        public string LastName { get; set; }
    }
}