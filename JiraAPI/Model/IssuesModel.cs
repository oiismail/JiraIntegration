using System;
using System.Collections.Generic;
namespace JiraAPI.Models
{
    public class IssuesModel
    {
        public string? StartDate { get; set; }
        public string? DueDate { get; set; }
        public string? Reporter { get; set; }
        public string? Assignee { get; set; }
        public string? IssueType { get; set; }
        public string? Status { get; set; }
        public string? Sprint { get; set; }
        public string? Priority { get; set; }
    }
}