using TimLearning.Domain.Entities;

namespace TimLearning.Domain.Services.LessonService;

public interface ILessonOrderService
{
    IEnumerable<Lesson> Order(IEnumerable<Lesson> lessons);
}
