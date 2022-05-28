using Atlassian.Jira;
using JiraAPI.Model.Common;
using JiraAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JiraAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class JiraIntegrationController : ControllerBase
    {
        AppConfiguration appConfiguration = new AppConfiguration();
        public JiraIntegrationController()
        {

        }

        [HttpGet("Jira/GetSprintChampiones")]
        public IActionResult GetSprintChampiones(string projectName,string sprintName, string teamSpecialtyName)
        {
            List<UsersModel> userList = new List<UsersModel>();
            List<Issue> sprintIssues = GetSprintIssues(projectName, sprintName, teamSpecialtyName);
            var issueGroupedByAssigneeUser = sprintIssues
                .Where(a=>a.AssigneeUser?.DisplayName != "" && a.AssigneeUser?.DisplayName != null)
                .GroupBy(y => y.AssigneeUser?.DisplayName).ToList();
            foreach (var issues in issueGroupedByAssigneeUser)
            {
                UsersModel userObj = new UsersModel();
                int issueCount = 0;
                foreach (var issue in issues)
                {
                    issueCount++;
                    userObj.AssigneeUserName = issue.AssigneeUser?.DisplayName;
                    userObj.TotalPoints+= Convert.ToInt64(issue.CustomFields
                        .Where(x => x.Name == "Story Points" || x.Name == "Story point estimate")
                        .FirstOrDefault()?.Values?.FirstOrDefault());
                    userObj.TotalIssues = issueCount;
                }
                userList.Add(userObj);
            }
            var sprintChampionesList = userList.OrderByDescending(x=>x.TotalPoints).Take(3).ToList();

            return Ok(sprintChampionesList);
        }

        private List<Issue> GetSprintIssues(string projectName, string sprintName = "", string teamSpecialtyName = "")
        {
            Jira jiraClient = Jira.CreateRestClient(appConfiguration.JiraURL, appConfiguration.JiraUsername, appConfiguration.JiraToken, new JiraRestClientSettings());
            string jqlString = PrepareJqlbyDates(projectName, sprintName, teamSpecialtyName);
            IssueSearchOptions options = new IssueSearchOptions(jqlString);
            options.StartAt = 0;
            options.MaxIssuesPerRequest = jiraClient.Issues.GetIssuesFromJqlAsync(options).Result.TotalItems;
            List<Issue> issuesList = new List<Issue>();
            IPagedQueryResult<Issue> partialPullRequests = jiraClient.Issues.GetIssuesFromJqlAsync(options).Result;
            issuesList.AddRange(partialPullRequests);
            return issuesList;
        }
        private string PrepareJqlbyDates(string project, string sprint, string teamSpecialtyName)
        {
            string jqlString = "project = " + project;

            if (!string.IsNullOrEmpty(sprint))
                jqlString += @" AND Sprint = " +"'" + sprint + "'";

            if (!string.IsNullOrEmpty(teamSpecialtyName))
                jqlString += @" AND labels = " + "'" + teamSpecialtyName + "'";

            return jqlString.Replace("\"", String.Empty);
        }
    }
}
