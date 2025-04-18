using System;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public event Action<string> OnStartSceneLoading;
    public event Action OnNextSceneTransitionEnd;
    public event Action OnQuitGame;
    public event Action OnStartNewGame;
    public event Action OnLoadGame;
    public event Action OnGoToMenu;
    public event Action<AudioClip> OnPlaySound;
    public event Action<AudioClip> OnPlaySoundWithRandomPitch;
    public event Action OnStartWalking;
    public event Action OnStopWalking;
    public event Action OnChangeVolume;
    public event Action<bool> OnChangeQuality;
    public event Action OnResumeGame;
    public event Action<MinigameSelector> OnMinigameTrigger;
    public event Action<String> OnMinigameKeyPressed;
    public event Action OnGoToNextDay;
    public event Action<string> OnSetDayCounterTrigger;
    public event Action OnResetDay;

    public void StartSceneLoading(string sceneName)
    {
        OnStartSceneLoading?.Invoke(sceneName);
    }

    public void NextSceneTransitionEnd()
    {
        OnNextSceneTransitionEnd?.Invoke();
    }

    public void QuitGame()
    {
        OnQuitGame?.Invoke();
    }

    public void StartNewGame()
    {
        OnStartNewGame?.Invoke();
    }

    public void LoadGame()
    {
        OnLoadGame?.Invoke();
    }

    public void GoToMenu()
    {
        OnGoToMenu?.Invoke();
    }

    public void PlaySound(AudioClip clip)
    {
        OnPlaySound?.Invoke(clip);
    }

    public void PlaySoundWithRandomPitch(AudioClip clip)
    {
        OnPlaySoundWithRandomPitch?.Invoke(clip);
    }

    public void StartWalking()
    {
        OnStartWalking?.Invoke();
    }

    public void StopWalking()
    {
        OnStopWalking?.Invoke();
    }

    public void ChangeVolume()
    {
        OnChangeVolume?.Invoke();
    }

    public void ChangeQuality(bool direction)
    {
        OnChangeQuality?.Invoke(direction);
    }

    public void ResumeGame()
    {
        OnResumeGame?.Invoke();
    }

    public void TestEvent()
    {
        Debug.Log("Test event triggered");
    }

    public void TriggerMiniGame(MinigameSelector minigame)
    {
        OnMinigameTrigger?.Invoke(minigame);
    }

    public void PressMinigameKey(string key)
    {
        OnMinigameKeyPressed?.Invoke(key);
    }

    public void GoToNextDay()
    {
        OnGoToNextDay?.Invoke();
    }

    public void SetDayCounterTrigger(string trigger)
    {
        OnSetDayCounterTrigger?.Invoke(trigger);
    }

    public void ResetDay()
    {
        OnResetDay?.Invoke();
    }
}
