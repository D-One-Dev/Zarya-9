using UnityEngine;
using Zenject;

public class Rocket : MonoBehaviour
{
    //[SerializeField] private SceneLoader sceneLoader;

    private EventHandler _eventHandler;

    [Inject]
    public void Construct(EventHandler eventHandler)
    {
        _eventHandler = eventHandler;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            if (item.itemName == "Ключ доступа")
            {
                //sceneLoader.StartSceneLoading("Game End");
                _eventHandler.StartSceneLoading("Game End");
            }
        }
    }
}