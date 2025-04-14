using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private EventHandler eventHandler;
    [SerializeField] private Animator blackScreenAnimator;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource walkAudioSource;
    [SerializeField] private TMP_Text volumeLabel;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Text qualityLabel;
    [SerializeField] private AudioMixerGroup audioMixer;
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

        Container.BindInterfacesAndSelfTo<MainMenuController>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        Container.Bind<Button>()
            .WithId("LoadGameButton")
            .FromInstance(loadGameButton)
            .AsTransient();

        Container.Bind<AudioSource>()
            .WithId("AudioSource")
            .FromInstance(audioSource)
            .AsTransient();

        Container.Bind<AudioSource>()
            .WithId("WalkAudioSource")
            .FromInstance(walkAudioSource)
            .AsTransient();

        Container.Bind<SoundController>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        Container.Bind<TMP_Text>()
            .WithId("VolumeLabel")
            .FromInstance(volumeLabel)
            .AsTransient();

        Container.Bind<Slider>()
            .WithId("VolumeSlider")
            .FromInstance(volumeSlider)
            .AsTransient();

        Container.Bind<TMP_Text>()
            .WithId("QualityLabel")
            .FromInstance(qualityLabel)
            .AsTransient();

        Container.Bind<AudioMixerGroup>()
            .FromInstance(audioMixer)
            .AsSingle();

        Container.BindInterfacesAndSelfTo<SettingsController>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }
}