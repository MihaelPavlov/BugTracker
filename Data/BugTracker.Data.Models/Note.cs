namespace BugTracker.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using BugTracker.Data.Common.Models;
    using BugTracker.Data.Enums.Note;

    public class Note : BaseDeletableModel<string>
    {
        public Note()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string ProjectId { get; set; }

        public Project Project { get; set; }

        public string CreateByUserId { get; set; }

        public ApplicationUser CreateByApplicationUser { get; set; }

        public string Header { get; set; }

        public string Description { get; set; }

        public NoteColorType NoteColorType { get; set; }
    }
}
