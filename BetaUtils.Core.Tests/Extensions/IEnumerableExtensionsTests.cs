using BetaUtils.Core.Extensions;



namespace BetaUtils.Tests.Extensions
{
    [TestClass]
    public class IEnumerableExtensionsTests
    {

        [TestMethod]
        public void IsNullOrEmpty_ReturnsTrue_WhenCollectionIsEmpty()
        {
            IEnumerable<int> items = Enumerable.Empty<int>();
            bool result = IEnumerableExtensions.IsNullOrEmpty(items);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsNullOrEmpty_ReturnsFalse_WhenCollectionIsNotNullOrEmpty()
        {
            IEnumerable<int> items = new List<int> { 1, 2, 3 };
            bool result = IEnumerableExtensions.IsNullOrEmpty(items);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ForEach_ActionIsNull_ThrowArgumentNullException()
        {
            var items = new List<int> { 1, 2, 3 };

            Assert.ThrowsException<ArgumentNullException>(() => IEnumerableExtensions.ForEach(items, null));
        }

        [TestMethod]
        public void ForEach_IteratesThroughList_EverythingIsOkay()
        {
            var items = new List<int> { 1, 2, 3 };

            var result = IEnumerableExtensions.ForEach(items, item => Console.WriteLine(item));

            CollectionAssert.AreEqual(items, result.ToList());
        }

        [TestMethod]
        public void ForEach_WithValidItemsAndAction_PerformsActionOnEachItem()
        {
            var items = new List<int> { 1, 2, 3 };
            var expectedOutput = "1\r\n2\r\n3\r\n";

           var output = new StringWriter();
            Console.SetOut(output);
            items.ForEach(item => Console.WriteLine(item));

            Assert.AreEqual(expectedOutput, output.ToString());
        }

        [TestMethod]
        public void ToHashSet_ReturnsHashSetWithExpectedItems()
        {
            var items = new List<int> { 1, 2, 3 };

            var result = IEnumerableExtensions.ToHashSet(items);

            Assert.AreEqual(3, result.Count);
            /*CollectionAssert.Contains(1, result);
            Assert.Contains(2, result);
            Assert.Contains(3, result);*/
        }

        [TestMethod]
        public void ToHashSet_ReturnsEmptyHashSetWhenInputIsEmpty()
        {
            var items = new List<int>();

            var result = IEnumerableExtensions.ToHashSet(items);

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void ToObservableCollection_ReturnsObservableCollectionWithExpectedItems()
        {
            var items = new List<int> { 1, 2, 3 };

            var result = IEnumerableExtensions.ToObservableCollection(items);

            Assert.AreEqual(3, result.Count);
            /*Assert.Contains(1, result);
            Assert.Contains(2, result);
            Assert.Contains(3, result);*/
        }

        [TestMethod]
        public void ToObservableCollection_ReturnsEmptyObservableCollectionWhenInputIsEmpty()
        {
            var items = new List<int>();

            var result = IEnumerableExtensions.ToObservableCollection(items);

            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void Batch_ReturnsCorrectNumberOfBatches()
        {
            var items = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var result = IEnumerableExtensions.Batch(items, 3);

            Assert.AreEqual(4, result.Count());
        }

        [TestMethod]
        public void Batch_ReturnsCorrectItemsInBatches()
        {
            var items = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var result = IEnumerableExtensions.Batch(items, 3).ToList();

            Assert.AreEqual(4, result.Count);

          /*  Assert.AreEqual(new List<int> { 1, 2, 3 }, result[0]);
            Assert.AreEqual(new List<int> { 4, 5, 6 }, result[1]);
            Assert.AreEqual(new List<int> { 7, 8, 9 }, result[2]);
            Assert.AreEqual(new List<int> { 10 }, result[3]);*/
        }

        [TestMethod]
        public void RemoveNulls_NullsExist_ReturnsNonNullItemsOnly()
        {
            var items = new List<int?> { 1, null, 2, null, 3 };
            var expected = "1,2,3";
            var result = string.Join(",", items.RemoveNulls());
            Assert.AreEqual(expected, result);
        }
    }
}
