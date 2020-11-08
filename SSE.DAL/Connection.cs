using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace SSE.DAL
{
    class Connection
    {
        public NpgsqlConnection con = new NpgsqlConnection("Server=localhost;" +
            " User Id = postgres;" +
            " Password = Postgres;" +
            " Database = sse");
        public void Connect()
        {
            try
            {
                con.Open();

            }
            catch (Exception e)
            {
                throw new Exception("Unable to connect to database");
            }
        }
        public void Disconnect()
        {
            con.Close();
        }
    }
}
