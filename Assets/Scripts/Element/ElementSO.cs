using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementsData", menuName = "ScriptableObjects/ElementsData", order = 1)]
public class ElementSO : ScriptableObject
{
    [SerializeField]
    private Element[] elements = new Element[37]
    {
       new Element(Element.Type.Red, 32),
       new Element(Element.Type.Black, 15),
       new Element(Element.Type.Red, 19),
       new Element(Element.Type.Black, 4),
       new Element(Element.Type.Red, 21),
       new Element(Element.Type.Black, 2),
       new Element(Element.Type.Red, 25),
       new Element(Element.Type.Black, 17),
       new Element(Element.Type.Red, 34),
       new Element(Element.Type.Black, 6),
       new Element(Element.Type.Red, 27),
       new Element(Element.Type.Black, 13),
       new Element(Element.Type.Red, 36),
       new Element(Element.Type.Black, 11),
       new Element(Element.Type.Red, 30),
       new Element(Element.Type.Black, 8),
       new Element(Element.Type.Red, 23),
       new Element(Element.Type.Black, 10),
       new Element(Element.Type.Red, 5),
       new Element(Element.Type.Black, 24),
       new Element(Element.Type.Red, 16),
       new Element(Element.Type.Black, 33),
       new Element(Element.Type.Red, 1),
       new Element(Element.Type.Black, 20),
       new Element(Element.Type.Red, 14),
       new Element(Element.Type.Black, 31),
       new Element(Element.Type.Red, 9),
       new Element(Element.Type.Black, 22),
       new Element(Element.Type.Red, 18),
       new Element(Element.Type.Black, 29),
       new Element(Element.Type.Red, 7),
       new Element(Element.Type.Black, 28),
       new Element(Element.Type.Red, 12),
       new Element(Element.Type.Black, 35),
       new Element(Element.Type.Red, 3),
       new Element(Element.Type.Black, 26),
       new Element(Element.Type.Zero, 0)

    };

    public Element[] ElementsData
    {
        get
        {
            return elements;
        }
    }


}
