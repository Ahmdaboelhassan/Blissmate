using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Blessmate.DTOs;
using Blessmate.Factory;
using Blessmate.Helpers;
using Blessmate.Models;
using Blessmate.Records;
using Blessmate.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Blessmate.Services;

public class AuthService  : IAuthService
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JWTSettings _jwtSettings ;
    public AuthService(IMapper mapper , UserManager<ApplicationUser> userManager , IOptions<JWTSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
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

        var token = await GenerateToken(patient);
        
        return AuthResponseFactory.SuccessAuthResponse(patient, token);
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
        
        var token = await GenerateToken(therapist);

        return AuthResponseFactory.SuccessAuthResponse(therapist, token);
    }
    public async Task<AuthResponse> LoginAsync(Login model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if(user is null)
            return new AuthResponse {messages = "Email or password Is Incorrect !"};
            
        var correctPassword = await _userManager.CheckPasswordAsync(user,model.Password);
        if(!correctPassword)        
            return new AuthResponse {messages = "Email or password Is Incorrect !"};
        
        var token = await GenerateToken(user);

        return AuthResponseFactory.SuccessAuthResponse(user, token);
    }

    public async Task<(string key , DateTime expireOn)>  GenerateToken(ApplicationUser user){
        var userClaims = await _userManager.GetClaimsAsync(user);
        
        var Claims = new []{
            new Claim(JwtRegisteredClaimNames.NameId , user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Iss , Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email , user.Email),
        }.
        Union(userClaims);

        var symmetricSecureKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.JWTKey));
        var cred = new SigningCredentials(symmetricSecureKey,SecurityAlgorithms.HmacSha256);

        var tokenInfo = new JwtSecurityToken(
          issuer : _jwtSettings.Issuer,
          audience : _jwtSettings.Audience,
          expires : DateTime.Now.AddDays(_jwtSettings.DurationInDays),
          claims : Claims,
          signingCredentials : cred
        );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenInfo);
        
        return (token , tokenInfo.ValidTo);
    }


}
