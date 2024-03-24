using TimLearning.Api.Responses.Lesson;
using TimLearning.Application.UseCases.Lessons.Dto;

namespace TimLearning.Api.Mappers.Lessons;

public static class LessonSystemDataDtoMapper
{
    public static LessonSystemDataResponse ToResponse(this LessonSystemDataDto dto)
    {
        return new LessonSystemDataResponse(dto.Id, dto.Name, dto.IsDraft);
    }
}
