using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace wpf_demo_phonebook
{
    class PhonebookDAO
    {
        private DbConnection conn;

        public PhonebookDAO()
        {
            conn = new DbConnection();
        }

        /// <summary>
        /// Méthode permettant de rechercher un contact par nom
        /// </summary>
        /// <param name="_name">Nom de famille ou prénom</param>
        /// <returns>Une DataTable</returns>
        public DataTable SearchByName(string _name)
        {
            string _query =
                $"SELECT * " +
                $"FROM [Contacts] " +
                $"WHERE FirstName LIKE @firstName OR LastName LIKE @lastName ";

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@firstName", SqlDbType.NVarChar);
            parameters[0].Value = _name;

            parameters[1] = new SqlParameter("@lastName", SqlDbType.NVarChar);
            parameters[1].Value = _name;

            return conn.ExecuteSelectQuery(_query, parameters);
        }

        /// <summary>
        /// Méthode permettant de rechercher un contact par id
        /// </summary>
        /// <param name="_name">Nom de famille ou prénom</param>
        /// <returns>Une DataTable</returns>
        public DataTable SearchByID(int _id)
        {
            string _query =
                $"SELECT * " +
                $"FROM [Contacts] " +
                $"WHERE ContactID = @_id ";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@_id", SqlDbType.Int);
            parameters[0].Value = _id;

            return conn.ExecuteSelectQuery(_query, parameters);
        }

        public DataTable GetAll()
        {
            string _query =
                $"SELECT * " +
                $"FROM [Contacts] ";

            return conn.ExecuteSelectQuery(_query, null);
        }

        public void Edit(int _id, ContactModel cm)
        {

            string _query =
                $"UPDATE Contacts " +
                $"SET FirstName = '{cm.FirstName}­', " +
                    $"LastName = '{cm.LastName}', " +
                    $"Email = '{cm.Email}', " +
                    $"Phone = '{cm.Phone}', " +
                    $"Mobile = '{cm.Mobile}' " +
                $"WHERE ContactId = @_id";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@_id", SqlDbType.Int);
            parameters[0].Value = _id;

            conn.ExecuteUpdateQuery(_query, parameters);

        }

        public void Remove(int _id)
        {

            string _query =
                $"Delete " +
                $"FROM [Contacts] " +
                $"WHERE ContactId = @_id";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@_id", SqlDbType.Int);
            parameters[0].Value = _id;

            conn.ExecuteDeleteQuery(_query, parameters);

        }

        public int Add(ContactModel cm)
        {
            // 
            string _query =
                $"Insert " +
                $"Into [Contacts] (FirstName, LastName, Email, Phone, Mobile) " +
                $"OUTPUT INSERTED.ContactID " +
                $"Values ('{cm.FirstName}­', '{cm.LastName}', '{cm.Email}', '{cm.Phone}', '{cm.Mobile}')";

            return conn.ExecuteInsertQuery(_query, null);
        }

    }
}
