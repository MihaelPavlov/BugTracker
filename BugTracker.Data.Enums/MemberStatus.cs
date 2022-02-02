namespace BugTracker.Data.Enums
{
    using System;

    /// <summary>
    /// An enum for the different types of status of the member.
    /// </summary>
    [Flags]
    public enum MemberStatus
    {
        /// <summary>
        /// Status when a member is ready for work.
        /// </summary>
        Active = 1 << 1,

        /// <summary>
        /// Status when a member is not available for work.
        /// </summary>
        Inactive = 1 << 2,

        /// <summary>
        /// Status when a member is on a vacation.
        /// </summary>
        Vacation = 1 << 3,

        /// <summary>
        /// Status when a member left the project.
        /// </summary>
        DepartFrom = 1 << 4,
    }
}
