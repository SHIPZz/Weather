using UnityEngine;

namespace CodeBase.UI.Facts
{
    public interface IFactUIFactory
    {
        FactItemView CreateFactItem(Transform parent, FactData factData);
    }
}