using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;

        private readonly List<IGrouping<(Color Color, Size Size), Shirt>> _groupShirts;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;

            _groupShirts = _shirts?.GroupBy(x => (x.Color, x.Size)).ToList();
        }


        public SearchResults Search(SearchOptions options)
        {
            var result = new SearchResults();

            if (options != null && _shirts != null)
            {
                result.Shirts = _groupShirts.Where(gs => 
                                        (!options.Colors.Any() || options.Colors.Any(c => c.Id == gs.Key.Color.Id)) &&
                                        (!options.Sizes.Any() || options.Sizes.Any(c => c.Id == gs.Key.Size.Id)))
                                    .SelectMany(y => y).ToList();

                result.SizeCounts = Size.All.Select(x => new SizeCount {
                    Size = x,
                    Count = result.Shirts.Count(y => y.Size.Id == x.Id)
                }).ToList();

                result.ColorCounts = Color.All.Select(x => new ColorCount {
                    Color = x,
                    Count = result.Shirts.Count(y => y.Color.Id == x.Id)
                }).ToList();

            };

            return result;
        }
    }
}