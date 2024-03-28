using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.UseCases.Lessons.Command.UpdateLesson;

public class UpdateLessonCommandHandler : IRequestHandler<UpdateLessonCommand>
{
    private readonly IAppDbContext _dbContext;

    public UpdateLessonCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(UpdateLessonCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var lesson = await _dbContext.Lessons.FirstOrDefaultAsync(
            l => l.Id == dto.Id,
            cancellationToken
        );
        if (lesson is null)
        {
            throw new NotFoundException();
        }

        if (dto.Name is not null)
        {
            lesson.Name = dto.Name;
        }
        if (dto.Text is not null)
        {
            lesson.Text = dto.Text;
        }
        if (dto.IsDraft is not null)
        {
            lesson.IsDraft = dto.IsDraft.Value;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
