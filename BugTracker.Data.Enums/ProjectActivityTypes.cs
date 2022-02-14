namespace BugTracker.Data.Enums
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// An enum for the different types project activities.
    /// </summary>
    [Flags]
    public enum ProjectActivityTypes
    {
        /// <summary>
        /// Status when a member is ready for work.
        /// </summary>
        Overview = 1 << 1,

        /// <summary>
        /// Status when a member is not available for work.
        /// </summary>
        WorkItem = 1 << 2,

        /// <summary>
        /// Status when a member is on a vacation.
        /// </summary>
        Note = 1 << 3,

        /// <summary>
        /// Status when a member left the project.
        /// </summary>
        Members = 1 << 4,
    }
}
