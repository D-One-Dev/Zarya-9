using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenuController : IInitializable
{
    [Inject(Id = "LoadGameButton")]
    private readonly Button loadGameButton;

    private EventHandler _eventHandler;

    [Inject]
    public void Construct(EventHandler eventHandler)
    {
        _eventHandler = eventHandler;
        _eventHandler.OnQuitGame += QuitGame;
        _eventHandler.OnStartNewGame += StartNewGame;
        _eventHandler.OnLoadGame += LoadGame;
        _eventHandler.OnGoToMenu += GoToMenu;
    }

    public void Initialize()
    {
        if (loadGameButton != null)
        {
            if (PlayerPrefs.GetInt("Day", 1) == 1) loadGameButton.interactable = false;
            else loadGameButton.interactable = true;
        }
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void StartNewGame()
    {
        PlayerPrefs.SetFloat("PlayerPosX", -10000f);
        PlayerPrefs.SetFloat("PlayerPosY", -10000f);
        PlayerPrefs.SetFloat("PlayerPosZ", -10000f);

        PlayerPrefs.SetFloat("PlayerRotY", -10000f);
        PlayerPrefs.SetFloat("PlayerRotX", -10000f);

        PlayerPrefs.SetInt("Day", 1);
        PlayerPrefs.Save();
        LoadGame();
    }

    private void LoadGame()
    {
        _eventHandler.StartSceneLoading("Gameplay");
    }

    private void GoToMenu()
    {
        _eventHandler.StartSceneLoading("Menu");
    }
}