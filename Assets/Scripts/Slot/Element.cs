using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Slot
{
    public class Element : MonoBehaviour
    {
        private const int MOVING_TIME = 10;
        private const float DEFAULT_SPEED = 200f;
        private const float MIN_SPEED = 40f;
        private const float UP_STEP = 5f;
        private const float DOWN_STEP = .97f;

        private RectTransform rectTransform = default;
        private Image image = default;
        private float startPositionY = default;
        public TypeElement Type { get; private set; }
        private float _speed = DEFAULT_SPEED;
        private float speed
        {
            get => _speed;
            set
            {
                _speed = value;
            }
        }

        private float top = default;
        private float bottom = default;

        private Slot game = default;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            image = GetComponent<Image>();
            game = GetComponentInParent<Slot>();
            startPositionY = rectTransform.localPosition.y;
            top = game.top.localPosition.y;
            bottom = game.bottom.localPosition.y;
        }

        public void SetType()
        {
            Type = game.GetRandTypeElement();
            image.sprite = Type.Sprite;
        }

        public void Move()
        {
            StartCoroutine(MoveCoroutine());
        }

        private void MoveDown()
        {
            rectTransform.localPosition -= new Vector3(0f, speed, 0f);
            if (rectTransform.localPosition.y <= bottom) { MoveUp(); }
        }

        private void MoveUp()
        {
            rectTransform.localPosition = new Vector3(
                rectTransform.localPosition.x,
                rectTransform.localPosition.y + (top - bottom),
                0f);
            SetType();
        }

        private IEnumerator MoveCoroutine()
        {
            game.SpinCounter++;
            speed = 0;
            for (; speed < DEFAULT_SPEED;)
            {
                speed += UP_STEP;
                yield return new WaitForEndOfFrame();
                MoveDown();
            }
            for (var i = 0; i < MOVING_TIME; i++)
            {
                yield return new WaitForEndOfFrame();
                MoveDown();
            }
            for (; speed > MIN_SPEED;)
            {
                speed *= DOWN_STEP;
                yield return new WaitForEndOfFrame();
                MoveDown();
            }
            for (; Mathf.Abs(rectTransform.anchoredPosition.y - startPositionY) > speed;)
            {
                speed *= DOWN_STEP;
                yield return new WaitForEndOfFrame();
                MoveDown();
            }
            rectTransform.localPosition = new Vector3(rectTransform.localPosition.x, startPositionY, 0f);
            game.SpinCounter--;
            game.CheckEnd();
        }
    }
}