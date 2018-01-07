using System;
using System.Collections.Generic;
using System.Text;

namespace LoyaltyShares.Helpers
{
    public class OAuthServerInfo
    {
        public static Uri FacebookAuthorizationEndpoint = new Uri("https://m.facebook.com/dialog/oauth/");
        public static Uri FacebookTokenEndpoint = new Uri("https://graph.facebook.com/v2.11/oauth/access_token?");
        public static Uri FacebookRedirectionEndpoint = new Uri("https://www.facebook.com/connect/login_success.html");
        public static string FacebookClientId = "277775746024163";
        public static string FacebookScope = "";
        public static string FacebookClientSecret = "9e2c5a8af7ff05e89ec5035fe5ebb3bd";
    }
}
