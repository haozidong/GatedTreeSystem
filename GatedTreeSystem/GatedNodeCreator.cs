
namespace GatedTreeSystem
{
    /// <summary>
    /// Factory class of <see cref="IGatedNode"/>.
    /// This class will create <see cref="IGatedNode"/> with <see cref="GatedNode"/> with a random gate position.
    /// </summary>
    public class GatedNodeCreator : IGatedNodeCreator
    {
        /// <summary>
        /// Create a new instance of <see cref="IGatedNode"/> with <see cref="GatedNode"/> with a random gate position.
        /// </summary>
        /// <returns></returns>
        public IGatedNode NewGatedNode()
        {
            return new GatedNode(GatePositionHelper.GetRandomGatePosition());
        }
    }
}
