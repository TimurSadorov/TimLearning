using MediatR;
using TimLearning.Domain.Entities;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.UseCases.Courses.Commands.CreateCourse;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand>
{
    private readonly IAppDbContext _dbContext;

    public CreateCourseCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        _dbContext.Add(
            new Course
            {
                Name = dto.Name,
                ShortName = dto.ShortName,
                Description = dto.Description,
                IsDraft = true,
                IsDeleted = false
            }
        );
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
