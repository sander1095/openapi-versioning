namespace minimal_setup_no_openapi.Models;

public record User(int Id, string Name);
public record UserV2(int Id, string Name, DateOnly BirthDate);
