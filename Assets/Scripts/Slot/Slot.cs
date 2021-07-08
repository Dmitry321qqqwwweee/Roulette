using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Slot
{
    public class Slot : MonoBehaviour
    {
        [SerializeField]
        private int _HEIGHT = 5;

        [SerializeField]
        private int HEIGHT = 3;
        [SerializeField]
        private int WIDTH = 5;

        public Action<int> OnWin = (int _gain) => { };

        private const int BONUS_COUNT = 5;
        public Action<int> OnGetBonus = (int _gain) => { };

        private List<Element> _elements = new List<Element>();
        private List<List<Element>> elementsBig = new List<List<Element>>();
        private List<List<Element>> elements = new List<List<Element>>();

        [SerializeField]
        private SlotConfig slotConfig = default;
        private SlotConfig SlotConfig
        {
            get => slotConfig;
            set
            {
                slotConfig = value;
                typeElements = value.TypeElements;
            }
        }

        private IList<TypeElement> typeElements = default;
        [SerializeField]
        private Line[] lines = default;

        [HideInInspector]
        public int SpinCounter = 0;

        public RectTransform top = default;
        public RectTransform bottom = default;

        public int Bet = default;

        private void Awake()
        {
            SlotConfig = SlotConfig;

            for (var i = 0; i < typeElements.Count; i++)
            {
                typeElements[i].Id = i;
            }
        }

        private void Start()
        {
            FillElementsList();
        }

        public void Spin()
        {
            if (SpinCounter == 0)
            {
                StartCoroutine(SpinCoroutine());
                foreach (var line in lines)
                {
                    line.Hide();
                }
            }
        }

        public TypeElement GetRandTypeElement()
        {
            float rand = UnityEngine.Random.Range(0, GetProbability(typeElements));
            foreach (var el in typeElements)
            {
                if (rand < el.Probability) { return el; }
                else { rand -= el.Probability; }
            }
            return typeElements[0];
        }

        private float GetProbability(IList<TypeElement> elements)
        {
            float sum = 0f;
            foreach (var el in elements)
            {
                sum += el.Probability;
            }
            return sum;
        }

        private void FillElementsList()
        {
            _elements.AddRange(GetComponentsInChildren<Element>());
            foreach (var element in _elements)
            {
                element.SetType();
            }
            for (var i = 0; i < WIDTH; i++)
            {
                elementsBig.Add(new List<Element>());
            }
            for (var i = 0; i < _HEIGHT; i++)
            {
                for (var j = 0; j < WIDTH; j++)
                {
                    elementsBig[j].Add(_elements[i + j * _HEIGHT]);
                }
            }
        }

        public void CheckEnd()
        {
            if (SpinCounter != 0) { return; }
            FillEndList();
            float winCoins = 0f;
            int winFreeSpins = 0;
            int bonuses = 0;
            bool riskGame = false;
            for (var l = 0; l < lines.Length; l++)
            {
                var line = lines[l];
                var typeElementsInt = new List<int>();
                foreach (var typeElement in typeElements)
                {
                    typeElementsInt.Add(0);
                }
                foreach (var point in line.Points)
                {
                    typeElementsInt[elements[(int)point.x][(int)point.y].Type.Id]++;
                    if (elements[(int)point.x][(int)point.y].Type.TypeOfElement == TypeElement.TypeOfElements.Wild)
                    {
                        for (var i = 0; i < typeElementsInt.Count; i++) { typeElementsInt[i]++; }
                    }
                }
                for (var i = 0; i < typeElementsInt.Count; i++)
                {
                    if (typeElements[i].GetScore(typeElementsInt[i]) > 0)
                    {
                        line.Show();
                        foreach (var point in line.Points)
                        {
                            if (elements[(int)point.x][(int)point.y].Type.TypeOfElement == TypeElement.TypeOfElements.Wild) { riskGame = true; }
                        }
                        switch (typeElements[i].TypeOfElement)
                        {
                            case TypeElement.TypeOfElements.Normal:
                                winCoins += typeElements[i].GetScore(typeElementsInt[i]) + Bet;
                                break;
                            case TypeElement.TypeOfElements.Scatter:
                                winFreeSpins += (int)typeElements[i].GetScore(typeElementsInt[i]);
                                break;
                        }
                    }
                }
            }
            for (var i = 0; i < WIDTH; i++)
            {
                for (var j = 0; j < HEIGHT; j++)
                {
                    if (elements[i][j].Type.TypeOfElement == TypeElement.TypeOfElements.Bonus)
                    {
                        bonuses++;
                    }
                }
            }
            //if (bonuses >= BONUS_COUNT) { OnGetBonus((int)winCoins); }
            OnWin((int)winCoins);
            if (riskGame) { OnGetBonus((int)winCoins); }
        }
        
        private void FillEndList()
        {
            elements = new List<List<Element>>();
            for (var i = 0; i < WIDTH; i++)
            {
                elements.Add(new List<Element>());
                for (var j = _HEIGHT - HEIGHT; j < _HEIGHT; j++)
                {
                    elements[i].Add(elementsBig[i][j]);
                }
            }
        }

        private IEnumerator SpinCoroutine()
        {
            for (var i = 0; i < WIDTH; i++)
            {
                for (var j = 0; j < _HEIGHT; j++)
                {
                    elementsBig[i][j].Move();
                }
                yield return new WaitForSeconds(.1f);
            }
        }
    }
}
