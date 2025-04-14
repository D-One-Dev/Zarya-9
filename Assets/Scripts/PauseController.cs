using UnityEngine;
using Zenject;
//using UnityEngine.Rendering.PostProcessing;

public class PauseController
{
    [Inject(Id = "PauseScreen")]
    private readonly GameObject _pauseScreen;
    [Inject(Id = "SettingsScreen")]
    private readonly GameObject _settingsScreen;

    private Controls _controls;
    private EventHandler _eventHandler;

    private bool _isPaused;

    [Inject]
    public void Construct(Controls controls, EventHandler eventHandler)
    {
        _controls = controls;
        _eventHandler = eventHandler;
        _eventHandler.OnGoToMenu += GoToMenu;
        _eventHandler.OnResumeGame += PlayPause;

        _controls.Gameplay.Esc.performed += ctx => PlayPause();
        _controls.Enable();
    }

    // private void Awake()
    // {
    //     _controls = new Controls();
    //     _controls.Gameplay.Esc.performed += ctx => PlayPause();
    // }
    // private void OnEnable()
    // {
    //     _controls.Enable();
    // }
    // private void OnDisable()
    // {
    //     _controls.Disable();
    // }

    public void PlayPause()
    {
        if (!_isPaused)
        {
            Time.timeScale = 0f;
            _pauseScreen.SetActive(true);
            _settingsScreen.SetActive(false);
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
            _isPaused = true;
        }
        else
        {
            _pauseScreen.SetActive(false);
            Time.timeScale = 1f;
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
            _isPaused = false;
        }
    }

    private void GoToMenu()
    {
        Time.timeScale = 1f;
        _eventHandler.StartSceneLoading("Menu");
    }
}
