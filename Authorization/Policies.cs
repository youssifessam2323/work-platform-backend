using Microsoft.AspNetCore.Authorization;

namespace work_platform_backend.Authorization
{
  public class Policies
{
public const string LEADER = "Leader";
public const string MEMBER = "Member";
public static AuthorizationPolicy LeaderPolicy()
{
return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(LEADER).Build();
}
public static AuthorizationPolicy MemberPolicy()
{
return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(MEMBER).Build();
}
}
}