using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Podgotovka2
{
    public class MySqlDataProvider : IDataProvider
    {
        private MySqlConnection Connection;
        public MySqlDataProvider()
        {
            try
            {
                Connection = new MySqlConnection(
                    "Server=127.0.0.1;Database=mydb;port=3306;UserId=root;password=030104;");
            }
            catch (Exception)
            {
            }
        }
        
        

        public IEnumerable<Product> GetProducts()
        {
            GetProductTypes();
           List<Product> ProductList = new List<Product>();
            string Query = @"SELECT 
            p.*,
            pt.Title AS ProductTypeTitle
        FROM
            Product p
        LEFT JOIN
            ProductType pt ON p.ProductTypeID = pt.ID";
            try
            {
                Connection.Open();
                try
                {
                    MySqlCommand Command = new MySqlCommand(Query, Connection);
                    // получаем результат команды (массив строк)
                    MySqlDataReader Reader = Command.ExecuteReader();

                    while (Reader.Read())
                    {
                        Product NewProduct = new Product();
                        NewProduct.ID = Reader.GetInt32("ID");
                        NewProduct.Title = Reader.GetString("Title");
                        NewProduct.ProductTypeID = Reader.GetInt32("ProductTypeID");
                        NewProduct.ArticleNumber = Reader.GetString("ArticleNumber");
                        NewProduct.ProductionPersonCount = Reader.GetInt32("ProductionPersonCount");
                        NewProduct.ProductionWorkshopNumber = Reader.GetInt32("ProductionWorkshopNumber");
                        NewProduct.MinCostForAgent = Reader.GetInt32("MinCostForAgent");
                      
                        NewProduct.CurrentProductType = GetProductType(Reader.GetInt32("ProductTypeID"));

                        NewProduct.Description = Reader["Description"].ToString();
                        NewProduct.Image = Reader["Image"].ToString();

                        ProductList.Add(NewProduct);
                    }
                }
                finally
                {
                    Connection.Close();
                }
            }
            catch (Exception)
            {

            }
            return ProductList;
        }
        private List<ProductType> ProductTypeList = null;
        public IEnumerable<ProductType> GetProductTypes()
        {
           if (ProductTypeList == null)
            {
                ProductTypeList = new List<ProductType>();
                string Query = "SELECT * FROM ProductType";
                try
                {
                    Connection.Open();
                    try
                    {
                        MySqlCommand Command = new MySqlCommand(Query, Connection);
                        MySqlDataReader Reader = Command.ExecuteReader();
                        while (Reader.Read())
                        {
                            ProductType NewProductType = new ProductType();
                            NewProductType.ID = Reader.GetInt32("ID");
                            NewProductType.Title = Reader.GetString("Title");
                            ProductTypeList.Add(NewProductType);
                        }
                    }
                    finally 
                    { 
                        Connection.Close(); 
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            return ProductTypeList;
        }
        public ProductType GetProductType (int id)
        {
            GetProductTypes();
            return ProductTypeList.Find(at=> at.ID == id);
        }
    }
}
