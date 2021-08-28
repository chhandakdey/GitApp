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
        private readonly ITransformer _transformer;
        private readonly IDao<GitRequestDTO, IEnumerable<GitCommentDTO>> Dao;     

        public GitService(IDao<GitRequestDTO, IEnumerable<GitCommentDTO>> dao, IAsciiSortingService asciiService, ITransformer transformer)
        {
            Dao = dao;
            _asciiService = asciiService;
            _transformer = transformer;
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
                    sortedString = sortedString + word.Word + "(" + word.Count + "), ";
                }
                resultDto.SortedWords = sortedString;
                lstResultDTO.Add(resultDto);
            }
            return lstResultDTO;
        }
    }
}
