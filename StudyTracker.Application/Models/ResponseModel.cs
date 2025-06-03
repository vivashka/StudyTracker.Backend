namespace StudyTracker.Application.Models;

public sealed record ResponseModel<T>(
    T SuccessEntity, 
    bool IsSuccess,
    ActionErrorModel? ErrorEntity
);