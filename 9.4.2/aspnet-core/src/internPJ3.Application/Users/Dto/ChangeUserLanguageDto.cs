using System.ComponentModel.DataAnnotations;

namespace internPJ3.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}