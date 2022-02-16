namespace BugTracker.Data.Enums
{
    using System;

    /// <summary>
    /// An enum for the different types of work item.
    /// </summary>
    [Flags]
    public enum WorkItemType
    {
        /// <summary>
        /// Type for new implementations.
        /// </summary>
        Feature = 1 << 1,

        /// <summary>
        /// Type for bug in our application.
        /// </summary>
        Bug = 1 << 2,

        /// <summary>
        /// Type for something that needs to be complete.
        /// </summary>
        Task = 1 << 3,
    }
}
