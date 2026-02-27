namespace minimal_setup_no_openapi.Models;

public record UserV1(int Id, string Name);
public record UserV2(int Id, string Name, DateOnly BirthDate);
