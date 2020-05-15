using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace wpf_demo_phonebook
{
    static class PhoneBookBusiness
    {
        private static PhonebookDAO dao = new PhonebookDAO();

        public static ContactModel GetContactByName(string _name)
        {
            ContactModel cm = null;

            DataTable dt = new DataTable();

            dt = dao.SearchByName(_name);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cm = RowToContactModel(row);
                }
            }

            return cm;
        }

        public static ContactModel GetContactByID(int _id)
        {
            ContactModel cm = null;

            DataTable dt = new DataTable();

            dt = dao.SearchByID(_id);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cm = RowToContactModel(row);
                }
            }

            return cm;
        }

        public static ObservableCollection<ContactModel> GetContacts()
        {
            ContactModel cm;
            ObservableCollection<ContactModel> contacts = new ObservableCollection<ContactModel>();
            DataTable dt = new DataTable();

            dt = dao.GetAll();

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    cm = RowToContactModel(row);
                    contacts.Add(cm);
                }
            }
            return contacts;
        }

        private static ContactModel RowToContactModel(DataRow row)
        {
            ContactModel cm = new ContactModel();

            cm.ContactID = Convert.ToInt32(row["ContactID"]);
            cm.FirstName = row["FirstName"].ToString();
            cm.LastName = row["LastName"].ToString();
            cm.Email = row["Email"].ToString();
            cm.Phone = row["Phone"].ToString();
            cm.Mobile = row["Mobile"].ToString();

            return cm;
        }

        public static int AddContact(ContactModel _cm)
        {
            int nbAdd = 0;
            nbAdd = dao.Add(_cm);

            return nbAdd;
        }

    }
}
