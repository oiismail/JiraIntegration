using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JiraAPI.Models
{
    public class IssuesModel
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public string Reporter { get; set; }
        public string Assignee { get; set; }
        public string StoryPoints { get; set; }
        public string IssueType { get; set; }
        public DateTime CreatedDate { get; set; }
        //public DateTime SolvedDate { get; set; }
        public string Status { get; set; }
        public string Sprint { get; set; }
    }
}