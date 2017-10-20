using System;

namespace GatedTreeSystem
{
    /// <summary>
    /// The controller of a gated tree system.
    /// </summary>
    public class GatedTreeController : IGatedTreeController
    {
        /// <summary>
        /// The gated tree object.
        /// </summary>
        private IGatedTree tree;

        /// <summary>
        /// Number of balls will be available.
        /// As specified in the rules, it will be number of branches of the tree at bottom level minus one.
        /// As the tree is a full binary tree, it will also equals to the number of nodes in the tree.
        /// For example, for a tree with depth as 4,
        ///   the number of nodes at bottom level would be 'power(2, depth - 1)' equals 8,
        ///   the number of branches at bottom level would be 'power(2, depth)' equals 16,
        ///   the number of nodes in the tree would be 'power(2, depth) - 1' equals 15,
        ///   and the number of balls would be 15
        /// </summary>
        private int numberOfBalls;

        /// <summary>
        /// Construct a instance of GatedTreeController.
        /// </summary>
        /// <param name="tree">The gated tree object.</param>
        public GatedTreeController(IGatedTree tree)
        {
            this.tree = tree ?? throw new ArgumentNullException();
            this.numberOfBalls = tree.NumberOfNodes;
        }

        /// <summary>
        /// Number of balls will be available.
        /// </summary>
        public int NumberOfBalls => numberOfBalls;

        /// <summary>
        /// Predict which container put under the bottom level of branches will not get a ball.
        /// Prediction must be ran before <see cref="RunBalls"/> or after <see cref="Reset"/>
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
        public int PredictEmptyContainer()
        {
            //The index of the node we will check next step.
            int nodeIndex = 0;
            //The node we will check next step
            IGatedNode node = tree.Nodes[nodeIndex];

            for (int level = 0; level < tree.Depth - 1; level++)
            {
                //If the gate is open to left, then the left child tree will get enough balls.
                //So we only need to consider the right child tree.
                //Vice versa.
                nodeIndex = node.GatePosition == GatePosition.Left ?
                    2 * nodeIndex + 2 :
                    2 * nodeIndex + 1;

                node = tree.Nodes[nodeIndex];
            }

            //Now the keyNode is the node at bottom level which not get enough ball, it will only get one ball.
            //So now we can know which branch of this node no ball will pass through it.
            //If its gate is open to left, then the right branch will not get a ball.

            //Translate the index to a lidex at the level instead of the whole tree.
            nodeIndex = TranslateToLevelIndex(nodeIndex, tree.Depth);

            //Then let's check which branch/containter will not get a ball
            //Node index is start from 0, while returned human readable start from 1.
            return node.GatePosition == GatePosition.Left ?
                nodeIndex * 2 + 2 :
                nodeIndex * 2 + 1;
        }

        /// <summary>
        /// Run all the balls through the system one by one.
        /// </summary>
        /// <returns></returns>
        public void RunBalls()
        {
            for (int ball = 0; ball < numberOfBalls; ball++)
            {
                tree.RunOneBall();
            }
        }

        /// <summary>
        /// Check and return which branch/container did not get a ball。
        /// </summary>
        /// <returns></returns>
        public int CheckEmptyContainer()
        {
            int nodeIndex = 0;
            IGatedNode node = tree.Nodes[nodeIndex];

            while (nodeIndex < tree.NumberOfNodes)
            {
                int nextNodeIndex = node.BallsPassedToLeft < node.BallsPassedToRight ?
                    2 * nodeIndex + 1 :
                    2 * nodeIndex + 2;

                if (nextNodeIndex >= tree.NumberOfNodes)
                    break;

                nodeIndex = nextNodeIndex;
                node = tree.Nodes[nodeIndex];
            }

            //Translate the index to a lidex at the level instead of the whole tree.
            nodeIndex = TranslateToLevelIndex(nodeIndex, tree.Depth);

            //Node index is start from 0, while returned human readable start from 1.
            return node.BallsPassedToLeft == 0 ?
                nodeIndex * 2 + 1 :
                nodeIndex * 2 + 2;
        }

        /// <summary>
        /// Reset the state, or the tree object.
        /// </summary>
        public void Reset() => tree.Reset();

        /// <summary>
        /// Translate the index to a lidex at the level from a node index in the whole tree.
        /// For a tree with depth as 4, if the whole tree index of the node is 12, and level is 4,
        ///   then the level index would be 5 for index start from 0;
        /// </summary>
        /// <param name="index">A node index in the whole tree.</param>
        /// <param name="level">The level of the node</param>
        /// <returns></returns>
        private static int TranslateToLevelIndex(int index, int level)
        {
            int numberOfNodesOfUpperLevels = (int)Math.Pow(2, level - 1) - 1;
            return index - numberOfNodesOfUpperLevels;
        }
    }
}
