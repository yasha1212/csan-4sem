using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    [Serializable]
    public class UsersListPackage
    {
        public int UserID { get; private set; }

        public Dictionary<int, string> Users { get; private set; }

        public UsersListPackage(int userID, Dictionary<int, string> users)
        {
            UserID = userID;
            Users = users;
        }
    }
}
