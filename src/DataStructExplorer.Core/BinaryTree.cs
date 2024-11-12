namespace DataStructExplorer.Core;

public class BinaryTree
{
    public TreeNode? Root;

    private const string NullLeafRepresentation = "*"; 
    
    public string TraverseDepthFirst()
    {
        return TraverseNodeDepthFirstRecursive(Root);
    }

    private string TraverseNodeDepthFirstRecursive(TreeNode? node)
    {
        if (node is null)
            return NullLeafRepresentation;

        string result = node.Data.ToString();
        result += TraverseNodeDepthFirstRecursive(node.Left);
        result += TraverseNodeDepthFirstRecursive(node.Right);

        return result;
    }

    public string TraverseBreadthFirst()
    {
        if (Root is null)
            return string.Empty;

        var queue = new Queue<TreeNode?>();
        queue.Enqueue(Root);
        string result = string.Empty;

        while (queue.Count > 0)
        {
            TreeNode? current = queue.Dequeue();

            if (current is null)
            {
                result += NullLeafRepresentation;
                continue;
            }

            result += current.Data;
            queue.Enqueue(current.Left);
            queue.Enqueue(current.Right);
        }

        return result;
    }
}