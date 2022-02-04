using System;
using System.Collections.Generic;

namespace BugTracker.Data.Utilities
{
    public class OperationResult
    {
        private readonly List<Exception> _errors = new List<Exception>();

        /// <summary>
        /// Gets or sets a value indicating whether the operation is successful or not.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets an <see cref="List{T}"/> containing the error codes and messages of the <see cref="OperationResult{T}" />.
        /// </summary>
        public IReadOnlyCollection<Exception> Errors => this._errors.AsReadOnly();

        /// <summary>
        /// Gets or sets the first exception that resulted from the operation.
        /// </summary>
        public Exception InitialException { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationResult"/> class.
        /// </summary>
        /// <param name="success">A value indicating whether the operation result is initially successful or not.</param>
        /// <remarks>If the operation is a get operation, an empty result must return a truthy Success value.</remarks>
        /// 
        public OperationResult(bool success = true)
        {
            this.Success = success;
        }

        public void AppendError(Exception error)
        {
            if (error is null)
                throw new ArgumentNullException(nameof(error));

            if (this.InitialException is null)
                this.InitialException = error;

            this.Success = false;
            this._errors.Add(error);
        }

        /// <summary>
        /// Use this method to get a string with all error messages.
        /// </summary>
        /// <returns>All error messages, joined with a new line character.</returns>
        public override string ToString() => string.Join(Environment.NewLine, this.Errors);
    }
    public class OperationResult<T> : OperationResult
    {
        public OperationResult(bool success = true)
            : base(success)
        {

        }
        /// <summary>
        /// Gets or sets the related object of the operation.
        /// </summary>
        public T RelatedObject { get; set; }
    }
}
