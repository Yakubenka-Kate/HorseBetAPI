using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record UserForRegistrationDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string? UserName { get; init; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Incorrect email adres")]
        public string? Email { get; init; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }
        public double? Balance = 500;
    }

    public record UserForAuthenticationDto
    {
        [Required(ErrorMessage = "User name is required")]
        public string? UserName { get; init; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }
    }
}
