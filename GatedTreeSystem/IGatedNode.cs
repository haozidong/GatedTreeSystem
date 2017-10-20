
namespace GatedTreeSystem
{
    /// <summary>
    /// This is the interface for a node in our system.
    /// Each node has a gate to control which direction the ball will pass through to: Left or Right.
    /// </summary>
    public interface IGatedNode
    {
        /// <summary>
        /// Get or set the gate position of this node.
        /// </summary>
        GatePosition GatePosition
        {
            get;
            set;
        }

        /// <summary>
        /// Get the number of balls passed through this node to its left branch
        /// </summary>
        int BallsPassedToLeft
        {
            get;
        }

        /// <summary>
        /// Get the number of balls passed through this node to its right branch
        /// </summary>
        int BallsPassedToRight
        {
            get;
        }

        /// <summary>
        ///Pass a ball throught this node.
        ///After a ball passed, the gate of this node will switch to opposite position.
        /// </summary>
        void RunOneBall();

        /// <summary>
        /// Reset this node with a gate position.
        /// </summary>
        /// <param name="gatePosition"></param>
        void Reset(GatePosition gatePosition);

        /// <summary>
        /// Switch the gate from left to right, or from right to left.
        /// </summary>
        void SwitchGate();
    }
}
