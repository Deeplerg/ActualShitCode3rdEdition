namespace DataStructExplorer.Core;

public class AccessLevelNode
{
    public string Role { get; set; }
    public HashSet<string> Permissions { get; private set; }
    public AccessLevelNode? Left { get; set; }
    public AccessLevelNode? Right { get; set; }

    public AccessLevelNode(string role, HashSet<string>? permissions = null)
    {
        Role = role;
        Permissions = permissions ?? new HashSet<string>();
    }
}

