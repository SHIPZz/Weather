using System.Collections.Generic;

namespace CodeBase.Gameplay.Dogs
{
    public class DogService : IDogService
    {
        private readonly Dictionary<int, DogFact> _dogs = new();
        private int _nextDogId = 1;
        private DogFact _lastDog;
        

        public void Add(DogFact dogData)
        {
            _dogs[_nextDogId] = dogData;
            _nextDogId++;
        }

        public void Add(IEnumerable<DogFact> dogData)
        {
            foreach (var dogFact in dogData)
            {
                Add(dogFact);
            }
        }

        public void SetLastSelectedDog(DogFact data)
        {
            _lastDog = data;
        }

        public DogFact Get(int id) => _dogs[id];
        
        public DogFact GetLastSelectedDog() => _lastDog;

        public List<DogFact> GetAll() => new(_dogs.Values);
    }
}