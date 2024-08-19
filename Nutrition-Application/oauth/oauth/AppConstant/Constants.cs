using System;
namespace oauth.AppConstant
{
    public class Constants
    {
		public static string AppName = "OAuthNativeFlow";

		// OAuth
		// For Google login, configure at https://console.developers.google.com/
		public static string iOSClientId = "400464822587-3teg53bc4t9tqktp1731dp1kdfk9cscu.apps.googleusercontent.com";
		public static string AndroidClientId = "400464822587-8pfhrmmqm1ejd9imqira8q49mlcsvdbi.apps.googleusercontent.com";

		// These values do not need changing
		public static string Scope = "https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile";
		public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
		public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
		public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

		// Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
		public static string iOSRedirectUrl = "com.googleusercontent.apps.400464822587-3teg53bc4t9tqktp1731dp1kdfk9cscu:/oauth2redirect";
		public static string AndroidRedirectUrl = "com.googleusercontent.apps.400464822587-8pfhrmmqm1ejd9imqira8q49mlcsvdbi:/oauth2redirect";
	}
}
