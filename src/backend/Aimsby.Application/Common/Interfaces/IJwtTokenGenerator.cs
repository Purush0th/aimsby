using Aimsby.Domain.Entities;

namespace Aimsby.Application.Common.Interfaces;

public record TokenResult(string AccessToken, DateTime ExpiresAtUtc);

/// <summary>Access-token creation port; JWT specifics live in Infrastructure.</summary>
public interface IJwtTokenGenerator
{
    TokenResult CreateToken(AdminUser admin);
}
