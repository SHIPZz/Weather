using System.Collections.Generic;
using CodeBase.UI.Facts;

namespace CodeBase.Extensions
{
    public static class DataExtensions
    {
        public static List<FactData> AsFactDataList(this List<DogFact> from)
        {
            List<FactData> to = new();
            
            for (int i = 0; i < from.Count; i++)
            {
                FactData factData = new FactData()
                {
                    Id = i + 1,
                    Name = from[i].attributes.name,
                    Description = from[i].attributes.description,
                    ServerId = from[i].id
                };
                
                to.Add(factData);
            }

            return to;
        }
    }
}