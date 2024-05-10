using TimLearning.Domain.Entities.Data;
using TimLearning.Shared.BaseEntities;

namespace TimLearning.Domain.Entities;

public class Exercise : IIdHolder<Guid>
{
    public Guid Id { get; init; }

    public required Guid AppArchiveId { get; set; }
    public StoredFile AppArchive { get; set; } = null!;

    public required MainAppContainerData AppContainerData { get; set; }
    public required string RelativePathToDockerfile { get; set; }
    public required string[] RelativePathToInsertCode { get; set; }
    public required string StandardCode { get; set; }

    public required List<ServiceContainerImageData> ServiceApps { get; set; }

    public List<UserSolution> UserSolutions { get; init; } = null!;
}
