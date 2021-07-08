using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roullete
{
    public class BallsController : Wheel
    {
        private const float moveTop = 180f;
        private const float speed = 200f;

        [SerializeField]
        private Fortune fortune = default;

        protected override void Awake()
        {
            base.Awake();
            stopAngularVelocity = 20;
            rb.angularDrag = 0.6f;
        }

        protected override void EndSpin()
        {
            StartCoroutine(MoveToFortune());
        }

        private IEnumerator MoveToFortune()
        {
            Vector2 movePos = new Vector2(0, -moveTop);

            while(rect.sizeDelta != movePos)
            {
                rect.sizeDelta = Vector2.MoveTowards(rect.sizeDelta, movePos, speed * Time.deltaTime);

                yield return new WaitForEndOfFrame();
            }

            rb.angularDrag = fortune.RB.angularDrag;
            rb.angularVelocity = fortune.RB.angularVelocity;

            yield return null;
        }

        public void MoveTurnBalls(float angle)
        {
            StartCoroutine(MoveTurn(angle));
        }

        private IEnumerator MoveTurn(float angle)
        {
            Quaternion angleQuaternion = Quaternion.Euler(0, 0, angle);

            while (Mathf.Abs(rect.eulerAngles.z - angle) > 0.1f)
            {
                rect.rotation = Quaternion.Slerp(rect.rotation, angleQuaternion, 0.1f);
                yield return new WaitForEndOfFrame();
            }
        }

        public void ReturnBall()
        {
            if (isSpining == true) { return; }

            rect.sizeDelta = new Vector3(0, 0, 0);

            rect.rotation = Quaternion.Euler(0, 0, 0);

            rb.angularDrag = angularDragConst;
        }


    }
}
