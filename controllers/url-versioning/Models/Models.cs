namespace url_versioning.Models;

public record Userv1(int Id, string Name, string Email);
public record Userv2(int Id, string Name, string Email, DateOnly BirthDate);

public record Scorev1(int UserId, int Score);
public record Scorev2(int UserId, int Score, DateTimeOffset AchievedOn);
