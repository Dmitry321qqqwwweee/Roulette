using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action = System.Action;

namespace Roullete
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Wheel : MonoBehaviour
    {
        protected Rigidbody2D rb = default;
        protected RectTransform rect = default;

        public RectTransform Rect
        {
            get
            {
                return rect;
            }
        }

        [SerializeField]
        private Vector2Int rangeForce = default;

        [SerializeField]
        private DirectionSpining directionSpining = default;

        protected float angularDragConst = 0.4f;

        protected bool isSpining { get; private set; }

        protected float stopAngularVelocity = 0f;

        public Action Spin { get; private set; }

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

            rect = GetComponent<RectTransform>();

            rb.gravityScale = 0;

            rb.angularDrag = 0.4f;

            Spin += StartSpin;
        }

        private void StartSpin()
        {
            if (isSpining == true) { return; }

            isSpining = true;

            rb.angularVelocity = ReturnAngular();

            StartCoroutine(CheckSpining());
        }

        private float ReturnAngular()
        {
            float forceAng = Random.Range(rangeForce.x, rangeForce.y);

            forceAng = directionSpining == DirectionSpining.ClockWise ? forceAng : forceAng * -1;

            return forceAng;
        }

        private IEnumerator CheckSpining()
        {
            while (Mathf.Abs(rb.angularVelocity) > stopAngularVelocity)
            {
                yield return new WaitForEndOfFrame();
            }

            isSpining = false;

            EndSpin();
            
        }

        

       protected virtual void EndSpin() {  }


        protected virtual void OnDestroy()
        {
            Spin -= StartSpin;
        }


    }


    [System.Serializable]
    public enum DirectionSpining
    {
        ClockWise,
        CounterClockWise
    }
}
