using System.Text;
using AutoMapper;
using Blessmate.Models;
using Blessmate.Records;
using Blessmate.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace Blessmate.Services;

public class AuthService  : IAuthService
{
    private readonly IMapper _mapper;
    private readonly UserManager<Therpist> _userManager;
    public AuthService(IMapper mapper , UserManager<Therpist> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }
    public async Task<AuthResponse> RegisterAsync(Register model)
    { 
        var userExists = await _userManager.FindByEmailAsync(model.Email);
        if(userExists is not null)
            return new AuthResponse {messages = "Email Is Exist Try Another One"};
            
        var therapist = _mapper.Map<Therpist>(model);
        therapist.UserName = model.Email;
        
        var result = await _userManager.CreateAsync(therapist,model.Password);

        if(!result.Succeeded){
            var messagesBuilder = new StringBuilder();
            foreach (var err in result.Errors){
                messagesBuilder.Append(err.Description);
            }
            return new AuthResponse {messages = messagesBuilder.ToString()};
        }
        
        return new AuthResponse{
            id = therapist.Id,
            email = therapist.Email,
            firstname = therapist.FirstName,
            lastname = therapist.LastName,
            isAuth =  true,
            isConfirmed = therapist.IsConfirmed,
            isEmailConfirmed = therapist.EmailConfirmed,
            messages = "Therapist Registered Successfully",
        };
    }
    public async Task<AuthResponse> LoginAsync(Login model)
    {
        var therapist = await _userManager.FindByEmailAsync(model.Email);
        if(therapist is null)
            return new AuthResponse {messages = "Email Is Incorrect !"};
            
        var correctPassword = await _userManager.CheckPasswordAsync(therapist,model.Password);
        if(!correctPassword)        
            return new AuthResponse {messages = "Password Is Incorrect !"};
        
        return new AuthResponse {
            id = therapist.Id,
            email = therapist.Email,
            firstname = therapist.FirstName,
            lastname = therapist.LastName,
            isAuth =  true,
            isConfirmed = therapist.IsConfirmed,
            isEmailConfirmed = therapist.EmailConfirmed,
            messages = "Therapist Login Successfully"
        };
    }

}
