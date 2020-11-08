using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSE.BOL;
using Npgsql;

namespace SSE.DAL
{
    public class UserDAL
    {
        public User Login(User u)
        {
            Connection connection = new Connection();

            String sqlCode = "SELECT id,name,password FROM users WHERE name=@name AND password=@password";
            try
            {
                connection.Connect();
                NpgsqlCommand command = new NpgsqlCommand(sqlCode, connection.con);
                command.Parameters.Add(new NpgsqlParameter("name", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters.Add(new NpgsqlParameter("password", NpgsqlTypes.NpgsqlDbType.Varchar));
                command.Parameters[0].Value = u.Name;
                command.Parameters[1].Value = u.Password;
                NpgsqlDataReader rs = command.ExecuteReader();
                if (rs.Read())
                {
                    u.Id = Convert.ToInt32(rs[0]);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Disconnect();
            }
            return u;

        }
    }
}
