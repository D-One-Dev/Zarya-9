using Sylphiette;
using UnityEngine;
using Zenject;

public class RocketRoomDoor : MonoBehaviour
{
    [SerializeField] private DoorCollider doorCollider;

    private DayCounter _dayCounter;

    [Inject]
    public void Construct(DayCounter dayCounter)
    {
        _dayCounter = dayCounter;
    }

    private void Start()
    {
        if (_dayCounter.CurrentDay > 2) doorCollider.locked = false;
        else doorCollider.locked = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            if (item.itemName == "Шестеренка")
            {
                doorCollider.locked = false;
                SylphietteDialogueSystem.Instance.StartNextDialogue();
                Destroy(other.gameObject);
            }
        }
    }
}