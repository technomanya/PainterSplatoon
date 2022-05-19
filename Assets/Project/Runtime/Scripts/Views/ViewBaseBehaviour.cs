using UnityEngine;
using UnityEngine.Serialization;

namespace Views
{
    public abstract class ViewBaseBehaviour : MonoBehaviour
    {
        public GameObject panel;
        

        public virtual void Open()
        {
            panel.SetActive(true);
        }

        public virtual void Close()
        {
            panel.SetActive(false);
        }
    }
}
