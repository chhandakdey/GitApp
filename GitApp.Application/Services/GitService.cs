using GitApp.Application.DTOs;
using GitApp.Application.Interfaces;
using GitApp.Application.Transformers.Interfaces;
using GitApp.Domain;
using GitApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Application.Services
{
    public class GitService : IGitService
    {
        private readonly IAsciiSortingService _asciiService;        
        private readonly IDao<GitRequestDTO, IEnumerable<GitCommentDTO>> Dao;     

        public GitService(IDao<GitRequestDTO, IEnumerable<GitCommentDTO>> dao, IAsciiSortingService asciiService)
        {
            Dao = dao;
            _asciiService = asciiService;
        }
        public async Task<IEnumerable<GitResultCommentDTO>> GetCommitMessagesAsync(GitRequestDTO model)
        {
            var resultCommitList = await Dao.GetAllAsync(model);
            List<GitComment> listComment = new();
            foreach(var singleCommit in resultCommitList)
            {
                var sortedList = _asciiService.GetSorted(singleCommit.Message);
                List<CommentWord> lstCommentWord = new();
                foreach(var sorted in sortedList)
                {
                    CommentWord word = new();
                    word.Word = sorted.Key;
                    word.Count = sorted.Value;
                    lstCommentWord.Add(word);
                }
                GitComment comment = new();
                comment.Comment = singleCommit.Message;
                comment.Id = singleCommit.Id;
                comment.SortedWords = lstCommentWord;
                listComment.Add(comment);
            }
            var lstResultDTO = new List<GitResultCommentDTO>();
            foreach(var comment in listComment) 
            {
                var resultDto = new GitResultCommentDTO();
                resultDto.Comment = comment.Comment;
                resultDto.Id = comment.Id;
                var sortedString = string.Empty;
                foreach(var word in comment.SortedWords)
                {
                    if(!string.IsNullOrWhiteSpace(word.Word))
                        sortedString = sortedString + word.Word + "(" + word.Count + "), ";
                }
                if(sortedString.Length > 1)
                    resultDto.SortedWords = sortedString.Substring(0, sortedString.Length - 2);
                lstResultDTO.Add(resultDto);
            }
            return lstResultDTO;
        }
    }
}
