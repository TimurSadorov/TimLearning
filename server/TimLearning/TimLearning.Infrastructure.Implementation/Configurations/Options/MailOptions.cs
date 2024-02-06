using System.ComponentModel.DataAnnotations;
using TimLearning.Shared.Configuration;

namespace TimLearning.Infrastructure.Implementation.Configurations.Options;

public class MailOptions : IConfigurationOptions
{
    public static string SectionName => "Mail";

    [Required]
    public required string Server { get; init; }

    [Required]
    public required int Port { get; init; }

    [Required]
    public required string UserName { get; init; }

    [Required]
    public required string UserMail { get; init; }

    [Required]
    public required string Password { get; init; }

    [Required]
    public required bool UseSsl { get; init; }
}
