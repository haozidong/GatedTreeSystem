
namespace GatedTreeSystem
{
    /// <summary>
    /// The interface for Factory class of <see cref="IGatedNode"/>.
    /// </summary>
    public interface IGatedNodeCreator
    {
        /// <summary>
        /// Create a new instance of <see cref="IGatedNode"/>.
        /// </summary>
        /// <returns></returns>
        IGatedNode NewGatedNode();
    }
}
