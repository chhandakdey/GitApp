﻿using GitApp.Application.DTOs;
using GitApp.Infrastructure.Adapters.Interfaces;
using GitApp.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for any GIT related operations
    /// </summary>
    public class GitRepository : IGitRepository
    {
        private readonly IApiClient _gitApiClient;       
        private readonly ILogger<GitRepository> _logger;
        private readonly IConfigurationSection _configurationSection;

        public GitRepository(IApiClient gitApiClient, ILogger<GitRepository> logger, IConfiguration configuration)
        {
            _gitApiClient = gitApiClient;
            _logger = logger;
            _configurationSection = configuration.GetSection("GitAppSettings");
        }
        /// <summary>
        /// Get all comments from Git
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetAllCommentsAsync(GitRequestDTO requestModel)
        {
            var targetUrl = BuildTargetUrl(requestModel);
            _logger.LogDebug($"GitRepository: GetAllCommentsAsync => {targetUrl}");
            return await _gitApiClient.SendAsync(targetUrl, HttpMethod.Get, GetHeaders(requestModel.Username, requestModel.AccessToken));
        }
        /// <summary>
        /// Build the target URL
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        private string BuildTargetUrl(GitRequestDTO requestModel)
        {
            var targetUrl = _configurationSection.GetValue<string>("GitCommentUrl");
            var gitInitialStr = _configurationSection.GetValue<string>("GitRepoInitialUrl");
            var gitEndStr = _configurationSection.GetValue<string>("GitRepoEndUrl");
            var repoName = requestModel.RepoUrl.Substring(gitInitialStr.Length);
            repoName = repoName.Substring(0, repoName.Length - gitEndStr.Length);
            return targetUrl.Replace("{repo}", repoName);
        }
        /// <summary>
        /// Making Http Headers
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private IEnumerable<KeyValuePair<string, string>> GetHeaders(string username, string password)
        {
            var headers = new List<KeyValuePair<string, string>>();
            var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
            var clientAuthrizationHeader = new AuthenticationHeaderValue("Basic",
                                                          Convert.ToBase64String(byteArray));
            headers.Add(new KeyValuePair<string, string>("Authorization", clientAuthrizationHeader.ToString()));
            headers.Add(new KeyValuePair<string, string>("Accept", _configurationSection.GetValue<string>("AcceptHeaderForComment")));
            headers.Add(new KeyValuePair<string, string>("User-Agent", "GitApp"));
            _logger.LogDebug($"GitRepository: GetHeaders => Headers are added");
            return headers;
        }
    }
}
