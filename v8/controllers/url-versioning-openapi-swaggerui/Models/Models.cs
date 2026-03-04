namespace url_versioning_openapi_swaggerui.Models;

public record UserV1(int Id, string Name, string Email);
public record UserV2(int Id, string Name, string Email, DateOnly BirthDate);

public record ScoreV1(int UserId, int Score);
public record ScoreV2(int UserId, int Score, DateTimeOffset AchievedOn);
