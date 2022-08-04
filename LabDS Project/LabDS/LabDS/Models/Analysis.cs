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
        public double Value { get; set; }
        public string Range { get; set; }
        internal static Analysis GetByName(string name)
        {
            Analysis res = null;
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand("select Id from Products where Name = @name", con))
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
                    using (SqlCommand cmd = new SqlCommand("select * from Products where Id = @id", con))
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
                                CategoryId = (int)reader["CategoryId"],
                                Range = (string)reader["Range"]
                            };
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex) { }

            return result;
        }
        public static bool Insert(Requests.ProductAddRequest model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"insert into Products (Name, Price, ProductCategoryId, Quantity, IsDeleted)
                                                        values(@Name, @Price, @ProductCategoryId,@Quantity, 0)
                                                        ", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("@Name", model.Name));
                        cmd.Parameters.Add(new SqlParameter("@Price", model.Price));
                        cmd.Parameters.Add(new SqlParameter("@ProductCategoryId", model.ProductCategoryId));
                        cmd.Parameters.Add(new SqlParameter("@Quantity", model.Quantity));
                        cmd.Parameters.Add(new SqlParameter("@IsDeleted", false));
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
        internal static List<Analysis> ListProducts()
        {
            List<Analysis> result = new List<Analysis>();
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"select p.*,pc.Name Category 
                                                             from Products p inner join ProductCategories pc 
                                                             on p.ProductCategoryId=pc.Id 
                                                             where p.IsDeleted = 0 and pc.IsDeleted = 0", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        con.Open();
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            var product = new Product()
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Price = (double)reader["Price"],
                                ProductCategory = new ProductCategory()
                                {
                                    Name = (string)reader["Category"]
                                },
                                Quantity = (double)reader["Quantity"]
                            };

                            result.Add(product);
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex) { }
            return result;
        }
        internal static bool Edit(Product product)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand(@"update 
                                                             products
                                                             set 
                                                             Name = @Name, 
                                                             Price = @Price, 
                                                             ProductCategoryId = @ProductCategoryId, 
                                                             Quantity = @Quantity
                                                             where Id = @Id
                                                             ", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("Id", product.Id));
                        cmd.Parameters.Add(new SqlParameter("Name", product.Name));
                        cmd.Parameters.Add(new SqlParameter("Price", product.Price));
                        cmd.Parameters.Add(new SqlParameter("ProductCategoryId", product.ProductCategoryId));
                        cmd.Parameters.Add(new SqlParameter("Quantity", product.Quantity));
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
        internal static bool Delete(Product product)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Utils.DB_CONNECTION_STRING))
                {
                    using (SqlCommand cmd = new SqlCommand("Update products set IsDeleted = 1 where Id = @id", con))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.Add(new SqlParameter("Id", product.Id));
                        cmd.Parameters.Add(new SqlParameter("IsDeleted", true));
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