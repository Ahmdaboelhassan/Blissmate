using System.Text;
using AutoMapper;
using Blessmate.DTOs;
using Blessmate.Helpers;
using Blessmate.Models;
using Blessmate.Records;
using Blessmate.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace Blessmate.Services;

public class AuthService  : IAuthService
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    public AuthService(IMapper mapper , UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }
    public async Task<AuthResponse> RegisterAsync(Register model)
    { 
        var userExists = await _userManager.FindByEmailAsync(model.Email);
        if(userExists is not null)
            return new AuthResponse {messages = "Email Is Exist Try Another One"};

        var patient = _mapper.Map<Patient>(model);
        patient.UserName = model.Email;
        
        var result = await _userManager.CreateAsync(patient,model.Password);

        if(!result.Succeeded){
            var messagesBuilder = new StringBuilder();
            foreach (var err in result.Errors){
                messagesBuilder.Append(err.Description);
            }
            return new AuthResponse {messages = messagesBuilder.ToString()};
        }
        
        return new AuthResponse{
            id = patient.Id,
            email = patient.Email,
            firstname = patient.FirstName,
            lastname = patient.LastName,
            isAuth =  true,
            isEmailConfirmed = patient.EmailConfirmed,
            messages = "Patient Registered Successfully",
        };
    }
    public async Task<AuthResponse> RegisterAsync(TherapistRegister model)
    { 
        var userExists = await _userManager.FindByEmailAsync(model.Email);
        if(userExists is not null)
            return new AuthResponse {messages = "Email Is Exist Try Another One"};

        var therapist = _mapper.Map<Therapist>(model);
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
            isEmailConfirmed = therapist.EmailConfirmed,
            messages = "Therapist Registered Successfully",
        };
    }
    public async Task<AuthResponse> LoginAsync(Login model)
    {
        var therapist = await _userManager.FindByEmailAsync(model.Email);
        if(therapist is null)
            return new AuthResponse {messages = "Email or password Is Incorrect !"};
            
        var correctPassword = await _userManager.CheckPasswordAsync(therapist,model.Password);
        if(!correctPassword)        
            return new AuthResponse {messages = "Email or password Is Incorrect !"};
        
        return new AuthResponse {
            id = therapist.Id,
            email = therapist.Email,
            firstname = therapist.FirstName,
            lastname = therapist.LastName,
            isAuth =  true,
            isEmailConfirmed = therapist.EmailConfirmed,
            messages = "Therapist Login Successfully"
        };
    }

}
