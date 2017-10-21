using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GatedTreeSystem.Tests
{
    [TestClass]
    public class GatedTreeControllerTest
    {
        // TODO: using a mock framework to replace this.
        class MockGatedNode : IGatedNode
        {
            public MockGatedNode(GatePosition gatePosition, int ballsPassedToLeft, int ballsPassedToRight)
            {
                this.gatePosition = gatePosition;
                this.ballsPassedToLeft = ballsPassedToLeft;
                this.ballsPassedToRight = ballsPassedToRight;
            }

            private GatePosition gatePosition;
            private int ballsPassedToLeft;
            private int ballsPassedToRight;

            public int BallsPassedToLeft { get => ballsPassedToLeft; }
            public int BallsPassedToRight { get => ballsPassedToRight; }
            public GatePosition GatePosition { get => gatePosition; set => gatePosition = value; }

            public void Reset(GatePosition gatePosition) => throw new NotImplementedException();

            public void RunOneBall() => throw new NotImplementedException();

            public void SwitchGate() => throw new NotImplementedException();
        }

        // TODO: using a mock framework to replace this.
        class MockGatedTree : IGatedTree
        {
            public MockGatedTree(int depth)
            {
                this.depth = depth;
                this.numberOfNodes = (int)Math.Pow(2, depth) - 1;
                this.nodes = new IGatedNode[numberOfNodes];
            }

            private int depth;
            private int numberOfNodes;

            public int Depth { get => depth; }

            public int NumberOfNodes { get => numberOfNodes; }

            private IGatedNode[] nodes = null;

            private int numberOfBallsRun = 0;

            public IGatedNode[] Nodes => nodes;

            public bool IsReset { get => isReset; }
            public int NumberOfBallsRun { get => numberOfBallsRun;}

            private bool isReset = false;

            public void Reset()
            {
                isReset = true;
            }

            public void RunOneBall() => numberOfBallsRun++;
        }

        [TestMethod]
        public void CanNotCreateGatedTreeControllerWithInvalidArguments()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new GatedTreeController(null));
        }

        [TestMethod]
        public void CanPredictEmptyContainer()
        {
            MockGatedTree tree = new MockGatedTree(2);
            tree.Nodes[0] = new MockGatedNode(GatePosition.Left, 0, 0);
            tree.Nodes[1] = new MockGatedNode(GatePosition.Left, 0, 0);
            tree.Nodes[2] = new MockGatedNode(GatePosition.Right, 0, 0);

            GatedTreeController controller = new GatedTreeController(tree);

            int result = controller.PredictEmptyContainer();

            Assert.AreEqual(result, 3);
        }

        [TestMethod]
        public void CanRunBalls()
        {
            MockGatedTree tree = new MockGatedTree(2);
            GatedTreeController controller = new GatedTreeController(tree);

            controller.RunBalls();

            Assert.AreEqual(tree.NumberOfBallsRun, 3);
        }

        [TestMethod]
        public void CanReset()
        {
            MockGatedTree tree = new MockGatedTree(2);
            GatedTreeController controller = new GatedTreeController(tree);

            controller.Reset();

            Assert.AreEqual(tree.IsReset, true);
        }

        [TestMethod]
        public void CanCheckEmptyContainer()
        {
            MockGatedTree tree = new MockGatedTree(2);
            tree.Nodes[0] = new MockGatedNode(GatePosition.Right, 2, 1);
            tree.Nodes[1] = new MockGatedNode(GatePosition.Left, 1, 1);
            tree.Nodes[2] = new MockGatedNode(GatePosition.Left, 0, 1);

            GatedTreeController controller = new GatedTreeController(tree);

            int result = controller.CheckEmptyContainer();

            Assert.AreEqual(result, 3);
        }
    }
}
