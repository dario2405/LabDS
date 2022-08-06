using LabDS.App_Start;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LabDS.Models
{
    public class Analysis
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public AnalysisCategory Category { get; set; }
        public double Price { get; set; }
        public string Range { get; set; }
        public string Unit { get; set; }
        internal static Analysis GetByName(string name)
        {
            Analysis res = null;
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand("select Id from Analyzes where Name = @name", con))
                    {
                        cmd.Parameters.Add(new SqlParameter("name", name));
                        cmd.CommandType = System.Data.CommandType.Text;
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            res = new Analysis()
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
        internal static Analysis GetById(int id)
        {
            Analysis result = null;

            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"select a.*,ac.Name Category 
                                                             from Analyzes a inner
                                                             join AnalysisCategories ac
                                                             on a.CategoryId = ac.Id
                                                             where a.Id = @id", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("id", @id));
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result = new Analysis()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Price = (double)reader["Price"],
                                Category = new AnalysisCategory()
                                {
                                    Name = (string)reader["Category"]
                                },
                                Range = (string)reader["Range"],
                                Unit = (string)reader["Unit"]
                            };
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex) { }

            return result;
        }
        public static bool Insert(Requests.AnalysisAddRequest model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"insert into Analyzes (Name, Price, CategoryId, Range)
                                                        values(@Name, @Price, @CategoryId,@Range)
                                                        ", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Name", model.Name));
                        cmd.Parameters.Add(new SqlParameter("@Price", model.Price));
                        cmd.Parameters.Add(new SqlParameter("@CategoryId", model.CategoryId));
                        cmd.Parameters.Add(new SqlParameter("@Range", model.Range));
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
        internal static List<Analysis> ListAnalyzes()
        {
            List<Analysis> result = new List<Analysis>();
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"select a.*,ac.Name Category 
                                                             from Analyzes a inner join AnalysisCategories ac 
                                                             on a.CategoryId=ac.Id 
                                                            ", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            var analysis = new Analysis()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Price = (double)reader["Price"],
                                Category = new AnalysisCategory()
                                {
                                    Name = (string)reader["Category"]
                                },
                                Range = (string)reader["Range"],
                                Unit = (string)reader["Unit"]
                            };

                            result.Add(analysis);
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex) { }
            return result;
        }
        internal static bool Edit(Analysis analysis)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"update 
                                                             analyzes
                                                             set 
                                                             Name = @Name, 
                                                             Price = @Price, 
                                                             CategoryId = @CategoryId, 
                                                             Range = @Range,
                                                             Unit = @Unit
                                                             where Id = @Id
                                                             ", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("Id", analysis.Id));
                        cmd.Parameters.Add(new SqlParameter("Name", analysis.Name));
                        cmd.Parameters.Add(new SqlParameter("Price", analysis.Price));
                        cmd.Parameters.Add(new SqlParameter("CategoryId", analysis.CategoryId));
                        cmd.Parameters.Add(new SqlParameter("Range", analysis.Range));
                        cmd.Parameters.Add(new SqlParameter("Unit", analysis.Unit));
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
        internal static bool Delete(Analysis analysis)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand("Delete analyzes where Id = @id", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("Id", analysis.Id));
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