using UnityEngine;
using Zenject;

public class GameEndInstaller : MonoInstaller
{
    [SerializeField] private EventHandler eventHandler;
    [SerializeField] private Animator blackScreenAnimator;
    public override void InstallBindings()
    {
        Container.Bind<EventHandler>()
        .FromInstance(eventHandler)
        .AsSingle()
        .NonLazy();

        Container.Bind<Animator>()
            .WithId("BlackScreenAnimator")
            .FromInstance(blackScreenAnimator)
            .AsTransient();

        Container.Bind<SceneLoader>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }
}