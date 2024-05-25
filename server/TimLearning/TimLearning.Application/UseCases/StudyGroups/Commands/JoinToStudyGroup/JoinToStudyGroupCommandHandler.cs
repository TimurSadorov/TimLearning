using MediatR;
using TimLearning.Domain.Entities;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Infrastructure.Interfaces.Providers.Clock;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.StudyGroups.Commands.JoinToStudyGroup;

public class JoinToStudyGroupCommandHandler : IRequestHandler<JoinToStudyGroupCommand>
{
    private readonly IAppDbContext _dbContext;
    private readonly IAsyncSimpleValidator<JoinToStudyGroupCommand> _validator;
    private readonly IDateTimeProvider _dateTimeProvider;

    public JoinToStudyGroupCommandHandler(
        IAppDbContext dbContext,
        IAsyncSimpleValidator<JoinToStudyGroupCommand> validator,
        IDateTimeProvider dateTimeProvider
    )
    {
        _dbContext = dbContext;
        _validator = validator;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task Handle(JoinToStudyGroupCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var now = await _dateTimeProvider.GetUtcNow();
        var student = new GroupStudent
        {
            UserId = request.CallingUserId,
            StudyGroupId = request.Dto.GroupId,
            Added = now
        };
        _dbContext.Add(student);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
