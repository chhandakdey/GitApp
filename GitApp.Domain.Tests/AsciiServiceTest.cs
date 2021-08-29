using GitApp.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GitApp.Domain.Tests
{
    public class AsciiServiceTest
    {   
        [Fact]
        public void AsciiSortingService_GetSorted_WordCount()
        {
            var logger = Mock.Of<ILogger<AsciiSortingService>>();
            var sortingService = new AsciiSortingService(logger);
            var result = sortingService.GetSorted("A Simple Test");
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void AsciiSortingService_GetSorted_NumberOfOccurences()
        {
            var logger = Mock.Of<ILogger<AsciiSortingService>>();
            var sortingService = new AsciiSortingService(logger);
            var result = sortingService.GetSorted("Hello Hello").FirstOrDefault();
            Assert.Equal(2, result.Value);
        }

        [Fact]
        public void AsciiSortingService_GetSorted_AsciiSort()
        {
            var logger = Mock.Of<ILogger<AsciiSortingService>>();
            var sortingService = new AsciiSortingService(logger);
            var result = sortingService.GetSorted("This is a new work").FirstOrDefault();
            Assert.Equal("a", result.Key);
        }
    }
}
