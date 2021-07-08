using UnityEngine;

namespace Slot
{
    public class Line : MonoBehaviour
    {
        public Vector2[] Points = default;

        public void Show()
        {
            gameObject.SetActive(true);
            Invoke(nameof(Hide), 3f);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
