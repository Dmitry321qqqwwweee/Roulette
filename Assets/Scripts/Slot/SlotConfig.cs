using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/SlotConfig")]
public class SlotConfig : ScriptableObject
{
    [SerializeField]
    private List<Slot.TypeElement> typeElements = default;
    public IList<Slot.TypeElement> TypeElements => typeElements.AsReadOnly();
}
