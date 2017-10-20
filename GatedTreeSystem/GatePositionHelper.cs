using System;

namespace GatedTreeSystem
{
    /// <summary>
    /// A helper class for <see cref="GatePosition"/>
    /// </summary>
    public static class GatePositionHelper
    {
        /// <summary>
        /// A random generater
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// Generate a radom <see cref="GatePosition"/>
        /// </summary>
        /// <returns></returns>
        public static GatePosition GetRandomGatePosition()
        {
            return random.Next(2) == 0 ? GatePosition.Left : GatePosition.Right;
        }
    }
}
