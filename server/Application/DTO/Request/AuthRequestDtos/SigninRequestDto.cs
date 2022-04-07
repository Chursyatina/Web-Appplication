namespace Application.DTO.Request.AuthRequestDtos
{
    using System.ComponentModel.DataAnnotations;

    public class SigninRequestDto
    {
        [Required]
        [Display(Name = "Phone number")]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
