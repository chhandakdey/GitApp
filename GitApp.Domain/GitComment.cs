using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Domain
{
    public class GitComment
    {
        public long Id { get; set; }
        public string Comment { get; set; }
        public string FineComment { get; set; }
        public IEnumerable<CommentWord> SortedWords { get; set; }
    }

    public class CommentWord
    {
        public string Word { get; set; }
        public int Count { get; set; }
    }
}
