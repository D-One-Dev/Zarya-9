using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

public class SystemInstaller : MonoInstaller
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
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private string[] day1Triggers;
    [SerializeField] private string[] day2Triggers;
    [SerializeField] private string[] day3Triggers;
    [SerializeField] private string[] day4Triggers;
    [SerializeField] private string[] day5Triggers;
    [SerializeField] private string[] day6Triggers;
    [SerializeField] private string[] day7Triggers;
    public override void InstallBindings()
    {
        Container.Bind<EventHandler>()
        .FromInstance(eventHandler)
        .AsSingle()
        .NonLazy();

        Container.Bind<Controls>()
            .FromNew()
            .AsTransient();

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

        Container.Bind<GameObject>()
            .WithId("PauseScreen")
            .FromInstance(pauseScreen)
            .AsTransient();

        Container.Bind<GameObject>()
            .WithId("SettingsScreen")
            .FromInstance(settingsScreen)
            .AsTransient();

        Container.Bind<PauseController>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        Container.Bind<Transform>()
            .WithId("PlayerTransform")
            .FromInstance(playerTransform)
            .AsTransient();

        Container.Bind<Transform>()
            .WithId("CameraTransform")
            .FromInstance(cameraTransform)
            .AsTransient();

        Container.Bind<string[]>()
            .WithId("Day1Triggers")
            .FromInstance(day1Triggers)
            .AsTransient();

        Container.Bind<string[]>()
            .WithId("Day2Triggers")
            .FromInstance(day2Triggers)
            .AsTransient();

        Container.Bind<string[]>()
            .WithId("Day3Triggers")
            .FromInstance(day3Triggers)
            .AsTransient();

        Container.Bind<string[]>()
            .WithId("Day4Triggers")
            .FromInstance(day4Triggers)
            .AsTransient();

        Container.Bind<string[]>()
            .WithId("Day5Triggers")
            .FromInstance(day5Triggers)
            .AsTransient();

        Container.Bind<string[]>()
            .WithId("Day6Triggers")
            .FromInstance(day6Triggers)
            .AsTransient();

        Container.Bind<string[]>()
            .WithId("Day7Triggers")
            .FromInstance(day7Triggers)
            .AsTransient();

        Container.BindInterfacesAndSelfTo<DayCounter>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }
}