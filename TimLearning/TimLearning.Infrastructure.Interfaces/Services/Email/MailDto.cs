namespace TimLearning.Infrastructure.Interfaces.Services.Email;

public record MailDto(
    List<string> To,
    string Subject,
    string HtmlBody,
    string TextBody,
    DateTimeOffset CreateAt
)
{
    public MailDto(
        string to,
        string subject,
        string htmlBody,
        string textBody,
        DateTimeOffset createAt
    )
        : this(new List<string> { to }, subject, htmlBody, textBody, createAt) { }
};
