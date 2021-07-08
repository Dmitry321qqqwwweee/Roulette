using UnityEngine;

[System.Serializable]
public class Element
{
    public Element (Type _type, int _number)
    {
        type = _type;
        number = _number;
    }

    public enum Type
    {
        Red,
        Black,
        Zero,
        DoubleZero,
    }

    [SerializeField]
    private Type type = default;
    [SerializeField]
    private int number = default;

    public Type ElementType => type;
    public int Number => number;
}
