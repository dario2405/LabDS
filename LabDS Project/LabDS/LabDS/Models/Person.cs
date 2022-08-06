using LabDS.App_Start;
using LabDS.Models.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LabDS.Models
{
    public enum RoleType : byte
    {
        NONE = 0,
        Admin = 1,
        User = 2,
        Pacient = 3
    }
    public enum GenderType : byte
    {
        Male = 1,
        Female = 2
    }
    public class Person
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string Username { get; set; }
        [Required, DataType(DataType.Password), MinLength(8)]
        public string Password { get; set; }
        public string PhoneNO { get; set; }
        public RoleType Role { get; set; }
        public GenderType Gender { get; set; }
        public static bool Register(Person person)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"insert into persons(Name, Surname, Username, Password, Birthdate, PhoneNO, Role, Gender)
                                                            values(@name, @surname, @username, @password, @birthdate, @PhoneNO, 1, 1)", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("name", person.Name));
                        cmd.Parameters.Add(new SqlParameter("surname", person.Surname));
                        cmd.Parameters.Add(new SqlParameter("username", person.Username));
                        cmd.Parameters.Add(new SqlParameter("password", person.Password));
                        cmd.Parameters.Add(new SqlParameter("birthdate", person.BirthDate));
                        cmd.Parameters.Add(new SqlParameter("PhoneNO", person.PhoneNO));
                        cmd.Parameters.Add(new SqlParameter("role", RoleType.Admin));
                        cmd.Parameters.Add(new SqlParameter("Gender", GenderType.Male));
                        con.Open();
                        return cmd.ExecuteNonQuery() == 1;


                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return false;
        }

        internal static Person Login(LoginRequestModel model)
        {
            Person result = null;

            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand("select * from Persons where Username = @username and Password = @password", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("username", model.Username));
                        cmd.Parameters.Add(new SqlParameter("password", model.Password));
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result = new Person()
                            {
                                Username = (string)reader["Username"],
                                Role = (RoleType)reader["Role"]
                            };
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        internal static Person GetByUsername(string username)
        {
            Person result = null;

            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand("select * from Persons where Username = @username", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("username", @username));
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result = new Person()
                            {
                                Id = (int)reader["Id"],
                                Username = (string)reader["Username"],
                                BirthDate = (DateTime)reader["BirthDate"],
                                Name = (string)reader["Name"],
                                Surname = (string)reader["Surname"],
                                Password = (string)reader["Password"],
                                PhoneNO = (string)reader["PhoneNO"]
                            };
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        internal static Person GetById(int id)
        {
            Person result = null;
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand("select * from Persons where Id = @id", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("id", @id));
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result = new Person()
                            {
                                Id = (int)reader["Id"],
                                Username = (string)reader["Username"],
                                Password = (string)reader["Password"],
                                BirthDate = (DateTime)reader["BirthDate"],
                                Name = (string)reader["Name"],
                                Surname = (string)reader["Surname"],
                                PhoneNO = (string)reader["PhoneNO"]
                            };
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }
        internal static bool Edit(Person person)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"update 
                                                             persons
                                                             set 
                                                             Name = @Name, 
                                                             Surname = @Surname, 
                                                             Username = @Username, 
                                                             Password = @Password, 
                                                             Birthdate = @Birthdate,
                                                             PhoneNO = @PhoneNO
                                                             where Id = @Id
                                                             ", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("id", person.Id));
                        cmd.Parameters.Add(new SqlParameter("name", person.Name));
                        cmd.Parameters.Add(new SqlParameter("surname", person.Surname));
                        cmd.Parameters.Add(new SqlParameter("username", person.Username));
                        cmd.Parameters.Add(new SqlParameter("password", person.Password));
                        cmd.Parameters.Add(new SqlParameter("birthdate", person.BirthDate));
                        cmd.Parameters.Add(new SqlParameter("phoneno", person.PhoneNO));
                        con.Open();
                        return cmd.ExecuteNonQuery() == 1;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return false;
        }
        internal static List<Person> ListUsers()
        {
            List<Person> result = new List<Person>();
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand("select * from Persons where role = 2 ", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result.Add(new Person()
                            {
                                Id = (int)reader["Id"],
                                Username = (string)reader["Username"],
                                Role = (RoleType)reader["Role"],
                                BirthDate = (DateTime)reader["BirthDate"],
                                Name = (string)reader["Name"],
                                Surname = (string)reader["Surname"],
                                Password = (string)reader["Password"]
                            });
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex) { }
            return result;
        }
        internal static bool Delete(Person person)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand("Delete persons where Id = @id", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("id", person.Id));
                        con.Open();
                        return cmd.ExecuteNonQuery() == 1;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return false;
        }
    }
}