using System.Text;
using Docker.DotNet.Models;
using TimLearning.Shared.Extensions;

namespace TimLearning.DockerManager.Api.Services.Docker.Services;

public class ImageCreationProgressProcessor : IProgress<JSONMessage>
{
    public bool IsSuccessOperation { get; private set; } = true;
    public StringBuilder Messages { get; } = new();

    public void Report(JSONMessage value)
    {
        if (value.Stream.HasText())
        {
            Messages.AppendLine(value.Stream);
        }
        if (value.ErrorMessage.HasText())
        {
            IsSuccessOperation = false;
            Messages.AppendLine(value.ErrorMessage);
        }
    }
}
