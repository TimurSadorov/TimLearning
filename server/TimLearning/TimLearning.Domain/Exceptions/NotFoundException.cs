﻿namespace TimLearning.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string? message = null)
        : base(message) { }
}
