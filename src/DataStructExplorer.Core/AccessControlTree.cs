namespace DataStructExplorer.Core;

public class AccessControlTree
{
    public AccessLevelNode? Root { get; set; }

    public void GrantPermission(AccessLevelNode node, string permission)
    {
        node.Permissions.Add(permission);
    }

    public void RevokePermission(AccessLevelNode node, string permission)
    {
        node.Permissions.Remove(permission);
    }

    public HashSet<string> GetEffectivePermissions(AccessLevelNode node)
    {
        HashSet<string> effectivePermissions = new HashSet<string>();

        CollectPermissionsRecursively(node, effectivePermissions);
        return effectivePermissions;
    }
    
    private void CollectPermissionsRecursively(AccessLevelNode? currentNode, HashSet<string> permissions)
    {
        if (currentNode is null) return;
        foreach (var permission in currentNode.Permissions)
        {
            permissions.Add(permission);
        }

        CollectPermissionsRecursively(currentNode.Left, permissions);
        CollectPermissionsRecursively(currentNode.Right, permissions);
    }
}