using System.Collections.Generic;
using System.Linq;
using CodeBase.Models;

namespace CodeBase.Extensions
{
    public static class DataExtensions
    {
        public static List<DogFactData> AsFactDataList(this IEnumerable<DogFact> from)
        {
            List<DogFactData> to = new();
            
            for (int i = 0; i < from.Count(); i++)
            {
                DogFactData dogFactData = new DogFactData()
                {
                    Id = i + 1,
                    Name = from.ElementAt(i).attributes.name,
                    ServerId = from.ElementAt(i).id
                };
                
                to.Add(dogFactData);
            }

            return to;
        }
    }
}