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
        /// The separater between nodes when output tree as string.
        /// </summary>
        public const string NODE_SEPARATER = " ";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string LEVEL_SEPARATER = Environment.NewLine;

        /// <summary>
        /// The depth of the tree
        /// </summary>
        private int depth;

        /// <summary>
        /// Number of nodes on the tree.
        /// Since it is a full binary tree, it would be 'power(2, depth) - 1'.
        /// Here depth is the depth of the tree.
        /// Altoough it can be caculated, save it here for better performance.
        /// </summary>
        private int numberOfNodes;

        /// <summary>
        /// An array to save the nodes of the tree.
        /// Because our tree is a full binary tree, use a simple array is enough to save it.
        /// Nodes will ba saved from level to level, within same level, from left to right.
        /// </summary>
        private IGatedNode[] nodes;

        /// <summary>
        /// Construct a new instance of GatedTree with specified depth.
        /// </summary>
        /// <param name="depth">The depth of the tree, It must be bigger than <see cref="MIN_DEPTH"/>.</param>
        /// <param name="nodeCreator">An instance of <see cref="IGatedNodeCreator"/>, factory class of <see cref="IGatedNode"/>.</param>
        public GatedTree(int depth, IGatedNodeCreator nodeCreator)
        {
            if (depth < MIN_DEPTH || depth > MAX_DEPTH)
                throw new ArgumentOutOfRangeException(
                    String.Format("Value of depth must no smaller than {0} and no bigger than {1}.",
                    MIN_DEPTH, MAX_DEPTH));

            if (nodeCreator == null)
                throw new ArgumentNullException();

            this.depth = depth;
            this.numberOfNodes = (int)Math.Pow(2, depth) - 1;

            this.nodes = new IGatedNode[numberOfNodes];

            for (int i = 0; i < numberOfNodes; i++)
            {
                nodes[i] = nodeCreator.NewGatedNode();
            }
        }

        /// <summary>
        /// Get the nodes of this tree.
        /// </summary>
        public IGatedNode[] Nodes => this.nodes;

        /// <summary>
        /// Get the depth of this tree.
        /// </summary>
        public int Depth => this.depth;

        /// <summary>
        /// Get the number of nodes on the tree.
        /// </summary>
        public int NumberOfNodes => this.numberOfNodes;

        /// <summary>
        /// Reset the whole tree so that we can try the who system again.
        /// This will reset all nodes of the tree by setting a random position for each gate of node,
        ///   and clearing all the recorded number of balls passed through.
        /// </summary>
        public void Reset()
        {
            //Since we saved the full binary tree as an array, and reseting nodes do not need a specific order,
            //  we can just iterate the array.
            Array.ForEach(nodes, node => node.Reset(GatePositionHelper.GetRandomGatePosition()));
        }

        /// <summary>
        /// Run one ball through this gated tree.
        /// </summary>
        public void RunOneBall()
        {
            //Run from the root node.
            int nodeIndex = 0;

            while (nodeIndex < this.numberOfNodes)
            {
                IGatedNode node = this.nodes[nodeIndex];

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
        /// 4. For printing format of nodes, see <see cref="GatedNode.ToString()"./>;
        /// </summary>
        /// <returns>A formatted string represent the tree.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            //First iterate every level
            for (int level = 0; level < depth; level++)
            {
                int numberOfNodesOfUpperLevels = (int)Math.Pow(2, level) - 1;
                int numberOfNodesOfThisLevel = numberOfNodesOfUpperLevels + 1;

                int from = numberOfNodesOfUpperLevels;
                int to = numberOfNodesOfUpperLevels + numberOfNodesOfThisLevel;

                //Then iterate every node in the same level
                for (int node = from; node < to; node++)
                {
                    sb.Append(nodes[node].ToString());

                    if (node < to - 1)
                        sb.Append(NODE_SEPARATER);
                }

                if (level < depth - 1)
                    sb.Append(LEVEL_SEPARATER);
            }

            return sb.ToString();
        }
    }
}
