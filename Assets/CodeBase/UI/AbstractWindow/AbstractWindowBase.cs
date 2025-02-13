using UnityEngine;

namespace CodeBase.UI.AbstractWindow
{
    public abstract class AbstractWindowBase : MonoBehaviour
    {
        public virtual void Open()
        {
            
        }

        public virtual void Close()
        {
            Destroy(gameObject);
        }
    }
}