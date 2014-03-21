using _2_2aventyrliga_kontakter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2_2aventyrliga_kontakter
{
    public partial class Default : System.Web.UI.Page
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IEnumerable<Contact> ContactListView_GetData()
        {
            return Service.GetContacts();
        }

        public void ContactListView_InsertItem(Contact contact)
        {
            try
            {
                Service.SaveContact(contact);
            }
            catch (Exception)
            {
                //Det här kräver ValidationSummary
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade när kontakten skulle läggas till");
            }
        }
        // The id parameter name should match the DataKeyNames value set on the control
        public void ContactListView_UpdateItem(int ContactId)
        {
            try
            {
                var contact = Service.GetContact(ContactId);
                
                //Det här hade inte kunnat göras om jag kastat undantag i anropad metod 
                //istället för att returnera null 
                if (contact == null)
                {
                    //Kontakten hittades inte
                    ModelState.AddModelError(String.Empty, 
                        String.Format("Kontakt med kontaktnummer {0} gittades inte", ContactId));
                    return;
                }
                
                if (TryUpdateModel(contact))
                {
                    Service.SaveContact(contact);
                }
            }
            catch (Exception)
            {
                //Det här kräver ValidationSummary
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade när kontakten skulle läggas till");
            }
        }

        // OBS! The id parameter name should match the DataKeyNames value set on the control
        public void ContactListView_DeleteItem(int ContactId)
        {
            try
            {
                var contact = Service.GetContact(ContactId);

                //Det här hade inte kunnat göras om jag kastat undantag i anropad metod 
                //istället för att returnera null 
                if (contact == null)
                {
                    //Kontakten hittades inte
                    ModelState.AddModelError(String.Empty,
                        String.Format("Kontakt med kontaktnummer {0} gittades inte", ContactId));
                    return;
                }

                if (TryUpdateModel(contact))
                {
                    Service.DeleteContact(ContactId);
                }
            }
            catch (Exception)
            {
                //Det här kräver ValidationSummary
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade när kontakten skulle tas bort");
            }
            //try
            //{
            //    Service.DeleteContact(ContactId);
            //}
            //catch (Exception)
            //{
            //    //Det här kräver ValidationSummary
            //    ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade när kontakten skulle tas bort");
            //}
        }

    }
}