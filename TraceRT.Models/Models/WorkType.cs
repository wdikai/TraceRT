namespace TraceRT.Models.Models
{
    using System;
    using TraceRT.Models.Enums;

    public class WorkType
    {
        public WorkType(string title, WorkState state) 
        {
            this.Title = title;
            this.State = state;
        }

        public WorkState State { get; private set; }

        public string Title { get; private set; }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
