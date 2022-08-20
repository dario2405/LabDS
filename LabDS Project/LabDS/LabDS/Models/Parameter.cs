using LabDS.App_Start;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LabDS.Models
{
    public class Parameter
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? AnalysisId { get; set; }
        public Analysis Analysis { get; set; }
        [Required]
        public string Range { get; set; }
        [Required]
        public string Unit { get; set; }
        public double Value { get; set; }

        public static bool AddParameter( Parameter model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"insert into Parameters (Name, Unit, AnalysisId, Range, Value)
                                                        values(@Name, @Unit, @AnalysisId, @Range, @Value)
                                                        ", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Name", model.Name));
                        cmd.Parameters.Add(new SqlParameter("@Unit", model.Unit));
                        cmd.Parameters.Add(new SqlParameter("@AnalysisId", model.AnalysisId));
                        cmd.Parameters.Add(new SqlParameter("@Range", model.Range));
                        cmd.Parameters.Add(new SqlParameter("@Value", model.Value));
                        con.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                            return true;
                        con.Close();
                    }
                }
            }
            catch (Exception ex) { }
            return false;
        }
        internal static Parameter GetById(int id)
        {
            Parameter result = null;

            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"select p.*,a.Name Analysis 
                                                             from Parameters p inner
                                                             join Analyzes a
                                                             on p.AnalysisId = a.Id
                                                             where p.Id = @id", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@id", @id));
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result = new Parameter()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Range = (string)reader["Range"],
                                Unit = (string)reader["Unit"],
                                Value = (double)reader["Value"],
                                AnalysisId = (int)reader["AnalysisId"],
                                Analysis = new Analysis()
                                {
                                    Name = (string)reader["Analysis"]
                                }
                            };
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex) { }

            return result;
        }
        internal static bool Edit(Parameter parameter)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"update 
                                                             parameters
                                                             set 
                                                             Name = @Name, 
                                                             Range = @Range, 
                                                             Unit = @Unit, 
                                                             Value = @Value,  
                                                             AnalysisId = @AnalysisId  
                                                             where Id = @Id
                                                             ", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Id", parameter.Id));
                        cmd.Parameters.Add(new SqlParameter("@Name", parameter.Name));
                        cmd.Parameters.Add(new SqlParameter("@Range", parameter.Range));
                        cmd.Parameters.Add(new SqlParameter("@Unit", parameter.Unit));
                        cmd.Parameters.Add(new SqlParameter("@Value", parameter.Value));
                        cmd.Parameters.Add(new SqlParameter("@AnalysisId", parameter.AnalysisId));
                        
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
        internal static bool DeleteParameter(Parameter parameter)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand("Delete Parameters where Id = @id", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("Id", parameter.Id));
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
        internal static List<Parameter> ListParameters(int id)
        {
            List<Parameter> result = new List<Parameter>();
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"select p.*,a.Name Analysis 
                                                             from Parameters p inner join Analyzes a 
                                                             on a.Id=p.AnalysisId where a.Id = @id
                                                            ", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text; 
                        cmd.Parameters.Add(new SqlParameter("@Id", id));
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            var parameter = new Parameter()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                AnalysisId = (int)reader["AnalysisId"],
                                Analysis = new Analysis()
                                {
                                    Name = (string)reader["Analysis"]
                                },
                                Range = (string)reader["Range"],
                                Unit = (string)reader["Unit"],
                                Value = (double)reader["Value"]
                            };

                            result.Add(parameter);
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex) { }
            return result;
        }
    }
}