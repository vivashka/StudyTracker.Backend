using System.ComponentModel.DataAnnotations;

namespace StudyTracker.Application.Models;

public record Admin
{
    public Guid AdminId { get; init; }
}