using FluentValidation;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Shared.Validation.FluentValidator.Validators;

public interface ICombinedFluentAndSimpleValidator<in T> : IValidator<T>, IAsyncSimpleValidator<T>;
