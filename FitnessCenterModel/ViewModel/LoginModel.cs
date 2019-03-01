
using System.ComponentModel.DataAnnotations;
namespace FitnessCenterModel
{
    public class LoginModel
    {
        
        public string Email { get; set; }
        public string Password { get; set; }

        public int State { get; set; } //教練或學員

        public bool autoLogin { get; set; }


    }
}
