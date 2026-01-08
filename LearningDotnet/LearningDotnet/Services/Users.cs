using LearningDotnet.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningDotnet.Services
{
    public class Users
    {
        [AcceptVerbs("GET", "POST")]
        public bool verifyEmail(string Email)
        {
            var student = CollegeRepository.Students.Where(x => x.Email == Email).FirstOrDefault();
            if (student != null)
                return true;

            return false;

        }
    }
}
