using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GatedTreeSystem;
using System.Collections.Generic;
using System.Linq;

namespace GatedTreeSystem.Tests
{
    [TestClass]
    public class GatePositionHelperTest
    {
        [TestMethod]
        public void CanGetRandomGatePosition()
        {
            List<GatePosition> list = new List<GatePosition>();

            list.Add(GatePositionHelper.GetRandomGatePosition());
            list.Add(GatePositionHelper.GetRandomGatePosition());
            list.Add(GatePositionHelper.GetRandomGatePosition());
            list.Add(GatePositionHelper.GetRandomGatePosition());
            list.Add(GatePositionHelper.GetRandomGatePosition());
            list.Add(GatePositionHelper.GetRandomGatePosition());
            list.Add(GatePositionHelper.GetRandomGatePosition());
            list.Add(GatePositionHelper.GetRandomGatePosition());
            list.Add(GatePositionHelper.GetRandomGatePosition());
            list.Add(GatePositionHelper.GetRandomGatePosition());
            list.Add(GatePositionHelper.GetRandomGatePosition());
            list.Add(GatePositionHelper.GetRandomGatePosition());
            list.Add(GatePositionHelper.GetRandomGatePosition());
            list.Add(GatePositionHelper.GetRandomGatePosition());
            list.Add(GatePositionHelper.GetRandomGatePosition());
            list.Add(GatePositionHelper.GetRandomGatePosition());
            list.Add(GatePositionHelper.GetRandomGatePosition());
            list.Add(GatePositionHelper.GetRandomGatePosition());
            list.Add(GatePositionHelper.GetRandomGatePosition());
            list.Add(GatePositionHelper.GetRandomGatePosition());

            GatePosition p1 = list.First();
            Assert.IsFalse(list.All(x => x == p1));
        }
    }
}
