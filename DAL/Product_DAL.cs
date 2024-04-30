using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ADO.NET_Crud.Models;

namespace ADO.NET_Crud.DAL
{
    public class Product_DAL
    {
        string conString = ConfigurationManager.ConnectionStrings["adoConnectionstring"].ToString();

        //Get All Products
        public List<Product> GetAllProducts()
        {
            List<Product> prodList = new List<Product>();

            using(SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "USP_GetAllProducts";

                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtProduct = new DataTable();

                connection.Open();
                sqlDA.Fill(dtProduct);
                connection.Close();

                foreach(DataRow dr in dtProduct.Rows)
                {
                    prodList.Add(new Product
                    {
                        ProductId = dr["ProductId"] != DBNull.Value ? Convert.ToInt32(dr["ProductId"]) : 0,
                        ProductName = dr["ProductName"] != DBNull.Value ? dr["ProductName"].ToString() : string.Empty,
                        Price = dr["Price"] != DBNull.Value ? Convert.ToDecimal(dr["Price"]) : 0.0m,
                        Quantity = dr["Quantity"] != DBNull.Value ? Convert.ToInt32(dr["Quantity"]) : 0,
                        Remarks = dr["Remarks"] != DBNull.Value ? dr["Remarks"].ToString() : string.Empty

                    });
                }
            }
            return prodList;
        }

        //Insert Product

        public bool InsertProduct(Product product)
        {
            int? id = null;
            using(SqlConnection conn = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("USP_InsertProduct", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Quantity", product.Quantity);
                command.Parameters.AddWithValue("@Remarks", product.Remarks);

                conn.Open();
                id = command.ExecuteNonQuery();
                conn.Close();

            }
            if (id != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Get Product By Id
        public List<Product> GetProductById( int ProductId)
        {
            List<Product> prodList = new List<Product>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "USP_GetProductById";
                command.Parameters.AddWithValue("@ProductId", ProductId);

                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtProduct = new DataTable();

                connection.Open();
                sqlDA.Fill(dtProduct);
                connection.Close();

                foreach (DataRow dr in dtProduct.Rows)
                {
                    prodList.Add(new Product
                    {
                        ProductId = Convert.ToInt32(dr["ProductId"]),
                        ProductName = dr["ProductName"].ToString(),
                        Price = Convert.ToDecimal(dr["Price"]),
                        Quantity = Convert.ToInt32(dr["Quantity"]),
                        Remarks = dr["Remarks"].ToString(),
                    });
                }
            }
            return prodList;
        }

        //Insert Product

        public bool UpdateProduct(Product product)
        {
            int? i = null; 
            using (SqlConnection conn = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("USP_UpdateProduct", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductId", product.ProductId);
                command.Parameters.AddWithValue("@ProductName", product.ProductName);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Quantity", product.Quantity);
                command.Parameters.AddWithValue("@Remarks", product.Remarks);

                conn.Open();
                i = command.ExecuteNonQuery();
                conn.Close();

            }
            if (i != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Delete Product
        
        public string DeleteProduct(int productid)
        {
            string result = "";

            using(SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("USP_DeleteProduct", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ProductId", productid);
                command.Parameters.Add("@ReturnMessage", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                connection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@ReturnMessage"].Value.ToString();
                connection.Close();
            }
            return result;
        }
    }
}