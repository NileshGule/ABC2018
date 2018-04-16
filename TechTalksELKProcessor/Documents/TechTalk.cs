using System;

namespace TechTalksELKProcessor.Documents
{
    public class TechTalk
    {
        public int Id { get; set; }
        public string TechTalkName { get; set; }
        public string Category { get; set; }

        public DateTime EventTime { get; set; }
    }
}