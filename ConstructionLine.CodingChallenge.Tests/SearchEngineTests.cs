using System;
using System.Collections.Generic;
using ConstructionLine.CodingChallenge.Tests.SampleData;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchEngineTests : SearchEngineTestsBase
    {
        private List<Shirt> _shirts;

        [SetUp]
        public void Setup()
        {

            var dataBuilder = new SampleDataBuilder(100);

            _shirts = dataBuilder.CreateShirts();
        }

        [Test]
        public void Test()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Medium", Size.Medium, Color.Black),
                new Shirt(Guid.NewGuid(), "Blue - Large", Size.Large, Color.Blue),
            };

            var searchEngine = new SearchEngine(shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> {Color.Red},
                Sizes = new List<Size> {Size.Small}
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void Null_Data_Test()
        {
            var searchEngine = new SearchEngine(null);

            var results = searchEngine.Search(new SearchOptions { Colors = new List<Color> { Color.Red } });

            Assert.IsNotNull(results);
            Assert.IsNull(results.Shirts);
            Assert.IsNull(results.SizeCounts);
            Assert.IsNull(results.ColorCounts);
        }

        [Test]
        public void Search_Only_WithOut_SizeOrColor()
        {
            var searchEngine = new SearchEngine(_shirts);

            var searchOptions = new SearchOptions();

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(_shirts, searchOptions, results.ColorCounts);
        }


        [Test]
        public void Search_Only_WithSize()
        {
            var searchEngine = new SearchEngine(_shirts);

            var searchOptions = new SearchOptions
            {
                Sizes = new List<Size> { Size.Medium, Size.Large }
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(_shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void Search_Only_WithColors()
        {
            var searchEngine = new SearchEngine(_shirts);

            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Blue, Color.Yellow }
            };

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
            AssertSizeCounts(_shirts, searchOptions, results.SizeCounts);
            AssertColorCounts(_shirts, searchOptions, results.ColorCounts);
        }

        [Test]
        public void Search_Without_Options()
        {
            var searchEngine = new SearchEngine(_shirts);

            var results = searchEngine.Search(null);

            Assert.IsNotNull(results);
            Assert.IsNull(results.Shirts);
            Assert.IsNull(results.SizeCounts);
            Assert.IsNull(results.ColorCounts);
        }
    }
}
