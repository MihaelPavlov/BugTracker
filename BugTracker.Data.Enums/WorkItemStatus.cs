namespace BugTracker.Data.Enums
{
    using System;

    /// <summary>
    /// An enum for the different types of status of the work item.
    /// </summary>
    [Flags]
    public enum WorkItemStatus
    {
        /// <summary>
        /// Status when a work item is created.
        /// </summary>
        New = 1 << 1,

        /// <summary>
        /// Status when a work item will be scheluded.
        /// </summary>
        ToBeScheluded = 1 << 2,

        /// <summary>
        /// Status when a work item is in progress.
        /// </summary>
        InProgress = 1 << 3,

        /// <summary>
        /// Status when a work item is on hold.
        /// </summary>
        OnHold = 1 << 4,

        /// <summary>
        /// Status when a work item is closed/complete.
        /// </summary>
        Closed = 1 << 5,

        /// <summary>
        /// Status when a work item is rejected.
        /// </summary>
        Rejected = 1 << 6,
    }
}
