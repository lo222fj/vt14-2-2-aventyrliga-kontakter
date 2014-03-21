using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace _2_2aventyrliga_kontakter.Model.DAL
{
    public class ContactDAL
    {
        private static readonly string _connectionString;

        //Konstruktor
        static ContactDAL()
        {
            _connectionString = WebConfigurationManager.ConnectionStrings["1dv406_AdventureWorksAssignmentConnectionString"].ConnectionString;
        }

        //Skapar och initierar nytt anslutningsobjekt. Returnerar referens till det.
        private static SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        //Hämtar alla kontakter
        public IEnumerable<Contact> GetContacts()
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var contacts = new List<Contact>(100);

                    var cmd = new SqlCommand("Person.uspGetContacts", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var contactIdIndex = reader.GetOrdinal("ContactID");
                        var firstNameIndex = reader.GetOrdinal("FirstName");
                        var lastNameIndex = reader.GetOrdinal("LastName");
                        var emailAddressIndex = reader.GetOrdinal("EmailAddress");

                        while (reader.Read())
                        {
                            contacts.Add(new Contact
                                {
                                    ContactId = reader.GetInt32(contactIdIndex),
                                    FirstName = reader.GetString(firstNameIndex),
                                    LastName = reader.GetString(lastNameIndex),
                                    EmailAddress = reader.GetString(emailAddressIndex)
                                });
                        }
                    }
                    //Anpassar kapaciteten till faktiskt antal element i 
                    //List-objektet = avallokerr minne som inte används
                    contacts.TrimExcess();

                    return contacts;
                }

                catch
                {
                    throw new ApplicationException("Ett fel inträffade när kontakterna skulle hämtas från databasen");
                }
            }
        }
        //Hämtar en kontakt
        public Contact GetContactById(int contactId)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var cmd = new SqlCommand("Person.uspGetContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Proceduren kräver en parameter
                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contactId;

                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var contactIdIndex = reader.GetOrdinal("ContactID");
                        var firstNameIndex = reader.GetOrdinal("FirstName");
                        var lastNameIndex = reader.GetOrdinal("LastName");
                        var emailAddressIndex = reader.GetOrdinal("EmailAddress");

                        if (reader.Read())
                        {
                            return new Contact
                            {
                                ContactId = reader.GetInt32(contactIdIndex),
                                FirstName = reader.GetString(firstNameIndex),
                                LastName = reader.GetString(lastNameIndex),
                                EmailAddress = reader.GetString(emailAddressIndex)
                            };
                        }
                    }
                    //om readern inte hittar några rader returneras null. Ett annat
                    //alternativ är att kasta ett undantag. Vad man väljer att
                    // göra är beroende på vad man vill göra. Man vill inte att 
                    //undantag kastas ofta och om det faktiskt inte är ett fel.
                    return null;
                }

                catch
                {
                    throw new ApplicationException("Ett fel inträffade när kontakten skulle hämtas från databasen");
                }
            }
        }

        public void InsertContact(Contact contact)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    //Skapar och initierar ett SqlCommand-objekt. Det används till att
                    //exekvera en specifierad lagrad procedur
                    var cmd = new SqlCommand("Person.uspAddContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Proceduren kräver en parameter
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = contact.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = contact.LastName;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = contact.EmailAddress;
                    
                    //Hämtar data från proceduren. Output-parameter i den.
                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Direction= ParameterDirection.Output;

                    conn.Open();

                    //Insert-sats. Returnerar inga poster => 
                    //Metoden ExecuteNonQuery används för att exekvera procedurer
                    cmd.ExecuteNonQuery();

                    //Hämtar primärnyckelns värde för den nya posten och tillelar värdet till Contact-objektet.
                    contact.ContactId = (int)cmd.Parameters["@ContactId"].Value;
                }

                catch
                {
                    throw new ApplicationException("Ett fel inträffade när kontakten skulle läggas till i databasen");
                }
            }
        }

        public void UpdateContact(Contact contact)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    //Skapar och initierar ett SqlCommand-objekt. Det används till att
                    //exekvera en specifierad lagrad procedur
                    var cmd = new SqlCommand("Person.uspUpdateContact", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Proceduren kräver en parameter
                    cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contact.ContactId;
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = contact.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = contact.LastName;
                    cmd.Parameters.Add("@EmailAddress", SqlDbType.VarChar, 50).Value = contact.EmailAddress;

                    conn.Open();

                    //Update-sats. Returnerar inga poster => 
                    //Metoden ExecuteNonQuery används för att exekvera procedurer
                    cmd.ExecuteNonQuery();
                }

                catch
                {
                    throw new ApplicationException("Ett fel inträffade när kontakten skulle uppdateras i databasen");
                }
            }
        }

        public void DeleteContact(int contactId)
        {
            {
                using (var conn = CreateConnection())
                {
                    try
                    {
                        //Skapar och initierar ett SqlCommand-objekt. Det används till att
                        //exekvera en specifierad lagrad procedur
                        var cmd = new SqlCommand("Person.uspRemoveContact", conn);
                        cmd.CommandType = CommandType.StoredProcedure;

                        //Proceduren kräver en parameter
                        cmd.Parameters.Add("@ContactID", SqlDbType.Int, 4).Value = contactId;
                        
                        conn.Open();

                        //Delete-sats. Returnerar inga poster => 
                        //Metoden ExecuteNonQuery används för att exekvera procedurer
                        cmd.ExecuteNonQuery();
                    }

                    catch
                    {
                        throw new ApplicationException("Ett fel inträffade när kontakten skulle uppdateras i databasen");
                    }
                }
            }
        }
    }
}