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
                                                             where pa.PacientId = @Id", con))
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

    }
}