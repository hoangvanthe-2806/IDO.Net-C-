using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class dbConnectionData
    {
        public static SqlConnection HamKetNoi()
        {
            SqlConnection Conn = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=DemoHocSinh;Integrated Security=True");
            return Conn;
        }
    }
}
