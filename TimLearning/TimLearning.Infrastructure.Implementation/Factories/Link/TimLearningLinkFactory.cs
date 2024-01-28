using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using TimLearning.Infrastructure.Implementation.Configurations.Options;
using TimLearning.Infrastructure.Interfaces.Factories.Link;

namespace TimLearning.Infrastructure.Implementation.Factories.Link;

public class TimLearningLinkFactory : ITimLearningLinkFactory
{
    private readonly TimLearningOptions _timLearningOptions;

    public TimLearningLinkFactory(IOptions<TimLearningOptions> timLearningOptions)
    {
        _timLearningOptions = timLearningOptions.Value;
    }

    public string GetLinkToUserConfirm(string userEmail, string signature)
    {
        var linkToConfirm = new UriBuilder(
            new Uri(new Uri(_timLearningOptions.Url, UriKind.Absolute), "/account/confirm")
        )
        {
            Query = new QueryBuilder
            {
                { "email", userEmail },
                { "signature", signature }
            }.ToString()
        };

        return linkToConfirm.ToString();
    }
}
