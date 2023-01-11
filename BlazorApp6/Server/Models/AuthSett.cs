namespace BlazorApp6.Server.Models
{
    public class AuthSettings
    {
        public Passinfo Passinfo { get; set; }
        public Passreq Passreq { get; set; }
        public Lockout Lockout { get; set; }
        public Jwtoken Jwtoken { get; set; }
    }

    public class Passinfo
    {
        public int DegreeOfParallelism { get; set; }
        public int MemorySize { get; set; }
        public int Iterations { get; set; }
        public int SaltLen { get; set; }
        public int PassLen { get; set; }
    }

    public class Passreq
    {
        public int RequiredLength { get; set; }
        public bool RequireLowercase { get; set; }
        public bool RequireUppercase { get; set; }
        public bool RequireDigit { get; set; }
        public bool RequireNonAlphanumeric { get; set; }
    }

    public class Lockout
    {
        public bool AllowedForNewUsers { get; set; }
        public int DefaultLockoutTimeSpanInMins { get; set; }
        public int MaxFailedAccessAttempts { get; set; }
    }
    public class Jwtoken
    {
        public string Token { get; set; }  
    }
}
