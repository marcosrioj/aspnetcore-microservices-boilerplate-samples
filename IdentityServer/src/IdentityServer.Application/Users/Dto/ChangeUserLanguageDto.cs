using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}