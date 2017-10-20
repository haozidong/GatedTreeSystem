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
            private int numberOfReset = 0;

            public int NumberOfReset => numberOfReset;

            public IGatedNode NewGatedNode()
            {
                MockdNode node = new MockdNode();
                node.OnReset += Node_OnReset;

                return node;
            }

            private void Node_OnReset(object sender, EventArgs e) => numberOfReset++;
        }

        // TODO: using a mock framework to replace this.
        class MockdNode : IGatedNode
        {
            public event EventHandler OnReset;

            private int ballsPassedToLeft;
            private int ballsPassedToRight;
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
                return String.Format("{0} ({1},{2})",
                    this.gatePosition == GatePosition.Left ? "/" : "\\",
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

            string treeString = tree.ToString();
            string expected = "\\ (1,0)" + Environment.NewLine + "\\ (1,0) / (0,0)";

            Assert.AreEqual(treeString, expected);
        }

        [TestMethod]
        public void CanToString()
        {
            int depth = 2;

            GatedTree tree = new GatedTree(depth, new MockNodeCreator());

            string treeString = tree.ToString();
            string expected = "/ (0,0)" + Environment.NewLine + "/ (0,0) / (0,0)";

            Assert.AreEqual(treeString, expected);
        }
    }
}
