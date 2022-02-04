namespace BugTracker.Data.Enums
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    public enum Alerts
    {
        /// <summary>
        ///
        /// </summary>
        Success = 1 << 1,

        /// <summary>
        /// 
        /// </summary>
        Info = 1 << 2,

        /// <summary>
        /// 
        /// </summary>
        Warning = 1 << 3,

        /// <summary>
        /// 
        /// </summary>
        Danger = 1 << 4,
    }
}
