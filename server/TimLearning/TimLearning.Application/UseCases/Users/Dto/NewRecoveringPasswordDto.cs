namespace TimLearning.Application.UseCases.Users.Dto;

public record NewRecoveringPasswordDto(string UserEmail, string Signature, string NewPassword);
