using AutoMapper;
using Blessmate.DTOs;
using Blessmate.Models;
using Blessmate.Records;
using Blessmate.Services;
using Blessmate.tests.Fakes;
using FakeItEasy;
using Microsoft.AspNetCore.Identity;

namespace Blessmate.tests;

public class AuthServiceTests
    {
      private readonly IMapper _mapper;
      private readonly UserManager<ApplicationUser> _userManager;

      public AuthServiceTests()
      {
        _mapper = A.Fake<IMapper>();
        _userManager = A.Fake<UserManager<ApplicationUser>>();
      }
      
      [Fact]
      public void RegisterAsync_WhenEmailExists_RetrunAuthResonseWithIsAuthEqualFalse()
      {
       
        // Arrange
         var register = A.Fake<Register>();
         var therapist = A.Fake<ApplicationUser>();
         var sut = new AuthService(_mapper,_userManager);


        A.CallTo(() => _userManager.FindByEmailAsync(A<string>.Ignored))
         .Returns(Task.FromResult<ApplicationUser?>(therapist));
        // Act
        var result = sut.RegisterAsync(register).GetAwaiter().GetResult();

        // Assert
        Assert.False(result.isAuth);
      }

      [Fact]
      public void RegisterAsync_CreateUserNotSuccess_RetrunAuthResonseWithIsAuthEqualFalse()
    {
        // Arrange

        var therapst = A.Fake<ApplicationUser>();
        var register = A.Fake<Register>();
        var falseIdentityResult = A.Fake<IdentityResult>();
        var sut = new AuthService(_mapper, _userManager);

        A.CallTo(() => _userManager.CreateAsync(A<ApplicationUser>.Ignored,A<string>.Ignored))
                .Returns(Task.FromResult<IdentityResult>(falseIdentityResult));
       
        // Act
        var result = sut.RegisterAsync(register).GetAwaiter().GetResult();

        // Assert
        Assert.False(result.isAuth);
      }

    [Fact]
    public void RegisterAsync_CreateUserSuccess_RetrunAuthResonseWithIsAuthEqualTrue()
    {
        // Arrange
        var therapst = A.Fake<ApplicationUser>();
        var register = A.Fake<Register>();
        var trueIdentityResult = new TrueIdentityResult();
        var sut = new AuthService(_mapper, _userManager);

        A.CallTo(() => _userManager.FindByEmailAsync(A<string>.Ignored))
               .Returns(Task.FromResult<ApplicationUser?>(null));

        A.CallTo(() => _userManager.CreateAsync(A<ApplicationUser>.Ignored, A<string>.Ignored))
                .Returns(Task.FromResult<IdentityResult>(trueIdentityResult));
        
        // Act
        var result = sut.RegisterAsync(register).GetAwaiter().GetResult();

        // Assert
        Assert.True(result.isAuth);
    }

    

}



