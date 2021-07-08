using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action = System.Action;

namespace Roullete {

    public class Fortune : Wheel
    {
        [SerializeField]
        private WinRollete winRoullete = default;

        public Action EndSpinFortuneEvent { get; set; }

        protected override void Awake()
        {
            base.Awake();
            EndSpinFortuneEvent += EndSpin;
            rb.angularDrag = 0.4f;
            stopAngularVelocity = 0f;
        }

        public Rigidbody2D RB
        {
            get
            {
                return rb;
            }
        }

        protected override void EndSpin()
        {
            winRoullete.CheckWinEvent.Invoke(rect);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            EndSpinFortuneEvent -= EndSpin;
        }
    }
}
