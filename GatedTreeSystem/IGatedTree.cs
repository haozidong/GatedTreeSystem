
namespace GatedTreeSystem
{
    /// <summary>
    /// This is the interface for a gated tree in our system.
    /// </summary>
    public interface IGatedTree
    {
        /// <summary>
        /// Reset the whole tree.
        /// </summary>
        void Reset();

        /// <summary>
        /// Run one ball through this gated tree.
        /// </summary>
        void RunOneBall();

        /// <summary>
        /// Get the depth of this tree.
        /// </summary>
        int Depth
        {
            get;
        }

        /// <summary>
        /// Get the number of nodes of this tree.
        /// </summary>
        int NumberOfNodes
        {
            get;
        }

        IGatedNode[] Nodes
        {
            get;
        }
    }
}
