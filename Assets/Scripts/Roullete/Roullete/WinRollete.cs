using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace Roullete
{
    public class WinRollete : MonoBehaviour
    {
        private const float numberOfCells = 37f;
        private const float step = 360f / numberOfCells;

        [SerializeField]
        private BallsController ballsController = default;

        public Action<RectTransform> CheckWinEvent { get; private set; }

        public Action<Element> WinValue = default;

        [SerializeField]
        private ElementSO elementSO = default;

        [SerializeField]
        private float[] roulleteStep = new float[38];

        [SerializeField]
        private UIRoulleteWIn uiRoulleteWin = default;

        private RoulleteController roulleteController = default;

        private void Awake()
        {
            CheckWinEvent += CheckWinValue;
            roulleteController = GetComponent<RoulleteController>();
        }

        private void CheckWinValue(RectTransform fortuneRect)
        { 
            var rotationFortune = RealAngle(fortuneRect.rotation.eulerAngles.z + step);
            var rotationBalls = RealAngle(ballsController.Rect.eulerAngles.z);

            rotationBalls -= rotationFortune;
            rotationBalls = RealAngle(rotationBalls);

            var rewardNumber = (int)(rotationBalls / step);
            rewardNumber = elementSO.ElementsData.Length - 2 - rewardNumber;
            rewardNumber = rewardNumber == -2 ? elementSO.ElementsData.Length - 2 : rewardNumber;
            rewardNumber = rewardNumber == -1 ? elementSO.ElementsData.Length - 1 : rewardNumber;

            if (rewardNumber == elementSO.ElementsData.Length)
            {
                rewardNumber = elementSO.ElementsData.Length - 1;
            }

            WinValue.Invoke(elementSO.ElementsData[rewardNumber]);
            VisibleRoulleteWin(elementSO.ElementsData[rewardNumber]);
            BallRoll(fortuneRect);
        }

        private void BallRoll(RectTransform fortuneRect)
        {
            roulleteStep[0] = fortuneRect.eulerAngles.z - step / 2;
            roulleteStep[0] -= 360 * (int)(roulleteStep[0] / 360);
            roulleteStep[0] += 360 * (int)(roulleteStep[0] / 360);

            for (int i = 1; i < roulleteStep.Length; i++)
            {
                roulleteStep[i] = roulleteStep[i - 1] + step;
                roulleteStep[i] -= 360 * (int)(roulleteStep[i] / 360);
                roulleteStep[i] += 360 * (int)(-roulleteStep[i] / 360);
            }

            var ballEuler = RealAngle(ballsController.Rect.eulerAngles.z);

            var nearest = roulleteStep.OrderBy(x => Math.Abs((long)x - ballEuler)).First();

            ballsController.MoveTurnBalls(nearest);
        }

        private void VisibleRoulleteWin(Element winElement)
        {
            uiRoulleteWin.OpenRoulleteValue(winElement);

            StartCoroutine(WinPanelOpenAndClose(winElement));
        }

        private IEnumerator WinPanelOpenAndClose(Element inputElement)
        {
            yield return new WaitForSeconds(1.5f);

            uiRoulleteWin.Close();

            yield return new WaitForSeconds(1f);

            roulleteController.EndSpinAction.Invoke();

        }

        private float RealAngle(float inputAngle)
        {
            inputAngle -= inputAngle >= 360 ? 360 : 0;
            inputAngle += inputAngle <= 0 ? 360 : 0;
            return inputAngle;
        }

        private void OnDestroy()
        {
            CheckWinEvent -= CheckWinValue;
        }
    }
}
