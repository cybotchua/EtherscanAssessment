using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;
using EtherscanAssessment.Models.Tokens;

namespace EtherscanAssessment.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read()
        {
            List<Token> tokenArr = new List<Token>();

            string connStr = "server=localhost;user=root;database=etherscandb;port=3306;password=etherscan123";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = "SELECT * FROM token";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var token = new Token
                    {
                        Id = rdr.GetString("id"),
                        Symbol = rdr.GetString("symbol"),
                        Name = rdr.GetString("name"),
                        ContractAddress = rdr.GetString("contract_address"),
                        TotalSupply = rdr.GetInt32("total_supply"),
                        TotalHolders = rdr.GetInt32("total_holders")
                    };

                    tokenArr.Add(token);
                }

                rdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            conn.Close();
            Console.WriteLine("Done");

            return Json(new { results = tokenArr });
        }

        public ActionResult Insert(string symbol, string name, int totalsupply, string contractaddress, int totalholders)
        {
            string connStr = "server=localhost;user=root;database=etherscandb;port=3306;password=etherscan123";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = "INSERT INTO token (symbol, name, total_supply, contract_address, total_holders) VALUES (?symbol,?name,?totalsupply,?contractaddress,?totalholders)";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add("?symbol", MySqlDbType.VarChar).Value = symbol;
                cmd.Parameters.Add("?name", MySqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("?totalsupply", MySqlDbType.Int32).Value = totalsupply;
                cmd.Parameters.Add("?contractaddress", MySqlDbType.VarChar).Value = contractaddress;
                cmd.Parameters.Add("?totalholders", MySqlDbType.Int32).Value = totalholders;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Json(new { insertSuccess = false });
            }

            conn.Close();
            Console.WriteLine("Done");

            return Json(new { insertSuccess = true });
        }

        public ActionResult Update(string id, string symbol, string name, int totalsupply, string contractaddress, int totalholders)
        {
            string connStr = "server=localhost;user=root;database=etherscandb;port=3306;password=etherscan123";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = "UPDATE token SET symbol = ?symbol, name = ?name, total_supply = ?totalsupply, contract_address = ?contractaddress, total_holders = ?totalholders WHERE id = ?id";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add("?id", MySqlDbType.VarChar).Value = id;
                cmd.Parameters.Add("?symbol", MySqlDbType.VarChar).Value = symbol;
                cmd.Parameters.Add("?name", MySqlDbType.VarChar).Value = name;
                cmd.Parameters.Add("?totalsupply", MySqlDbType.Int32).Value = totalsupply;
                cmd.Parameters.Add("?contractaddress", MySqlDbType.VarChar).Value = contractaddress;
                cmd.Parameters.Add("?totalholders", MySqlDbType.Int32).Value = totalholders;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Json(new { updateSuccess = false });
            }

            conn.Close();
            Console.WriteLine("Done");

            return Json(new { updateSuccess = true });
        }
    }
}
