
namespace GatedTreeSystem
{
    /// <summary>
    /// The controller of a gated tree system.
    /// </summary>
    interface IGatedTreeController
    {
        /// <summary>
        /// Number of balls will be available.
        /// </summary>
        int NumberOfBalls
        {
            get;
        }

        /// <summary>
        /// Predict which container put under the bottom level of branches will not get a ball.
        /// </summary>
        /// <returns></returns>
        int PredictEmptyContainer();

        /// <summary>
        /// Run all the balls through the system one by one.
        /// </summary>
        void RunBalls();

        /// <summary>
        /// Check and return which branch/container did not get a ball。
        /// </summary>
        /// <returns></returns>
        int CheckEmptyContainer();

        /// <summary>
        /// Reset the state.
        /// </summary>
        void Reset();
    }
}
