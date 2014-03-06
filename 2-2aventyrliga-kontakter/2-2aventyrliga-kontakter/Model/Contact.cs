using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2_2aventyrliga_kontakter.Model
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}