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
}
