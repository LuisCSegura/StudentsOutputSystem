using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSE.BOL;
using SSE.DAL;

namespace SSE.BLL
{
    public class UserBLL
    {
        UserDAL dal = new UserDAL();
        public User Login(User u)
        {
            try
            {
                return dal.Login(u);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
