using LabDS.App_Start;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LabDS.Models
{
    public class AnalysisCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        internal static AnalysisCategory GetByName(string name)
        {
            AnalysisCategory res = null;
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand("select Id from AnalysisCategories where Name = @name", con))
                    {

                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("name", name));
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            res = new AnalysisCategory()
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
        internal static AnalysisCategory GetById(int id)
        {
            AnalysisCategory result = null;

            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand("select * from AnalysisCategories where Id = @id", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("id", @id));
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result = new AnalysisCategory()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"]
                            };
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex) { }

            return result;
        }
        public static bool Insert(AnalysisCategory model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand("insert into AnalysisCategories (Name) values (@Name)", con))

                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Name", model.Name));
                        con.Open();                        
                        return cmd.ExecuteNonQuery() == 1;
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        internal static List<AnalysisCategory> ListCategories()
        {
            List<AnalysisCategory> result = new List<AnalysisCategory>();
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand("select * from AnalysisCategories order by Id desc", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result.Add(new AnalysisCategory()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"]
                            });

                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex) { }
            return result;
        }
        internal static bool Edit(AnalysisCategory category)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"update 
                                                             AnalysisCategories
                                                             set 
                                                             Name = @Name
                                                             where Id = @Id
                                                             ", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("Id", category.Id));
                        cmd.Parameters.Add(new SqlParameter("Name", category.Name));
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
        internal static bool Delete(AnalysisCategory category)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand("Delete analysiscategories where Id = @id", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("Id", category.Id));
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