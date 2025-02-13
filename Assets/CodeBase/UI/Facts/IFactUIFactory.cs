using CodeBase.Models;
using UnityEngine;

namespace CodeBase.UI.Facts
{
    public interface IDogFactUIFactory
    {
        DogFactItemView CreateFactItem(Transform parent, DogFactData dogFactData);
    }
}