using LabDS.App_Start;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LabDS.Models
{
    public class Pacient
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required]
        public string PhoneNO { get; set; }
        [Required(ErrorMessage = "Zgjidhni gjinine e pacientit")]
        public string Gender { get; set; }
        public List<PacientAnalysis> Analyzes { get; set; }
        public static bool AddPacient(Pacient person)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"insert into pacients(Name, Surname, Birthdate, PhoneNO, Gender)
                                                            values(@name, @surname, @birthdate, @PhoneNO, @gender)", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("name", person.Name));
                        cmd.Parameters.Add(new SqlParameter("surname", person.Surname));
                        cmd.Parameters.Add(new SqlParameter("birthdate", person.BirthDate));
                        cmd.Parameters.Add(new SqlParameter("PhoneNO", person.PhoneNO));
                        cmd.Parameters.Add(new SqlParameter("Gender", person.Gender));
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
        internal static Pacient GetLast()
        {
            Pacient res = null;
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand("select top 1 * from Pacients order by Id desc", con))
                    {

                        cmd.CommandType = System.Data.CommandType.Text;
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            res = new Pacient()
                            {
                                Id = (int)reader["Id"]
                            };
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex) { }
            return res;
        }
        internal static Pacient GetByName(string name)
        {
            Pacient result = null;

            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand("select * from Pacients where Name = @name", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("name", @name));
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result = new Pacient()
                            {
                                Id = (int)reader["Id"],
                                BirthDate = (DateTime)reader["BirthDate"],
                                Name = (string)reader["Name"],
                                Surname = (string)reader["Surname"],
                                PhoneNO = (string)reader["PhoneNO"],
                                Gender = (string)reader["Gender"]
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
        internal static Pacient GetByFullName(string name, string surname)
        {
            Pacient result = null;

            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand("select * from Pacients where Name = @name and Surname = @surname", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("name", @name));
                        cmd.Parameters.Add(new SqlParameter("surname", @surname));
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result = new Pacient()
                            {
                                Id = (int)reader["Id"],
                                BirthDate = (DateTime)reader["BirthDate"],
                                Name = (string)reader["Name"],
                                Surname = (string)reader["Surname"],
                                PhoneNO = (string)reader["PhoneNO"],
                                Gender = (string)reader["Gender"]
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
        internal static Pacient GetById(int id)
        {
            Pacient result = null;
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand("select * from pacients where Id = @id", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("id", @id));
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result = new Pacient()
                            {
                                Id = (int)reader["Id"],
                                BirthDate = (DateTime)reader["BirthDate"],
                                Name = (string)reader["Name"],
                                Surname = (string)reader["Surname"],
                                PhoneNO = (string)reader["PhoneNO"],
                                Gender = (string)reader["Gender"]
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
        internal static bool EditPacient(Pacient pacient)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"update 
                                                             pacients
                                                             set 
                                                             Name = @Name, 
                                                             Surname = @Surname, 
                                                             Birthdate = @Birthdate,
                                                             PhoneNO = @PhoneNO,
                                                             Gender = @Gender
                                                             where Id = @Id
                                                             ", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("id", pacient.Id));
                        cmd.Parameters.Add(new SqlParameter("name", pacient.Name));
                        cmd.Parameters.Add(new SqlParameter("surname", pacient.Surname));
                        cmd.Parameters.Add(new SqlParameter("birthdate", pacient.BirthDate));
                        cmd.Parameters.Add(new SqlParameter("phoneno", pacient.PhoneNO));
                        cmd.Parameters.Add(new SqlParameter("gender", pacient.Gender));
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
        internal static List<Pacient> ListPacients()
        {
            List<Pacient> result = new List<Pacient>();
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand("select * from pacients", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result.Add(new Pacient()
                            {
                                Id = (int)reader["Id"],
                                BirthDate = (DateTime)reader["BirthDate"],
                                Name = (string)reader["Name"],
                                Surname = (string)reader["Surname"],
                                Gender = (string)reader["Gender"]
                            });
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex) { }
            return result;
        }
        internal static bool DeletePacient(Pacient pacient)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand("Delete pacients where Id = @id", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("id", pacient.Id));
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