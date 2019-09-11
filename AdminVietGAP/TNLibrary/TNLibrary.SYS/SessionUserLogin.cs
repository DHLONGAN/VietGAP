using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TNLibrary.SYS.Users;
namespace TNLibrary.SYS
{
    public class SessionUserLogin:User_Info
    {
      //  public User_Info UserLogin = new User_Info();
        public User_Role_Info Role = new User_Role_Info();
        public SessionUserLogin():base()
        {

        }
        public void CheckRole(string RoleID)
        {
            Role.Get_Role_Info(base.Name, RoleID);
        }
         
    }
}
