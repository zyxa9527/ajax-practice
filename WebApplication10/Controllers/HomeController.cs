using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using WebApplication10.Models;
using Newtonsoft.Json;

namespace WebApplication10.Controllers
{  
    public class HomeController : Controller
    {
        MyDataBase db = new MyDataBase();
        public ActionResult Index()
        {
            

            List<City> cityList = db.GetCityList();
            ViewBag.CityList = cityList;
            return View();
        }

        [HttpPost]
        public ActionResult Village(string id = "")
        {
           
                List<Village> list = db.GetVillageList(id);
                string result = "";
                if (list == null)
                {
                    //讀取資料庫錯誤

                    return Json(result);
                    

                }
                else
                {
                    result = JsonConvert.SerializeObject(list);
                    return Json(result);
                }
            
        
        }
        public class MyDataBase {
            public List<City> GetCityList()
            {
                try
                {
                    string connString = "Data Source =localhost;Initial Catalog =Carts;User ID =sa;Password = 123456";
                    SqlConnection conn = new SqlConnection
                    {
                        ConnectionString = connString
                    };
                    
                    string sql = @" SELECT id,City from City";
                    SqlCommand cmd = new SqlCommand(sql,conn);
                    List<City> list = new List<City>();
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            City city = new City
                            {
                                CityId = dr["id"].ToString(),
                                CityName = dr["City"].ToString()
                            };
                            list.Add(city);
                        }
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                    return null;
                }
                finally
                {
                    
                }
            }
            public List<Village> GetVillageList(string id)
            {
                try
                {
                    string connString = "Data Source =localhost;Initial Catalog =Carts;User ID =sa;Password = 123456";
                    SqlConnection conn = new SqlConnection
                    {
                        ConnectionString = connString
                    };

                    string sql = @" SELECT VillageId, Village FROM Village Where CityId=" + id ;
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    List<Village> list = new List<Village>();
                    if (conn.State != ConnectionState.Open)
                        conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Village data = new Village();
                            data.VillageId = dr["VillageId"].ToString();
                            data.VillageName = dr["Village"].ToString();
                            list.Add(data);
                        }
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    string error = ex.ToString();
                    return null;
                }
                finally
                {
                    
                }
            }


        }

    }
   
}