using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GatedTreeSystem;

namespace GatedTreeSystem.Tests
{
    [TestClass]
    public class GatedNodeTest
    {
        IGatedNode node;

        [TestInitialize]
        public void TestInitialize()
        {
            node = new GatedNode(GatePosition.Left);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            node = null;
        }

        [TestMethod]
        public void CatReset()
        {
            node.Reset(GatePosition.Left);

            Assert.AreEqual(node.GatePosition, GatePosition.Left);
            Assert.AreEqual(node.BallsPassedToLeft, 0);
            Assert.AreEqual(node.BallsPassedToLeft, 0);
        }

        [TestMethod]
        public void CanRunOneBallLeft()
        {
            node.Reset(GatePosition.Left);

            node.RunOneBall();

            Assert.AreEqual(node.GatePosition, GatePosition.Right);
            Assert.AreEqual(node.BallsPassedToLeft, 1);
            Assert.AreEqual(node.BallsPassedToRight, 0);
        }

        [TestMethod]
        public void CanRunOneBallRight()
        {
            node.Reset(GatePosition.Right);

            node.RunOneBall();

            Assert.AreEqual(node.GatePosition, GatePosition.Left);
            Assert.AreEqual(node.BallsPassedToLeft, 0);
            Assert.AreEqual(node.BallsPassedToRight, 1);
        }

        [TestMethod]
        public void CanSwitchGate()
        {
            node.Reset(GatePosition.Left);
            node.SwitchGate();

            Assert.AreEqual(node.GatePosition, GatePosition.Right);
        }

        [TestMethod]
        public void CanToString()
        {
            node.Reset(GatePosition.Left);

            string nodeString = node.ToString();
            string expect = "/ (0,0)";

            Assert.AreEqual(nodeString, expect);

            node.RunOneBall();

            nodeString = node.ToString();
            expect = "\\ (1,0)";

            Assert.AreEqual(nodeString, expect);

            node.RunOneBall();

            nodeString = node.ToString();
            expect = "/ (1,1)";

            Assert.AreEqual(nodeString, expect);
        }
    }
}
