using GitApp.Application.DTOs;
using GitApp.Application.Interfaces;
using GitApp.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GitApp.Infrastructure.Daos
{
    public class GitDao : IDao<GitRequestDTO,IEnumerable<GitCommentDTO>>
    {
        private readonly ILogger<GitDao> _logger;
        private readonly IGitRepository _gitRepository;
        public GitDao(ILogger<GitDao> logger, IGitRepository gitRepository)
        {
            _logger = logger;
            _gitRepository = gitRepository;
        }
        public async Task<IEnumerable<GitCommentDTO>> GetAllAsync(GitRequestDTO gitRequestModel)
        {
            var response = await _gitRepository.GetAllCommentsAsync(gitRequestModel);
            _logger.LogDebug($"GitDao: GetAllAsync => Response is returned");
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var responseData = await JsonSerializer.DeserializeAsync
                <IEnumerable<JsonElement>>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            _logger.LogDebug($"GitDao: GetAllAsync => Data is serialized {responseData}");
            List<GitCommentDTO> commentList = new List<GitCommentDTO>();
            foreach(var data in responseData)
            {
                int commentId;
                if (data.GetProperty("id").TryGetInt32(out commentId)) {
                    var comment = data.GetProperty("body").GetString();
                    if(!string.IsNullOrEmpty(comment))
                    {
                        commentList.Add(new GitCommentDTO { Id = commentId, Message = comment });
                    }
                }                
            }
            _logger.LogDebug($"GitDao: GetAllAsync => DTO Object is created");
            return commentList;
        }
    }
}
