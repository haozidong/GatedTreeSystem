using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GatedTreeSystem.Tests
{
    [TestClass]
    public class GatedTreeControllerTest
    {
        class MockGatedNode : IGatedNode
        {
            public GatePosition GatePosition { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public int BallsPassedToLeft => throw new NotImplementedException();

            public int BallsPassedToRight => throw new NotImplementedException();

            public void Reset(GatePosition gatePosition) => throw new NotImplementedException();

            public void RunOneBall() => throw new NotImplementedException();

            public void SwitchGate() => throw new NotImplementedException();
        }

        // TODO: using a mock framework to replace this.
        class MockGatedTree : IGatedTree
        {
            public int Depth => throw new NotImplementedException();

            public int NumberOfNodes => throw new NotImplementedException();

            public IGatedNode[] Nodes => throw new NotImplementedException();

            public void Reset()
            {
                throw new NotImplementedException();
            }

            public void RunOneBall()
            {
                throw new NotImplementedException();
            }
        }

        [TestMethod]
        public void CanNotCreateGatedTreeControllerWithInvalidArguments()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new GatedTreeController(null));
        }

        [TestMethod]
        public void CanPredictEmptyContainer()
        {

        }

        [TestMethod]
        public void CanRunBalls()
        {

        }

        [TestMethod]
        public void CanCheckEmptyContainer()
        {

        }
    }
}
