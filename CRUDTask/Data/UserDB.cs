using System.Data.SqlClient;
using System.Data;

namespace CRUDTask.Data
{
    public class UserDB
    {
        string _connectionString = string.Empty;
        public UserDB(string connnectionString)
        {
            _connectionString = connnectionString;
        }

        public bool LogIn(UserModel user)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            try
            {
                string commandText = $"Select * from UserDetails where Id = {user.Id} ";
                SqlCommand cmd = new SqlCommand(commandText, con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                UserModel model = null;

                while (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        model = new UserModel()
                        {
                            UserName = (string) reader["UserName"],
                            Password = (string)reader["Password"]
                        };
                        break;
                    }
                }

                if (model.UserName == user.UserName && model.Password == user.Password)
                {
                    return true;
                }


            }
            catch (Exception ex) { }
            finally
            {
                if (con != null)
                    con.Close();
            }

            return false;
        }
    }
}
