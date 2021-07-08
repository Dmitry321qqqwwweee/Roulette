using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action = System.Action;
using Roullete;

namespace TableMove {
    public class Table : MonoBehaviour
    {
        public Action MoveToFortuneAction = default;

        public Action MoveToFieldAction  = default;

        public Action TableEndMove = default;

        private RectTransform rect = default;

        private const float Speed = 3000f;

        [SerializeField]
        private RoulleteController roulleteController = default;

        private float startPos = default;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();

            MoveToFortuneAction += MoveToFortune;

            MoveToFieldAction += MoveToField;
        }

        private void Start()
        {
            startPos = rect.anchoredPosition.y;
        }

        private void MoveToFortune()
        {
            StartCoroutine(MoveToRoullete(0, TypeTableMove.Wheel));
        }

        private void MoveToField()
        {
            StartCoroutine(MoveToRoullete(startPos, TypeTableMove.Field));
        }



        private IEnumerator MoveToRoullete(float endPosition, TypeTableMove typeTableMove)
        {
            Vector2 andVEctor = new Vector2(0, endPosition);

            while (rect.anchoredPosition != andVEctor)
            {
                rect.anchoredPosition = Vector2.MoveTowards(rect.anchoredPosition, andVEctor, Speed * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }

            rect.anchoredPosition = andVEctor;


            switch (typeTableMove)
            {
                case TypeTableMove.Wheel:
                    roulleteController.SpinAction.Invoke();
                    break;
                case TypeTableMove.Field:
                    TableEndMove.Invoke();
                    break;
            }
            
        }

        private void OnDestroy()
        {
            MoveToFortuneAction -= MoveToFortune;
            MoveToFieldAction -= MoveToField;

        }

        private enum TypeTableMove
        {
            Field,
            Wheel
        }
    }
}
