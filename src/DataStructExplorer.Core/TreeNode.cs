namespace DataStructExplorer.Core;

public class TreeNode
{
    public char Data;
    public TreeNode? Left;
    public TreeNode? Right;

    public TreeNode(char data)
    {
        Data = data;
        Left = null;
        Right = null;
    }
}