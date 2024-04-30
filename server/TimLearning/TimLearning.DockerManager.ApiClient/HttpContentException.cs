namespace TimLearning.DockerManager.ApiClient;

public class HttpContentException : Exception
{
    public HttpContentException(string message)
        : base(message) { }
}
