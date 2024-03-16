using System.Text;
using Blessmate.Models;
using Blessmate.Records;
using Blessmate.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;


namespace Blessmate.Services
{
    public class EmailConfirmation : IEmailConfirmation
    {
        private readonly UserManager<Therpist> _userManger;
        public EmailConfirmation(UserManager<Therpist> userManager)
        {
            _userManger = userManager;
        }
        public async Task<ManageResponse> GetEmailConfirmationTokenAsync(int id)
        {
            var user = await _userManger.FindByIdAsync(id.ToString());

            if(user is null)
                return new ManageResponse("User is not exist");
            
            if(user.EmailConfirmed)
                return new ManageResponse("User is already confirmed");
            
            var result = await _userManger.GenerateEmailConfirmationTokenAsync(user);
            
            if (string.IsNullOrEmpty(result))
                return new ManageResponse("Something went wrong");
            
            result = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(result));

            return new ManageResponse("Success", true, result, user.Email);
        }

        public async Task<bool> ConfrimEmailAsync(int id , string token)
        {
            var user = await _userManger.FindByIdAsync(id.ToString());

            if(user is null) return false;

             if(user.EmailConfirmed) return false;

            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            var result = await _userManger.ConfirmEmailAsync(user,decodedToken);

            return result.Succeeded;
        }

    }
}