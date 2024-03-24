using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Services.LessonServices;
using TimLearning.Domain.Entities;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.UseCases.Lessons.Command.CreateLesson;

public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand>
{
    private readonly IAppDbContext _dbContext;
    private readonly ILessonPositionService _lessonPositionService;

    public CreateLessonCommandHandler(
        IAppDbContext dbContext,
        ILessonPositionService lessonPositionService
    )
    {
        _dbContext = dbContext;
        _lessonPositionService = lessonPositionService;
    }

    public async Task Handle(CreateLessonCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        if (
            await _dbContext.Modules.AnyAsync(m => m.Id == dto.ModuleId, cancellationToken) is false
        )
        {
            throw new NotFoundException();
        }

        var lastLesson = await _lessonPositionService.FindLastLesson(
            dto.ModuleId,
            cancellationToken
        );
        var newLesson = new Lesson(lastLesson)
        {
            Name = dto.Name,
            Text = "",
            ModuleId = dto.ModuleId,
            IsDraft = true
        };
        _dbContext.Add(newLesson);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
