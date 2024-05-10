using TimLearning.Domain.Entities;

namespace TimLearning.Domain.Services.LessonService;

public class LessonOrderService : ILessonOrderService
{
    public IEnumerable<Lesson> Order(IEnumerable<Lesson> lessons)
    {
        var lesson = lessons.SingleOrDefault(l => l.PreviousLesson is null);
        while (lesson is not null)
        {
            yield return lesson;
            lesson = lesson.NextLesson;
        }
    }
}
