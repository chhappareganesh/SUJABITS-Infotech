using System.Data.SqlClient;
using System.Data;

namespace CRUDTask.Data
{
    public class EmployeeDB
    {
        string _connectionString = string.Empty;
        public EmployeeDB(string connnectionString)
        {
            _connectionString = connnectionString;
        }

        public List<EmployeeModel> Employees()
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();

            SqlConnection con = new SqlConnection(_connectionString);
            try
            {
                string commandText = "Usp_GetAllEmployee";
                SqlCommand cmd = new SqlCommand(commandText, con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        employees.Add(new EmployeeModel()
                        {
                            EmployeeId = reader.GetInt32("EmployeeId"),
                            EmployeeName = reader.GetString("EmployeeName"),
                            Designation = reader.GetString("Designation"),
                            Salary = reader.GetInt32("Salary")
                           
                        });
                    }
                }
            }
            catch (Exception ex) { }
            finally
            {
                if (con != null)
                    con.Close();
            }

            return employees;
        }

        public EmployeeModel EmployeeByEmployeeId(int id)
        {
            EmployeeModel employee = new EmployeeModel();

            SqlConnection con = new SqlConnection(_connectionString);
            try
            {
                string commandText = "usp_EmployeeById";
                SqlCommand cmd = new SqlCommand(commandText, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", id);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        employee = new EmployeeModel()
                        {
                            EmployeeId = reader.GetInt32("EmployeeId"),
                            EmployeeName  = reader.GetString("EmployeeName"),
                            Designation = reader.GetString("Designation"),
                            Salary  = reader.GetInt32("Salary")
                        };
                        break;
                    }
                }
            }
            catch (Exception ex) { }
            finally
            {
                if (con != null)
                    con.Close();
            }

            return employee;
        }

        public bool Insert(EmployeeModel employee)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            try
            {
                string commandText = "usp_InsertEmployee";
                SqlCommand cmd = new SqlCommand(commandText, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                cmd.Parameters.AddWithValue("@Designation", employee.Designation);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
               

                SqlParameter status = new SqlParameter()
                {
                    ParameterName = "@Status",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(status);

                con.Open();
                int rows = cmd.ExecuteNonQuery();

                return (bool)status.Value;
            }
            catch (Exception ex) { }
            finally
            {
                if (con != null)
                    con.Close();
            }

            return false;
        }

        public bool Update(EmployeeModel employee)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            try
            {
                string commandText = "usp_UpdateEmployee";
                SqlCommand cmd = new SqlCommand(commandText, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                cmd.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                cmd.Parameters.AddWithValue("@Designation", employee.Designation);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
               

                SqlParameter status = new SqlParameter()
                {
                    ParameterName = "@Status",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(status);

                con.Open();
                int rows = cmd.ExecuteNonQuery();

                return (bool)status.Value;
            }
            catch (Exception ex) { }
            finally
            {
                if (con != null)
                    con.Close();
            }

            return false;
        }

        public bool Delete(int id)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            try
            {
                string commandText = "usp_DeleteEmployee";
                SqlCommand cmd = new SqlCommand(commandText, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", id);

                SqlParameter status = new SqlParameter()
                {
                    ParameterName = "@Status",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(status);

                con.Open();
                int rows = cmd.ExecuteNonQuery();

                return (bool)status.Value;
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
