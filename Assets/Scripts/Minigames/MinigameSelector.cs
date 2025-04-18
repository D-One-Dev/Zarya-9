using UnityEngine;
using Zenject;

public class MinigameSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] maps;
    [SerializeField] private IInteractable[] scripts;
    private IInteractable _currentScript;
    private bool _isOn;

    [Inject]
    public void Construct(EventHandler eventHandler)
    {
        eventHandler.OnMiniGameTrigger += SwitchState;
    }

    public void TurnOn()
    {
        int day = PlayerPrefs.GetInt("Day", 1);
        if (maps[day - 1] != null)
        {
            maps[day - 1].SetActive(true);
            _currentScript = maps[day - 1].GetComponent<IInteractable>();
            if (_currentScript == null) Debug.LogError("Cannot find IInteractable script on object " + gameObject);
            else _currentScript.TurnOn();
        }
    }

    public void TurnOff()
    {
        int day = PlayerPrefs.GetInt("Day", 1);
        _currentScript?.TurnOff();
        maps[day - 1].SetActive(false);
    }

    private void SwitchState(MinigameSelector minigame)
    {
        if (minigame == this)
        {
            Debug.Log(_isOn);
            if (_isOn) TurnOff();
            else TurnOn();
            _isOn = !_isOn;
        }
    }
}
