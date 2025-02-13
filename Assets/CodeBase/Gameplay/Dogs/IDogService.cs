using System.Collections.Generic;

namespace CodeBase.Gameplay.Dogs
{
    public interface IDogService
    {
        DogFact Get(int id);
        List<DogFact> GetAll();
        void Add(IEnumerable<DogFact> dogData);
        void SetLastSelectedDog(DogFact data);
        DogFact GetLastSelectedDog();
    }
}