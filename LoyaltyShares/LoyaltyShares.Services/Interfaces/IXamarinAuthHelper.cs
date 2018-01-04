using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyShares.Services.Interfaces
{
    public interface IXamarinAuthHelper
    {
        void SaveUserDetails(string FBToken, string name, string surname , string UserName);

        string GetUserDetail(string key);
        void RemoveUserDetails();
    }
}
