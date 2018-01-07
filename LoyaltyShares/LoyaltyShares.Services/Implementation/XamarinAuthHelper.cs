using LoyaltyShares.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace LoyaltyShares.Services.Implementation
{
    public class XamarinAuthHelper : IXamarinAuthHelper
    {
        public string GetUserDetail(string key)
        {
            var account = AccountStore.Create().FindAccountsForService("LoyaltyShares").FirstOrDefault();

            if (account != null)
            {
                switch (key)
                {
                    case "Username":
                        return (account != null) ? account.Properties["Username"] : null;

                    case "Name":
                        return (account != null) ? account.Properties["Name"] : null;

                    case "Surname":
                        return (account != null) ? account.Properties["UserID"] : null;

                    case "FBToken":
                        return (account != null) ? account.Properties["FBToken"] : null;
                    default:
                        break;
                }
            }
            return string.Empty;
        }

        public void RemoveUserDetails()
        {
            var account = AccountStore.Create().FindAccountsForService("LoyaltyShares").FirstOrDefault();

            if (account != null)
            {
                AccountStore.Create().Delete(account, "LoyaltyShares");
            }
        }

        public void SaveUserDetails(string FBToken, string name, string surname , string username)
        {
            Account account = new Account { Username = name };

            account.Properties.Add("Username", username);
            account.Properties.Add("Name", name);
            account.Properties.Add("Surname", surname);
            account.Properties.Add("FBToken", FBToken);          

            AccountStore.Create().Save(account, "LoyaltyShares");
        }
    }
}
