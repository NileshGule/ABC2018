using System;

namespace TechTalksELKProcessor.Documents
{
    public class TechTalk
    {
        public string TechTalkName { get; set; }
        public string Category { get; set; }

        public string Level { get; set; }

        public DateTime EventTime { get; set; }
    }
}