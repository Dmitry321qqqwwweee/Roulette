using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Slot
{
    public class WinPanel : MonoBehaviour
    {
        private const string WIN_TEXT = "WIN";
        private const string LOSE_TEXT = "LOSE";

        [SerializeField]
        private Text winLoseText = default;
        [SerializeField]
        private Text gainText = default;

        public void Set(int _gain)
        {
            winLoseText.text = _gain < 0 ? LOSE_TEXT : WIN_TEXT;
            gainText.text = _gain + "";
        }
    }
}