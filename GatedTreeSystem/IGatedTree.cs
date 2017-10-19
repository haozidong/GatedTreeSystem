
namespace GatedTreeSystem
{
    /// <summary>
    /// This is the interface for a gated tree in our system.
    /// </summary>
    interface IGatedTree
    {
        /// <summary>
        /// Reset the whole tree so that we can try the who system again.
        /// </summary>
        void Reset();

        /// <summary>
        /// Predict which container put under the bottom level of branches will not get a ball.
        /// </summary>
        /// <returns></returns>
        int Predict();

        /// <summary>
        /// Run all the balls through the system one by one.
        /// </summary>
        /// <returns></returns>
        int RunBalls();
    }
}
