using BetaUtils.Core.Extensions;
using Xunit;

namespace BetaUtils.Tests.Extensions
{
    public class IEnumerableExtensionsTests
    {

        [Fact]
        public void IsNullOrEmpty_ReturnsTrue_WhenCollectionIsNull()
        {
            IEnumerable<int> items = null;
            bool result = IEnumerableExtensions.IsNullOrEmpty(items);
            Assert.True(result);
        }

        [Fact]
        public void IsNullOrEmpty_ReturnsTrue_WhenCollectionIsEmpty()
        {
            IEnumerable<int> items = Enumerable.Empty<int>();
            bool result = IEnumerableExtensions.IsNullOrEmpty(items);
            Assert.True(result);
        }

        [Fact]
        public void IsNullOrEmpty_ReturnsFalse_WhenCollectionIsNotNullOrEmpty()
        {
            IEnumerable<int> items = new List<int> { 1, 2, 3 };
            bool result = IEnumerableExtensions.IsNullOrEmpty(items);
            Assert.False(result);
        }

        [Fact]
        public void ForEach_WithNullItems_ReturnsEmptyEnumerable()
        {
            IEnumerable<int> items = null;

            var result = IEnumerableExtensions.ForEach(items, item => Console.WriteLine(item));

            Assert.Empty(result);
        }

        [Fact]
        public void ForEach_WithValidItemsAndAction_PerformsActionOnEachItem()
        {
            var items = new List<int> { 1, 2, 3 };
            var expectedOutput = "1\r\n2\r\n3\r\n";

           var output = new StringWriter();
            Console.SetOut(output);
            items.ForEach(item => Console.WriteLine(item));

            Assert.Equal(expectedOutput, output.ToString());
        }

        [Fact]
        public void ToHashSet_ReturnsHashSetWithExpectedItems()
        {
            var items = new List<int> { 1, 2, 3 };

            var result = IEnumerableExtensions.ToHashSet(items);

            Assert.Equal(3, result.Count);
            Assert.Contains(1, result);
            Assert.Contains(2, result);
            Assert.Contains(3, result);
        }

        [Fact]
        public void ToHashSet_ReturnsEmptyHashSetWhenInputIsEmpty()
        {
            var items = new List<int>();

            var result = IEnumerableExtensions.ToHashSet(items);

            Assert.Empty(result);
        }

        [Fact]
        public void ToObservableCollection_ReturnsObservableCollectionWithExpectedItems()
        {
            var items = new List<int> { 1, 2, 3 };

            var result = IEnumerableExtensions.ToObservableCollection(items);

            Assert.Equal(3, result.Count);
            Assert.Contains(1, result);
            Assert.Contains(2, result);
            Assert.Contains(3, result);
        }

        [Fact]
        public void ToObservableCollection_ReturnsEmptyObservableCollectionWhenInputIsEmpty()
        {
            var items = new List<int>();

            var result = IEnumerableExtensions.ToObservableCollection(items);

            Assert.Empty(result);
        }

        [Fact]
        public void Batch_ReturnsCorrectNumberOfBatches()
        {
            var items = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var result = IEnumerableExtensions.Batch(items, 3);

            Assert.Equal(4, result.Count());
        }

        [Fact]
        public void Batch_ReturnsCorrectItemsInBatches()
        {
            var items = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var result = IEnumerableExtensions.Batch(items, 3).ToList();

            Assert.Equal(4, result.Count);

            Assert.Equal(new List<int> { 1, 2, 3 }, result[0]);
            Assert.Equal(new List<int> { 4, 5, 6 }, result[1]);
            Assert.Equal(new List<int> { 7, 8, 9 }, result[2]);
            Assert.Equal(new List<int> { 10 }, result[3]);
        }

        [Fact]
        public void RemoveNulls_NullsExist_ReturnsNonNullItemsOnly()
        {
            var items = new List<int?> { 1, null, 2, null, 3 };
            var expected = "1,2,3";
            var result = string.Join(",", items.RemoveNulls());
            Assert.Equal(expected, result);
        }
    }
}
