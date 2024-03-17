using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.UseCases.Courses.Commands.UpdateCourse;

public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand>
{
    private readonly IAppDbContext _dbContext;

    public UpdateCourseCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var course = await _dbContext.Courses.FirstOrDefaultAsync(
            c => c.Id == dto.Id,
            cancellationToken
        );
        if (course is null)
        {
            throw new NotFoundException();
        }

        if (dto.Name is not null)
        {
            course.Name = dto.Name;
        }
        if (dto.ShortName is not null)
        {
            course.ShortName = dto.ShortName;
        }
        if (dto.Description is not null)
        {
            course.Description = dto.Description;
        }
        if (dto.IsDraft is not null)
        {
            course.IsDraft = dto.IsDraft.Value;
        }
        if (dto.IsDeleted is not null)
        {
            course.IsDeleted = dto.IsDeleted.Value;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
