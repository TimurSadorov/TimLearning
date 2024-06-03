using Microsoft.EntityFrameworkCore;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.Services.CodeReviewNoteServices;

public class CodeReviewNoteService : ICodeReviewNoteService
{
    private readonly IAppDbContext _dbContext;

    public CodeReviewNoteService(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task DeleteIfAllCommentsIsDeleted(Guid noteId, CancellationToken ct = default)
    {
        var note = await _dbContext.CodeReviewNotes.FirstOrDefaultAsync(
            n => n.Id == noteId && n.Comments.All(c => c.Deleted),
            ct
        );
        if (note is null)
        {
            return;
        }

        note.Delete();

        await _dbContext.SaveChangesAsync(ct);
    }
}
