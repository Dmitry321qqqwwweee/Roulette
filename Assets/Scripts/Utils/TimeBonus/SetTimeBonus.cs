using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SetTimeBonus : MonoBehaviour
{
    [SerializeField]
    private TimeBonus.Type type = default;

    private Button button = default;

    private TimeBonus.Bonus bonus = default;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Click);

        bonus = TimeBonus.Get(type);

        bonus.OnSetAvailible += SetInteractable;
    }

    private void Start()
    {
        SetInteractable(bonus.IsAvailible);
    }

    private void SetInteractable(bool _interactable) =>
        button.interactable = _interactable;

    private void Click() =>
        bonus.Get();
}
