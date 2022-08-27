using LabDS.App_Start;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LabDS.Models
{
    public class PacientAnalysis
    {
        public int Id { get; set; }
        [Required]
        public int PacientId { get; set; }
        public Pacient Pacient { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int AnalysisId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public AnalysisCategory Category { get; set; }
        [Required]
        public double Price { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<PacientAnalysisParameter> Parameters { get; set; }
        
        internal static PacientAnalysis GetById(int id)
        {
            PacientAnalysis result = null;
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"select pa.*, c.Name Category, p.Name Pacient 
                                                             from PacientAnalyzes pa
                                                             inner
                                                             join AnalysisCategories c
                                                             on pa.CategoryId = c.Id
                                                             inner
                                                             join Pacients p
                                                             on pa.PacientId = p.Id
                                                             where pa.Id = @Id", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("id", @id));
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result = new PacientAnalysis()
                            {
                                Id = (int)reader["Id"],
                                PacientId = (int)reader["PacientId"],
                                Pacient = new Pacient()
                                {
                                    Name = (string)reader["Name"],
                                },
                                Name = (string)reader["Name"],
                                AnalysisId = (int)reader["AnalysisId"],
                                CategoryId = (int)reader["CategoryId"],
                                Category = new AnalysisCategory()
                                {
                                    Name = (string)reader["Category"],
                                },
                                Price = (double)reader["Price"],
                                CreatedOn = (DateTime)reader["CreatedOn"]
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

        internal static PacientAnalysis GetLast()
        {
            PacientAnalysis res = null;
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand("select top 1 * from PacientAnalyzes order by Id desc", con))
                    {

                        cmd.CommandType = System.Data.CommandType.Text;
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            res = new PacientAnalysis()
                            {
                                Id = (int)reader["Id"],
                                PacientId = (int)reader["PacientId"],
                                Pacient = new Pacient()
                                {
                                    Name = (string)reader["Name"],
                                },
                                Name = (string)reader["Name"],
                                AnalysisId = (int)reader["AnalysisId"],
                                CategoryId = (int)reader["CategoryId"],
                                //Category = new AnalysisCategory()
                                //{
                                //    Name = (string)reader["Category"],
                                //},
                                Price = (double)reader["Price"],
                                CreatedOn = (DateTime)reader["CreatedOn"]
                            };
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex) 
            {
                throw;
            }
            return res;
        }
        public static bool Insert(PacientAnalysis model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"insert into PacientAnalyzes (Name, Price, CategoryId, PacientId, AnalysisId, CreatedOn)
                                                        values(@Name, @Price, @CategoryId, @PacientId, @AnalysisId, @CreatedOn)", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Name", model.Name));
                        cmd.Parameters.Add(new SqlParameter("@Price", model.Price));
                        cmd.Parameters.Add(new SqlParameter("@CategoryId", model.CategoryId));
                        cmd.Parameters.Add(new SqlParameter("@PacientId", model.PacientId));
                        cmd.Parameters.Add(new SqlParameter("@AnalysisId", model.AnalysisId));
                        cmd.Parameters.Add(new SqlParameter("@CreatedOn", DateTime.Now));
                        con.Open();
                        return cmd.ExecuteNonQuery() == 1;
                        con.Close();
                    }
                }
            }
            catch (Exception ex) 
            {
                throw; 
            }
            return false;
        }
        internal static List<PacientAnalysis> ListAnalyzes(int id)
        {
            List<PacientAnalysis> result = new List<PacientAnalysis>();
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"select pa.*, c.Name Category, p.Name Pacient 
                                                             from PacientAnalyzes pa
                                                             inner join AnalysisCategories c
                                                             on pa.CategoryId = c.Id
                                                             inner join Pacients p
                                                             on pa.PacientId = p.Id
                                                             where pa.PacientId = @Id
                                                             order by pa.CreatedOn DESC", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Id", id));
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result.Add(new PacientAnalysis()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Price = (double)reader["Price"],
                                PacientId = (int)reader["PacientId"],
                                Pacient = new Pacient()
                                {
                                    Name = (string)reader["Pacient"],
                                },
                                CategoryId = (int)reader["CategoryId"],
                                Category = new AnalysisCategory()
                                {
                                    Name = (string)reader["Category"]
                                },
                                CreatedOn = (DateTime)reader["CreatedOn"]

                            });
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex) 
            {
                throw;
            }
            return result;
        }

        internal static bool EditAnalysis(PacientAnalysis analysis)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"update 
                                                             PacientAnalyzes
                                                             set 
                                                             Name = @Name, 
                                                             CategoryId = @CategoryId, 
                                                             AnalysisId = @AnalysisId,
                                                             PacientId = @PacientId,
                                                             Price = @Price,
                                                             CreatedOn = @CreatedOn
                                                             where Id = @Id
                                                             ", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Id", analysis.Id));
                        cmd.Parameters.Add(new SqlParameter("@Name", analysis.Name));
                        cmd.Parameters.Add(new SqlParameter("@CategoryId", analysis.CategoryId));
                        cmd.Parameters.Add(new SqlParameter("@AnalysisId", analysis.AnalysisId));
                        cmd.Parameters.Add(new SqlParameter("@PacientId", analysis.PacientId));
                        cmd.Parameters.Add(new SqlParameter("@Price", analysis.Price));
                        cmd.Parameters.Add(new SqlParameter("@CreatedOn", analysis.CreatedOn));
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

        internal static bool DeleteAnalysis(PacientAnalysis analysis)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"Delete PacientAnalysisParameters where PacientAnalysisId = @id
                                                             Delete PacientAnalyzes where Id = @id", con))
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

        internal static List<PacientAnalysis> Journal()
        {
            List<PacientAnalysis> result = new List<PacientAnalysis>();
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"select pa.*, c.Name Category, p.Name Pacient 
                                                             from PacientAnalyzes pa
                                                             inner join AnalysisCategories c
                                                             on pa.CategoryId = c.Id
                                                             inner join Pacients p
                                                             on pa.PacientId = p.Id
                                                             where pa.CreatedOn >= @Id
                                                             order by pa.CreatedOn DESC", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Id", DateTime.Now.ToShortDateString()));
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result.Add(new PacientAnalysis()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Price = (double)reader["Price"],
                                PacientId = (int)reader["PacientId"],
                                Pacient = new Pacient()
                                {
                                    Name = (string)reader["Pacient"],
                                },
                                CategoryId = (int)reader["CategoryId"],
                                Category = new AnalysisCategory()
                                {
                                    Name = (string)reader["Category"]
                                },
                                CreatedOn = (DateTime)reader["CreatedOn"]

                            });
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
    }
}