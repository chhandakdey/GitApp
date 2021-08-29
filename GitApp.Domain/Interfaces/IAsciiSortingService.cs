using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitApp.Domain.Interfaces
{
    /// <summary>
    /// Interface for Sorting Service
    /// </summary>
    public interface IAsciiSortingService
    {
        IEnumerable<KeyValuePair<string, int>> GetSorted(string statement);
    }
}
