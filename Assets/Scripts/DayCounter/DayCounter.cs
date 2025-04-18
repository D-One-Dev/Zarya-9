using System;
using UnityEngine;
using Zenject;

public class DayCounter : IInitializable
{
    [Inject(Id = "PlayerTransform")]
    private readonly Transform _playerTransform;
    [Inject(Id = "CameraTransform")]
    private readonly Transform _cameraTransform;
    [Inject(Id = "Day1Triggers")]
    private readonly string[] _day1Triggers;
    [Inject(Id = "Day2Triggers")]
    private readonly string[] _day2Triggers;
    [Inject(Id = "Day3Triggers")]
    private readonly string[] _day3Triggers;
    [Inject(Id = "Day4Triggers")]
    private readonly string[] _day4Triggers;
    [Inject(Id = "Day5Triggers")]
    private readonly string[] _day5Triggers;
    [Inject(Id = "Day6Triggers")]
    private readonly string[] _day6Triggers;
    [Inject(Id = "Day7Triggers")]
    private readonly string[] _day7Triggers;

    private bool _isSleeping;
    private EventHandler _eventHandler;

    public int CurrentDay { get; private set; } = 1;

    [Inject]
    public void Construct(EventHandler eventHandler)
    {
        _eventHandler = eventHandler;
        _eventHandler.OnGoToNextDay += GoToNextDay;
        _eventHandler.OnSetDayCounterTrigger += SetTrigger;
        _eventHandler.OnResetDay += ResetDay;
    }

    public void Initialize()
    {
        CurrentDay = PlayerPrefs.GetInt("Day", 1);
    }

    private void GoToNextDay()
    {
        if (!_isSleeping)
        {
            if (CheckSleep())
            {
                Debug.Log("Sleeping");
                PlayerPrefs.SetFloat("PlayerPosX", _playerTransform.position.x);
                PlayerPrefs.SetFloat("PlayerPosY", _playerTransform.position.y);
                PlayerPrefs.SetFloat("PlayerPosZ", _playerTransform.position.z);

                PlayerPrefs.SetFloat("PlayerRotY", _playerTransform.localEulerAngles.y);
                PlayerPrefs.SetFloat("PlayerRotX", _cameraTransform.localEulerAngles.x);

                CurrentDay++;
                PlayerPrefs.SetInt("Day", CurrentDay);

                PlayerPrefs.Save();
                _eventHandler.StartSceneLoading("Gameplay");

                _isSleeping = true;
            }
            else
            {
                Debug.Log("Can't sleep yet");
            }
        }
    }

    private void ResetDay()
    {
        CurrentDay = 1;
        PlayerPrefs.DeleteKey("Day");
        _eventHandler.StartSceneLoading("Gameplay");
    }

    private void SetTrigger(string trigger)
    {
        string[] triggers;
        switch (CurrentDay)
        {
            case 1:
                triggers = _day1Triggers;
                break;
            case 2:
                triggers = _day2Triggers;
                break;
            case 3:
                triggers = _day3Triggers;
                break;
            case 4:
                triggers = _day4Triggers;
                break;
            case 5:
                triggers = _day5Triggers;
                break;
            case 6:
                triggers = _day6Triggers;
                break;
            case 7:
                triggers = _day7Triggers;
                break;
            default:
                triggers = _day1Triggers;
                break;
        }

        if (Array.IndexOf(triggers, trigger) != -1)
        {
            triggers[Array.IndexOf(triggers, trigger)] = null;
        }
        else
        {
            Debug.LogError("Cannot find trigger " + trigger);
        }
    }

    private bool CheckSleep()
    {
        string[] triggers;
        switch (CurrentDay)
        {
            case 1:
                triggers = _day1Triggers;
                break;
            case 2:
                triggers = _day2Triggers;
                break;
            case 3:
                triggers = _day3Triggers;
                break;
            case 4:
                triggers = _day4Triggers;
                break;
            case 5:
                triggers = _day5Triggers;
                break;
            case 6:
                triggers = _day6Triggers;
                break;
            case 7:
                triggers = _day7Triggers;
                break;
            default:
                triggers = _day1Triggers;
                break;
        }

        foreach (string trigger in triggers)
        {
            if (trigger != null)
            {
                return false;
            }
        }

        return true;
    }
}
