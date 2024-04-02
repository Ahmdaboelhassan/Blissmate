using Blessmate.Models;
using Microsoft.AspNetCore.Identity;

namespace Blessmate.Helpers
{
    
    public class SeedFakeData
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public SeedFakeData(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public void Seed(){

            List<Patient> patients = new (){
                new Patient () {UserName="user1", Email = "user1@blessmate.com",PhoneNumber = "55112255",FirstName ="ahmed",LastName="essam",IsMale=true},
                 new Patient () {UserName="user7", Email = "user7@blessmate.com",PhoneNumber = "55112255",FirstName ="malk",LastName="khafagy",IsMale=false},
                new Patient () {UserName="user8", Email = "user8@blessmate.com",PhoneNumber = "55112255",FirstName ="menna",LastName="mohammed",IsMale=false},
                new Patient () {UserName="user2", Email = "user2@blessmate.com",PhoneNumber = "55112255",FirstName ="ahmed",LastName="magdy",IsMale=true},
                new Patient () {UserName="user3", Email = "user3@blessmate.com",PhoneNumber = "55112255",FirstName ="mahmoud",LastName="hany",IsMale=true},
              
            };

              List<Therapist> therapist = new (){
                new Therapist () {UserName="user6", Email = "user6@blessmate.com",PhoneNumber = "55112255",FirstName ="nada",LastName="ashraf",IsMale=false , ClinicAddress = "Mansoura",ClinicNumber ="112255",Description="Awasome doctor",IdentityConfirmed = true},
                 new Therapist () {UserName="user4", Email = "user4@blessmate.com",PhoneNumber = "55112255",FirstName ="mahmoud",LastName="hesham",IsMale=true, ClinicAddress = "Damitta",ClinicNumber ="112255",Description="Awasome doctor",IdentityConfirmed = true},
                new Therapist () {UserName="user5", Email = "user5@blessmate.com",PhoneNumber = "55112255",FirstName ="isalm",LastName="fathy",IsMale=true, ClinicAddress = "Mansoura",ClinicNumber ="112255",Description="Awasome doctor",IdentityConfirmed = true},
                new Therapist () {UserName="user9", Email = "user9@blessmate.com",PhoneNumber = "55112255",FirstName ="mai",LastName="adbelmohsen",IsMale=false, ClinicAddress = "Raselbar",ClinicNumber ="112255",Description="Awasome doctor",IdentityConfirmed = true},
            };


            foreach(var pt in patients){
               var result =  _userManager.CreateAsync(pt,"aA12345*").GetAwaiter().GetResult();
            }

             foreach(var th in therapist){
                _userManager.CreateAsync(th,"aA12345*").GetAwaiter().GetResult();
            }



        }
    }
}