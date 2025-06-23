using UnityEngine;

namespace Core.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class UIElement : MonoBehaviour
    {
        [SerializeField] private int sortOrder = 0;

        public int SortOrder => sortOrder;


        public virtual void Show()
        {
            this.gameObject.SetActive(true);
        }


        public virtual void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}
