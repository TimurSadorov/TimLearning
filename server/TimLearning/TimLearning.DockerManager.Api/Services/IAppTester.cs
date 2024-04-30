using TimLearning.DockerManager.ApiClient.V1.Request;
using TimLearning.DockerManager.ApiClient.V1.Response;

namespace TimLearning.DockerManager.Api.Services;

public interface IAppTester
{
    Task<V1AppTestResponse> Test(V1AppTestRequest request, CancellationToken ct = default);
}
