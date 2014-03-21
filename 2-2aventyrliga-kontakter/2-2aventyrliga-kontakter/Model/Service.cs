using _2_2aventyrliga_kontakter.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2_2aventyrliga_kontakter.Model
{
    public class Service
    {
        private ContactDAL _contactDAL;

        private ContactDAL ContactDAL
        {
            get { return _contactDAL ?? (_contactDAL = new ContactDAL()); }
            //finns det något i _contactDAL? Är det null returneras default(det till höger)
        }
        public Contact GetContact(int contactId)
        {
            return ContactDAL.GetContactById(contactId);
        }

        public IEnumerable<Contact> GetContacts()
        {
            return ContactDAL.GetContacts();
        }

        //Om kontakten finns i databasen är ContactId en siffra över 0 
        //Om kontakten inte fanns utan har initierats nyligen är ContactId 0 eftersom
        //den är av typen int som har standardvärde 0
        public void SaveContact(Contact contact)
        {
            if (contact.ContactId == 0)
            {
                ContactDAL.InsertContact(contact);
            }
            else
            {
                ContactDAL.UpdateContact(contact);
            }
        }
        public void DeleteContact(int contactId)
        {
            {
                ContactDAL.DeleteContact(contactId);
            }

        }
    }
}