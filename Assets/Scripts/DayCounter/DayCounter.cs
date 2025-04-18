using System;
using TMPro;
using UnityEngine;
using Zenject;

public class DayCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text dayText;
    [SerializeField] private Animator daySceenAnim;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private Transform player, cam;
    [SerializeField] private string[] day1Triggers;
    [SerializeField] private string[] day2Triggers;
    [SerializeField] private string[] day3Triggers;
    [SerializeField] private string[] day4Triggers;
    [SerializeField] private string[] day5Triggers;
    [SerializeField] private string[] day6Triggers;
    [SerializeField] private string[] day7Triggers;

    public static DayCounter Instance;
    public int currentDay = 1;

    public bool _canSleep;
    private bool _isSleeping;
    private EventHandler _eventHandler;

    [Inject]
    public void Construct(EventHandler eventHandler)
    {
        _eventHandler = eventHandler;
        _eventHandler.OnGoToNextDay += GoToNextDay;
    }

    private void Awake()
    {
        Instance = this;
        currentDay = PlayerPrefs.GetInt("Day", 1);
    }

    public void GoToNextDay()
    {
        if (!_isSleeping)
        {
            if (CheckSleep())
            {
                Debug.Log("Sleeping");
                PlayerPrefs.SetFloat("PlayerPosX", player.position.x);
                PlayerPrefs.SetFloat("PlayerPosY", player.position.y);
                PlayerPrefs.SetFloat("PlayerPosZ", player.position.z);

                PlayerPrefs.SetFloat("PlayerRotY", player.localEulerAngles.y);
                PlayerPrefs.SetFloat("PlayerRotX", cam.localEulerAngles.x);

                currentDay++;
                PlayerPrefs.SetInt("Day", currentDay);

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

    public void ResetDay()
    {
        currentDay = 1;
        PlayerPrefs.DeleteKey("Day");
    }

    public void SetTrigger(string trigger)
    {
        string[] triggers;
        switch (currentDay)
        {
            case 1:
                triggers = day1Triggers;
                break;
            case 2:
                triggers = day2Triggers;
                break;
            case 3:
                triggers = day3Triggers;
                break;
            case 4:
                triggers = day4Triggers;
                break;
            case 5:
                triggers = day5Triggers;
                break;
            case 6:
                triggers = day6Triggers;
                break;
            case 7:
                triggers = day7Triggers;
                break;
            default:
                triggers = day1Triggers;
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
        switch (currentDay)
        {
            case 1:
                triggers = day1Triggers;
                break;
            case 2:
                triggers = day2Triggers;
                break;
            case 3:
                triggers = day3Triggers;
                break;
            case 4:
                triggers = day4Triggers;
                break;
            case 5:
                triggers = day5Triggers;
                break;
            case 6:
                triggers = day6Triggers;
                break;
            case 7:
                triggers = day7Triggers;
                break;
            default:
                triggers = day1Triggers;
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
