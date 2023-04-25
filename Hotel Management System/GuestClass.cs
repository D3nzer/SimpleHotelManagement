using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Hotel_Management_System
{
 
    class GuestClass
    {
        DBConnect connect = new DBConnect();
       
        public bool insertGuest(string id, string fname, string lname, string phone, string city)
        {
            string insertQuerry = "INSERT INTO `guest`(`GuestId`, `GuestFirstName`, `GuestLastName`, `GuestPhone`, `GuestCity`) VALUES(@id,@fname,@lname,@ph,@ct)";
            MySqlCommand command = new MySqlCommand(insertQuerry, connect.GetConnection());
            //@id,@fname,@lname,@ph,@ct
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
            command.Parameters.Add("@fname", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@lname", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@ct", MySqlDbType.VarChar).Value = city;

            connect.OpenCon();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.CloseCon();
                return true;
            }
            else
            {
                connect.CloseCon();
                return false;
            }

        }

        // Create a function to get the guest list
        public DataTable getGuest()
        {
            string selectQuery = "SELECT * FROM `guest`";
            MySqlCommand command = new MySqlCommand(selectQuery, connect.GetConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }
        // now create a function for edit guest
        public bool editGuest(string id, string fname, string lname, string phone, string city)
        {
            string editQuerry = "UPDATE `guest` SET `GuestFirstName`=@fname,`GuestLastName`=@lname,`GuestPhone`=@ph,`GuestCity`=@ct WHERE `GuestId`=@id";
            MySqlCommand command = new MySqlCommand(editQuerry, connect.GetConnection());
            //@id,@fname,@lname,@ph,@ct
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
            command.Parameters.Add("@fname", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@lname", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@ph", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@ct", MySqlDbType.VarChar).Value = city;

            connect.OpenCon();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.CloseCon();
                return true;
            }
            else
            {
                connect.CloseCon();
                return false;
            }

        }

        // now create a function to delete the selected guest
        // we only need the guest id

        public bool removeGuest(string id)
        {
            string insertQuerry = "DELETE FROM `guest` WHERE `GuestId`=@id";
            MySqlCommand command = new MySqlCommand(insertQuerry, connect.GetConnection());
            //@id
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
            connect.OpenCon();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.CloseCon();
                return true;
            }
            else
            {
                connect.CloseCon();
                return false;
            }

        }
    }
}
