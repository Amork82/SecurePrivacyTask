using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace SecurePrivacyTask.Server.Dto.Input
{
    public class UserFilterDto
    {
        [AllowNull]
        public string? firstName { get; set; }

        [AllowNull]
        public string? lastName { get; set; }

        [AllowNull]
        public DateTime? dateOfBirth { get; set; }

        [Required]
        public bool onlyEnabled { get; set; }

        [AllowNull]
        public string? orderField { get; set; }

        [AllowNull]
        public string? orderDirection { get; set; }


    }
}
