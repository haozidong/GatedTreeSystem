using System;
using System.Text;

namespace GatedTreeSystem
{
    /// <summary>
    /// This is a full binary tree with a gate on each node.
    /// </summary>
    public class GatedTree : IGatedTree
    {
        //Max depth of tree, temporarily set it as 16.
        public const int MAX_DEPTH = 16;
        //Min depth of tree. Set min depth as 2, since we can not play this system without balls
        public const int MIN_DEPTH = 2;

        /// <summary>
        /// The depth of the tree
        /// </summary>
        private int depth;

        /// <summary>
        /// Number of nodes on the tree.
        /// Since it is a full binary tree, it would be 'power(2, depth) - 1'.
        /// Here depth is the depth of the tree.
        /// </summary>
        private int numberOfNodes;

        /// <summary>
        /// Number of balls will be available.
        /// As specified in the rule, it will be number of branches at bottom level minus one.
        /// As the tree is a full binary tree, it will also equals to the number of nodes in the tree.
        /// For example, for a tree with depth as 4,
        ///   the number of nodes at bottom level would be 8,
        ///   the number of branches at bottom level would be 16,
        ///   the number of nodes in the tree would be 15
        ///   and the number of balls would be 15
        /// </summary>
        private int numberOfBalls;

        /// <summary>
        /// An array to save the nodes of the tree.
        /// Because our tree is a full binary tree, use a simple array is enough to save it.
        /// Nodes will ba saved from level to level, within same level, from left to right.
        /// </summary>
        private INode[] nodes;

        /// <summary>
        /// Construct a new instance of GatedTree with specified depth.
        /// </summary>
        /// <param name="depth">The depth of the tree, It must be bigger than 1.</param>
        public GatedTree(int depth)
        {
            if (depth < MIN_DEPTH || depth > MAX_DEPTH)
                throw new ArgumentOutOfRangeException(
                    String.Format("Value of depth must no smaller than {0} and no bigger than {1}.",
                    MIN_DEPTH, MAX_DEPTH));

            this.depth = depth;
            this.numberOfNodes = (int)Math.Pow(2, depth) - 1;
            this.numberOfBalls = numberOfNodes;

            this.nodes = new Node[numberOfNodes];

            for (int i = 0; i < numberOfNodes; i++)
            {
                nodes[i] = new Node(GatePositionHelper.GetRandomGatePosition());
            }
        }

        /// <summary>
        /// Reset the whole tree so that we can try the who system again.
        /// This will reset all nodes of the tree by setting a random position for each position,
        ///   and clearing all the recorded number of balls passed through.
        /// </summary>
        public void Reset()
        {
            //Since we saved the full binary tree as an array, and reseting nodes do not need a specific order,
            //  we can just iterate the array.
            foreach (INode node in nodes)
            {
                node.Reset(GatePositionHelper.GetRandomGatePosition());
            }
        }

        /// <summary>
        /// Predict which container put under the bottom level of branches will not get a ball.
        /// Prediction must be run before <see cref="Run"/> or after <see cref="Reset"/>
        /// The predication is based on bellow rules:
        /// 1. It is a full binary tree;
        /// 2. The number of balls is one ball less than the total bottom level branches;
        /// 3. After a ball passed through a node, the gate of the node will switch.
        /// </summary>
        /// <returns>
        /// The index of the branch/container which will not get a ball
        /// We index the branch/container from left to right with number sequence start from 1 for easy understanding.
        /// So for a tree with depth as 4, the indices would be:
        /// 1, 2, 3, ..., 16
        /// </returns>
        public int Predict()
        {
            //The index of the node we will check next step.
            int keyNodeIndex = 0;

            for (int level = 0; level < depth - 1; level++)
            {
                //The node we will check next step
                INode node = nodes[keyNodeIndex];

                //If the gate is open to left, then the left child tree will get enough balls.
                //So we only need to consider the right child tree.
                //Vice versa.
                if (node.GatePosition == GatePosition.Left)
                {
                    //the index of the root node of right child tree
                    keyNodeIndex = 2 * keyNodeIndex + 2;
                }
                else
                {
                    //the index of the root node of left child tree
                    keyNodeIndex = 2 * keyNodeIndex + 1;
                }
            }

            //Now the keyNode is the node at bottom level which not get enough ball, it will only get one ball.
            //So now we can know which branch of this node no ball will pass through it.
            //If its gate is open to left, then the right branch will not get a ball.

            //Pick the node before translating the index, this is important
            INode keyNode = nodes[keyNodeIndex];

            //Translate the index to a lidex at the level instead of the whole tree.
            //For a tree with depth as 4, if the whole tree index of the node is 12,
            //  then the level index would be 5 for index start from 0;
            int numberOfNodesOfUpperLevels = (int)Math.Pow(2, depth - 1) - 1;
            keyNodeIndex = keyNodeIndex - numberOfNodesOfUpperLevels;

            //Then let's check which branch/containter will not get a ball
            //Node index is start from 0, while returned human readable start from 1.
            return keyNode.GatePosition == GatePosition.Left ?
                keyNodeIndex * 2 + 2 :
                keyNodeIndex * 2 + 1;
        }

        /// <summary>
        /// Run all the balls through the system one by one.
        /// After all the balls passed through, check and return which branch/container did not get a ball
        /// </summary>
        /// <returns></returns>
        public int RunBalls()
        {
            for (int ball = 0; ball < numberOfBalls; ball++)
            {
                RunOneBall();
            }

            int nodeIndex = 0;
            INode node = this.nodes[nodeIndex];

            while (nodeIndex < this.numberOfNodes)
            {
                node = this.nodes[nodeIndex];

                int nextNodeIndex = node.BallsPassedToLeft < node.BallsPassedToRight ?
                    2 * nodeIndex + 1 :
                    2 * nodeIndex + 2;

                if (nextNodeIndex > this.numberOfNodes)
                    break;

                nodeIndex = nextNodeIndex;
            }

            //Translate the index to a lidex at the level instead of the whole tree.
            int numberOfNodesOfUpperLevels = (int)Math.Pow(2, depth - 1) - 1;
            nodeIndex = nodeIndex - numberOfNodesOfUpperLevels;

            //Node index is start from 0, while returned human readable start from 1.
            return node.BallsPassedToLeft == 0 ?
                nodeIndex * 2 + 1 :
                nodeIndex * 2 + 2;
        }

        /// <summary>
        /// Run one ball through the system.
        /// </summary>
        private void RunOneBall()
        {
            //Run from the root node.
            int nodeIndex = 0;

            while (nodeIndex < this.numberOfNodes)
            {
                INode node = this.nodes[nodeIndex];

                int nextNodeIndex = node.GatePosition == GatePosition.Left ?
                    2 * nodeIndex + 1 :
                    2 * nodeIndex + 2;

                node.RunOneBall();

                nodeIndex = nextNodeIndex;
            }
        }

        /// <summary>
        /// Print the who tree as a string as follows:
        /// 1. Each level will be printed in one line;
        /// 2. Nodes in one level will be printed one by one from left to right;
        /// 3. Nodes in one level will be separtated by a space;
        /// 4. For printing format of nodes, see <see cref="Node.ToString()"./>;
        /// </summary>
        /// <returns>A formatted string represent the tree.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            //First iterate every level
            for (int level = 0; level < depth; level++)
            {
                int numberOfNodesOfUpperLevels = (int)Math.Pow(2, level) - 1;
                int numberOfNodesOfThisLevel = (int)Math.Pow(2, level);

                //Then iterate every node in the same level
                for (int node = numberOfNodesOfUpperLevels;
                    node < numberOfNodesOfUpperLevels + numberOfNodesOfThisLevel;
                    node++)
                {
                    sb.Append(nodes[node].ToString());
                    sb.Append(" ");
                }

                sb.Append(Environment.NewLine);
            }

            return sb.ToString();
        }
    }
}
