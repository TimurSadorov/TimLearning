﻿using Microsoft.AspNetCore.Http.Extensions;
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
        var linkToConfirm = new UriBuilder(new Uri(GetLinkToTimLearningSite(), "/account/confirm"))
        {
            Query = new QueryBuilder
            {
                { "email", userEmail },
                { "signature", signature }
            }.ToString()
        };

        return linkToConfirm.ToString();
    }

    public string GetLinkToRecoverPassword(string userEmail, string signature)
    {
        var linkToConfirm = new UriBuilder(
            new Uri(GetLinkToTimLearningSite(), "/account/passwrod-recovering")
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

    private Uri GetLinkToTimLearningSite()
    {
        return new Uri(_timLearningOptions.Url, UriKind.Absolute);
    }
}