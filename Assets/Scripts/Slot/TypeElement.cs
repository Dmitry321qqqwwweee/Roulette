using UnityEngine;
using System;

namespace Slot
{
    [Serializable]
    public class TypeElement
    {
        public enum TypeOfElements
        {
            Normal,
            Scatter,
            Wild,
            Bonus
        }

        [Serializable]
        public class ElementValue
        {
            public int Count = default;
            public float Value = default;
        }

        public TypeOfElements TypeOfElement = default;
        [HideInInspector]
        public int Id = default;
        public float Probability = 1;
        public Sprite Sprite = default;
        [SerializeField]
        private ElementValue[] elementValues = default;

        public float GetScore(int count)
        {
            int maxCount = 0;
            float value = 0;
            foreach (var elementValue in elementValues)
            {
                if ((count >= elementValue.Count) && (maxCount < elementValue.Count))
                {
                    maxCount = elementValue.Count;
                    value = elementValue.Value;
                }
            }
            return value;
        }
    }
}