namespace BugTracker.Data.Enums.Note
{
    using System;

    /// <summary>
    /// An enum for the different types of note colors.
    /// </summary>
    [Flags]
    public enum NoteColorType
    {
        /// <summary>
        /// Color for Team Buildings.
        /// </summary>
        Blue = 1 << 1,

        /// <summary>
        /// Color for Funny.
        /// </summary>
        Yellow = 1 << 2,

        /// <summary>
        /// Color for Sad News.
        /// </summary>
        Brown = 1 << 3,

        /// <summary>
        /// Color for Memes.
        /// </summary>
        Purple = 1 << 4,

        /// <summary>
        /// Color for Mettings.
        /// </summary>
        Orange = 1 << 5,

        /// <summary>
        /// Color for New Ideas.
        /// </summary>
        Green = 1 << 6,
    }
}
