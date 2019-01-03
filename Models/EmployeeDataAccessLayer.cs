using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreWithAngular.Models
{
    public class EmployeeDataAccessLayer
    {
        string connectionString = "";
        //To View all employees details
        public IEnumerable<Employee> GetAllEmployees()
        {
            DataTable dtPlans = new DataTable();
            OracleParameter orclParam;
            try
            {
                List<Employee> lstemployee = new List<Employee>();
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    OracleCommand cmd = new OracleCommand("spGetAllEmployees", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    orclParam = new OracleParameter();
                    orclParam.Direction = ParameterDirection.Output;
                    orclParam.ParameterName = "p_cursor";
                    orclParam.OracleDbType = OracleDbType.RefCursor;
                    cmd.Parameters.Add(orclParam);
                    con.Open();

                    OracleDataAdapter da = new OracleDataAdapter(cmd);
                    da.Fill(dtPlans);
                    if (dtPlans != null)
                    {
                        foreach (DataRow dr in dtPlans.Rows)
                        {
                            Employee employee = new Employee();
                            employee.ID = Convert.ToInt32(dr["EmployeeID"]);
                            employee.Name = dr["Name"].ToString();
                            employee.Gender = dr["Gender"].ToString();
                            employee.Department = dr["Department"].ToString();
                            employee.City = dr["City"].ToString();
                            lstemployee.Add(employee);
                        }
                    }
                    con.Close();
                }
                return lstemployee;
            }
            catch
            {
                throw;
            }
        } // GetAllEmployee

        //To Add new employee record 
        public int AddEmployee(Employee employee)
        {
            OracleParameter orclParam;
            try
            {
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    OracleCommand cmd = new OracleCommand("spAddEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    orclParam = new OracleParameter();
                    orclParam.Direction = ParameterDirection.Input;
                    orclParam.ParameterName = "Name";
                    orclParam.Value = employee.Name;
                    cmd.Parameters.Add(orclParam);

                    orclParam = new OracleParameter();
                    orclParam.Direction = ParameterDirection.Input;
                    orclParam.ParameterName = "City";
                    orclParam.Value = employee.City;
                    cmd.Parameters.Add(orclParam);

                    orclParam = new OracleParameter();
                    orclParam.Direction = ParameterDirection.Input;
                    orclParam.ParameterName = "Department";
                    orclParam.Value = employee.Department;
                    cmd.Parameters.Add(orclParam);

                    orclParam = new OracleParameter();
                    orclParam.Direction = ParameterDirection.Input;
                    orclParam.ParameterName = "Gender";
                    orclParam.Value = employee.Gender;
                    cmd.Parameters.Add(orclParam);
                    
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch
            {
                throw;
            }
        }
        //To Update the records of a particluar employee
        public int UpdateEmployee(Employee employee)
        {
            OracleParameter orclParam;
            try
            {
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    OracleCommand cmd = new OracleCommand("spUpdateEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    orclParam = new OracleParameter();
                    orclParam.Direction = ParameterDirection.Input;
                    orclParam.ParameterName = "EmpId";
                    orclParam.Value = employee.ID;
                    cmd.Parameters.Add(orclParam);

                    orclParam = new OracleParameter();
                    orclParam.Direction = ParameterDirection.Input;
                    orclParam.ParameterName = "Name";
                    orclParam.Value = employee.Name;
                    cmd.Parameters.Add(orclParam);

                    orclParam = new OracleParameter();
                    orclParam.Direction = ParameterDirection.Input;
                    orclParam.ParameterName = "City";
                    orclParam.Value = employee.City;
                    cmd.Parameters.Add(orclParam);

                    orclParam = new OracleParameter();
                    orclParam.Direction = ParameterDirection.Input;
                    orclParam.ParameterName = "Department";
                    orclParam.Value = employee.Department;
                    cmd.Parameters.Add(orclParam);

                    orclParam = new OracleParameter();
                    orclParam.Direction = ParameterDirection.Input;
                    orclParam.ParameterName = "Gender";
                    orclParam.Value = employee.Gender;
                    cmd.Parameters.Add(orclParam);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch
            {
                throw;
            }
        }
        //Get the details of a particular employee
        public Employee GetEmployeeData(int id)
        {
            try
            {
                Employee employee = new Employee();
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    string sqlQuery = "SELECT * FROM tblEmployee WHERE EmployeeID= " + id;
                    OracleCommand cmd = new OracleCommand(sqlQuery, con);
                    con.Open();
                    OracleDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        employee.ID = Convert.ToInt32(rdr["EmployeeID"]);
                        employee.Name = rdr["Name"].ToString();
                        employee.Gender = rdr["Gender"].ToString();
                        employee.Department = rdr["Department"].ToString();
                        employee.City = rdr["City"].ToString();
                    }
                }
                return employee;
            }
            catch
            {
                throw;
            }
        }
        //To Delete the record on a particular employee
        public int DeleteEmployee(int id)
        {
            OracleParameter orclParam;
            try
            {
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    OracleCommand cmd = new OracleCommand("spDeleteEmployee", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    orclParam = new OracleParameter();
                    orclParam.Direction = ParameterDirection.Input;
                    orclParam.ParameterName = "EmpId";
                    orclParam.Value = id;
                    cmd.Parameters.Add(orclParam);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch
            {
                throw;
            }
        }
    } // EmployeeDataAccess Layer
}
