namespace Blessmate.Helpers;

public static class HTMLTemplates
{
    public static string ConfirmEmail(string confirmUrl) {

          return $"<a href=\"{confirmUrl}\" >Click To Confirm Your Email</a>";
    }
    
    public static string ResetPassword(string confirmUrl) {

         return $"<a href=\"{confirmUrl}\" >Click To Reset Your Password</a>"; 
    }
    
}
