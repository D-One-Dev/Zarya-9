using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneLoader
{
    [Inject(Id = "BlackScreenAnimator")]
    private readonly Animator _blackScreenAnimator;
    private AsyncOperation _loadingSceneOperation;
    private bool _isLoading;

    [Inject]
    public void Construct(EventHandler eventHandler)
    {
        eventHandler.OnStartSceneLoading += StartSceneLoading;
    }

    private void StartSceneLoading(string scene)
    {
        if (!_isLoading)
        {
            _isLoading = true;
            _blackScreenAnimator.SetTrigger("FadeIn");
            _loadingSceneOperation = SceneManager.LoadSceneAsync(scene);
            _loadingSceneOperation.allowSceneActivation = false;
        }
    }

    public void FadeInEnd()
    {
        _loadingSceneOperation.allowSceneActivation = true;
    }
}
