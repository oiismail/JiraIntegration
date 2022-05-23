using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JiraAPI.Models
{
    public class UsersModel
    {
        public string AssigneeUserName { get; set; }
        public int TotalIssues { get; set; }
        public long TotalPoints { get; set; }
        
    }
}