using LabDS.App_Start;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LabDS.Models
{
    public class PacientAnalysisParameter
    {
        public int Id { get; set; }
        public int PacientAnalysisId { get; set; }
        public PacientAnalysis Analysis { get; set; }
        public string Name { get; set; }
        public string Range { get; set; }
        public string Unit { get; set; }
        [Required]
        public double Value { get; set; }

        internal static PacientAnalysisParameter GetById(int id)
        {
            PacientAnalysisParameter result = null;
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"select pap.*, pa.PacientId Analysis 
                                                             from PacientAnalysisParameters pap
                                                             inner join PacientAnalyzes pa
                                                             on pap.PacientAnalysisId = pa.Id
                                                             where pap.Id = @Id", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Id", id));
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result = new PacientAnalysisParameter()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                PacientAnalysisId = (int)reader["PacientAnalysisId"],
                                Analysis = new PacientAnalysis()
                                {
                                    PacientId = (int)reader["Analysis"]
                                },
                                Range = (string)reader["Range"],
                                Unit = (string)reader["Unit"],
                                Value = (double)reader["Value"]

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
            return result;
        }

        internal static bool AddParameter(PacientAnalysis analysis)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"INSERT INTO PacientAnalysisParameters(Name, Range, Unit, Value, PacientAnalysisId)
                                                             SELECT Name, Range, Unit, Value, @PacientAnalysisId
                                                             FROM Parameters
                                                             WHERE AnalysisId = @AnalysisId", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@PacientAnalysisId", analysis.Id));
                        cmd.Parameters.Add(new SqlParameter("@AnalysisId", analysis.AnalysisId));
                        con.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                throw;
            }
            return false;
        }

        internal static List<PacientAnalysisParameter> ListParameters(int id)
        {
            List<PacientAnalysisParameter> result = new List<PacientAnalysisParameter>();
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"select pap.*, pa.PacientId Analysis 
                                                             from PacientAnalysisParameters pap
                                                             inner join PacientAnalyzes pa
                                                             on pap.PacientAnalysisId = pa.Id
                                                             where pa.Id = @Id", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Id", id));
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result.Add(new PacientAnalysisParameter()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                PacientAnalysisId = (int)reader["PacientAnalysisId"],
                                Analysis = new PacientAnalysis()
                                {
                                    PacientId = (int)reader["Analysis"]
                                },
                                Range = (string)reader["Range"],
                                Unit = (string)reader["Unit"],
                                Value = (double)reader["Value"]

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

        internal static bool EditParameter(PacientAnalysisParameter model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"Update  
                                                             PacientAnalysisParameters
                                                             set 
                                                             Name = @Name, 
                                                             PacientAnalysisId = @PacientAnalysisId, 
                                                             Range = @Range,
                                                             Unit = @Unit,
                                                             Value = @Value
                                                             where Id = @Id 
                                                            ", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Id", model.Id));
                        cmd.Parameters.Add(new SqlParameter("@PacientAnalysisId", model.PacientAnalysisId));
                        cmd.Parameters.Add(new SqlParameter("@Name", model.Name));
                        cmd.Parameters.Add(new SqlParameter("@Range", model.Range));
                        cmd.Parameters.Add(new SqlParameter("@Unit", model.Unit));
                        cmd.Parameters.Add(new SqlParameter("@Value", model.Value));
                        con.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch
            {
                throw;
            }
            return false;
        }
    }
}