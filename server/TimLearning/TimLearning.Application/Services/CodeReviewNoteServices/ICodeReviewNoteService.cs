namespace TimLearning.Application.Services.CodeReviewNoteServices;

public interface ICodeReviewNoteService
{
    Task DeleteIfAllCommentsIsDeleted(Guid noteId, CancellationToken ct = default);
}
