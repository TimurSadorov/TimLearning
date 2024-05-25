using MediatR;
using TimLearning.Application.UseCases.StudyGroups.Dto;
using TimLearning.Domain.Entities;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Infrastructure.Interfaces.Providers.Clock;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.StudyGroups.Commands.CreateStudyGroup;

public class CreateStudyGroupCommandHandler : IRequestHandler<CreateStudyGroupCommand, Guid>
{
    private readonly IAppDbContext _dbContext;
    private readonly IAsyncSimpleValidator<NewStudyGroupDto> _validator;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateStudyGroupCommandHandler(
        IAppDbContext dbContext,
        IAsyncSimpleValidator<NewStudyGroupDto> validator,
        IDateTimeProvider dateTimeProvider
    )
    {
        _dbContext = dbContext;
        _validator = validator;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Guid> Handle(
        CreateStudyGroupCommand request,
        CancellationToken cancellationToken
    )
    {
        var dto = request.Dto;
        await _validator.ValidateAndThrowAsync(request.Dto, cancellationToken);

        var now = await _dateTimeProvider.GetUtcNow();
        var group = new StudyGroup
        {
            Name = dto.Name,
            CourseId = dto.CourseId,
            MentorId = request.CallingUserId,
            IsActive = true,
            Added = now
        };
        _dbContext.Add(group);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return group.Id;
    }
}
