using System;

namespace GatedTreeSystem
{
    /// <summary>
    /// This class represents a node in our system.
    /// Each node has a gate to control which direction the ball will pass through to: Left or Right.
    /// If the gate is open to Left, a ball will pass this node to its left child.
    /// If the gate is open to Right, a ball will pass the node to its right child
    /// </summary>
    public class GatedNode : IGatedNode
    {
        /// <summary>
        /// The format string for a gated node.
        /// </summary>
        public const string NODE_FORMAT = "{0} ({1},{2})";

        /// <summary>
        /// The string representation of gate postion left.
        /// </summary>
        public const string LEFT_GATE = "/";

        /// <summary>
        /// The string representation of gate postion right.
        /// </summary>
        public const string RIGHT_GATE = "\\";

        /// <summary>
        /// The fielrecordsds the current position of the gate of this node.
        /// </summary>
        private GatePosition gatePosition = GatePosition.Left;

        /// <summary>
        /// This field records how many balls passed through this node to its left branch.
        /// </summary>
        private int ballsPassedToLeft = 0;

        /// <summary>
        /// This field records how many balls passed through this node to its right branch.
        /// </summary>
        private int ballsPassedToRight = 0;

        /// <summary>
        /// Construct a new node, with its initial gate position.
        /// </summary>
        /// <param name="gatePosition">The initial gate position</param>
        public GatedNode(GatePosition gatePosition) => this.gatePosition = gatePosition;

        /// <summary>
        /// Get the gate position of this node.
        /// </summary>
        public GatePosition GatePosition
        {
            get => gatePosition;
            set => gatePosition = value;
        }

        /// <summary>
        /// Get the number of balls passed through this node to its left branch
        /// </summary>
        public int BallsPassedToLeft => ballsPassedToLeft;

        /// <summary>
        /// Get the number of balls passed through this node to its right branch
        /// </summary>
        public int BallsPassedToRight => ballsPassedToRight;

        /// <summary>
        /// Reset this node with a gate position.
        /// This will set the gate of this node to the specified position.
        /// At the same time, clear the recorded number of balls passed throght this node.
        /// </summary>
        public void Reset(GatePosition gatePosition)
        {
            this.gatePosition = gatePosition;
            this.ballsPassedToLeft = 0;
            this.ballsPassedToRight = 0;
        }

        /// <summary>
        /// Pass a ball throught this node.
        /// After a ball passed, the gate of this node will switch to opposite position.
        /// </summary>
        public void RunOneBall()
        {
            if (this.gatePosition == GatePosition.Left)
                ballsPassedToLeft++;
            else
                ballsPassedToRight++;

            this.SwitchGate();
        }

        /// <summary>
        /// Print the node as a string in format "Position (BallsPassedToLeft, BallsPassedToRight)".
        /// Position will be printed as "/" for left, "\" for right.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format(NODE_FORMAT,
                this.gatePosition == GatePosition.Left ? LEFT_GATE : RIGHT_GATE,
                this.ballsPassedToLeft,
                this.ballsPassedToRight);
        }

        /// <summary>
        /// Switch the gate from left to right, or from right to left.
        /// </summary>
        public void SwitchGate()
        {
            gatePosition = gatePosition == GatePosition.Left ? GatePosition.Right : GatePosition.Left;
        }
    }
}
