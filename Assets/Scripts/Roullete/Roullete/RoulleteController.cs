using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TableMove;

namespace Roullete
{
    [RequireComponent(typeof(WinRollete))]
    public class RoulleteController : MonoBehaviour
    {
        [SerializeField]
        private Fortune fortune = default;

        [SerializeField]
        private BallsController ballsController = default;

        public Action SpinAction { get; private set; } = default;

        public Action EndSpinAction { get; private set; } = default;

        [SerializeField]
        private Table table = default;

        private void Awake()
        {
            SpinAction += Spin;
            EndSpinAction += EndSpin;
        }

        private void Spin()
        {
            ballsController.ReturnBall();
            fortune.Spin.Invoke();
            ballsController.Spin.Invoke();
        }

        private void EndSpin()
        {
            ballsController.ReturnBall();
            table.MoveToFieldAction.Invoke();
        }

        private void OnDestroy()
        {
            SpinAction -= SpinAction;
            EndSpinAction -= EndSpin;
        }
    }
}
