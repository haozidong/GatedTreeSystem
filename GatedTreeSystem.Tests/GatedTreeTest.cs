using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GatedTreeSystem.Tests
{
    [TestClass]
    public class GatedTreeTest
    {
        // TODO: using a mock framework to replace this.
        class MockNodeCreator : IGatedNodeCreator
        {
            /// <summary>
            /// This field will record the number reset be called.
            /// </summary>
            private int numberOfReset = 0;

            public int NumberOfReset => numberOfReset;

            public IGatedNode NewGatedNode()
            {
                MockdGatedNode node = new MockdGatedNode();
                node.OnReset += Node_OnReset;

                return node;
            }

            private void Node_OnReset(object sender, EventArgs e) => numberOfReset++;
        }

        // TODO: using a mock framework to replace this.
        class MockdGatedNode : IGatedNode
        {
            public event EventHandler OnReset;

            private int ballsPassedToLeft;
            private int ballsPassedToRight;

            /// <summary>
            /// Gate position is default to left.
            /// </summary>
            private GatePosition gatePosition = GatePosition.Left;

            public GatePosition GatePosition { get => gatePosition; set => gatePosition = value; }

            public int BallsPassedToLeft => ballsPassedToLeft;

            public int BallsPassedToRight => ballsPassedToRight;

            public void Reset(GatePosition gatePosition) => OnReset?.Invoke(this, new EventArgs());

            public void RunOneBall()
            {
                if (gatePosition == GatePosition.Left)
                    ballsPassedToLeft++;
                else
                    ballsPassedToRight++;

                gatePosition = gatePosition == GatePosition.Left ? GatePosition.Right : GatePosition.Left;
            }

            public void SwitchGate() => throw new NotImplementedException();

            public override string ToString()
            {
                return String.Format(GatedNode.NODE_FORMAT,
                    this.gatePosition == GatePosition.Left ? GatedNode.LEFT_GATE : GatedNode.RIGHT_GATE,
                    this.ballsPassedToLeft,
                    this.ballsPassedToRight);
            }
        }

        [TestMethod]
        public void CanCreateGatedTree()
        {
            int depth = 4;
            int numberOfNodes = (int)Math.Pow(2, depth) - 1;

            GatedTree tree = new GatedTree(depth, new MockNodeCreator());

            Assert.AreEqual(tree.Depth, depth);
            Assert.AreEqual(tree.NumberOfNodes, numberOfNodes);
            Assert.IsNotNull(tree.Nodes);

            Assert.AreEqual(tree.Nodes.Length, numberOfNodes);
            Array.ForEach(tree.Nodes, node => Assert.IsNotNull(node));
        }

        [TestMethod]
        public void CanNotCreateGatedTreeWithInvalidArguments()
        {
            int depth = GatedTree.MIN_DEPTH - 1;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new GatedTree(depth, new MockNodeCreator()));

            depth = GatedTree.MAX_DEPTH + 1;

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new GatedTree(depth, new MockNodeCreator()));

            depth = 4;

            Assert.ThrowsException<ArgumentNullException>(() => new GatedTree(depth, null));
        }

        [TestMethod]
        public void CanReset()
        {
            int depth = 2;
            int numberOfNodes = (int)Math.Pow(2, depth) - 1;

            MockNodeCreator creator = new MockNodeCreator();

            GatedTree tree = new GatedTree(depth, creator);
            tree.Reset();

            Assert.AreEqual(numberOfNodes, creator.NumberOfReset);
        }

        [TestMethod]
        public void CanRunOneBall()
        {
            int depth = 2;

            GatedTree tree = new GatedTree(depth, new MockNodeCreator());
            tree.RunOneBall();

            Assert.AreEqual(tree.Nodes[0].GatePosition, GatePosition.Right);
            Assert.AreEqual(tree.Nodes[0].BallsPassedToLeft, 1);
            Assert.AreEqual(tree.Nodes[0].BallsPassedToRight, 0);

            Assert.AreEqual(tree.Nodes[1].GatePosition, GatePosition.Right);
            Assert.AreEqual(tree.Nodes[1].BallsPassedToLeft, 1);
            Assert.AreEqual(tree.Nodes[1].BallsPassedToRight, 0);

            Assert.AreEqual(tree.Nodes[2].GatePosition, GatePosition.Left);
            Assert.AreEqual(tree.Nodes[2].BallsPassedToLeft, 0);
            Assert.AreEqual(tree.Nodes[2].BallsPassedToRight, 0);
        }

        [TestMethod]
        public void CanToString()
        {
            int depth = 2;

            GatedTree tree = new GatedTree(depth, new MockNodeCreator());

            string treeString = tree.ToString();
            string expected = String.Format(GatedNode.NODE_FORMAT, GatedNode.LEFT_GATE, 0, 0) +
                GatedTree.LEVEL_SEPARATER +
                String.Format(GatedNode.NODE_FORMAT, GatedNode.LEFT_GATE, 0, 0) +
                GatedTree.NODE_SEPARATER +
                String.Format(GatedNode.NODE_FORMAT, GatedNode.LEFT_GATE, 0, 0);

            Assert.AreEqual(treeString, expected);
        }
    }
}
