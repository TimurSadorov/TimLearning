using TimLearning.Domain.Entities;

namespace TimLearning.Application.Services.LessonServices;

public interface ILessonPositionService
{
    Task<Lesson?> FindLastLesson(Guid moduleId, CancellationToken ct = default);
}