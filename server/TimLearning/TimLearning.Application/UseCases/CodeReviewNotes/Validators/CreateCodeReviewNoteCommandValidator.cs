﻿using TimLearning.Application.Services.CodeReviewServices;
using TimLearning.Application.UseCases.CodeReviewNotes.Commands.CreateCodeReviewNote;
using TimLearning.Domain.Entities.Enums;
using TimLearning.Shared.Validation.Exceptions.Localized;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.CodeReviewNotes.Validators;

public class CreateCodeReviewNoteCommandValidator
    : IAsyncSimpleValidator<CreateCodeReviewNoteCommand>
{
    private readonly ICodeReviewService _codeReviewService;

    public CreateCodeReviewNoteCommandValidator(ICodeReviewService codeReviewService)
    {
        _codeReviewService = codeReviewService;
    }

    public async Task ValidateAndThrowAsync(
        CreateCodeReviewNoteCommand entity,
        CancellationToken ct = default
    )
    {
        var status = await _codeReviewService.GetStatusAvailableToGroupMentor(
            entity.Dto.CodeReviewId,
            entity.CallingUserId,
            ct
        );

        if (status is not CodeReviewStatus.Started)
        {
            LocalizedValidationException.ThrowSimpleTextError(
                "Ревью кода либо не было начато, либо уже закончено."
            );
        }
    }
}
