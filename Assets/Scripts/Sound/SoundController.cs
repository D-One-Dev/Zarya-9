using UnityEngine;
using Zenject;

public class SoundController
{
    [Inject(Id = "AudioSource")]
    private readonly AudioSource _audioSource;
    [Inject(Id = "WalkAudioSource")]
    private readonly AudioSource _walkAudioSource;

    [Inject]
    public void Construct(EventHandler eventHandler)
    {
        eventHandler.OnPlaySound += PlaySound;
        eventHandler.OnPlaySoundWithRandomPitch += PlaySoundRandomPitch;
        eventHandler.OnStartWalking += StartWalking;
        eventHandler.OnStopWalking += StopWalking;
    }

    private void PlaySound(AudioClip sound)
    {
        _audioSource.volume = 1f;
        _audioSource.pitch = 1f;
        _audioSource.PlayOneShot(sound);
    }

    private void PlaySoundRandomPitch(AudioClip sound)
    {
        _audioSource.volume = 1f;
        _audioSource.pitch = Random.Range(.9f, 1.1f);
        _audioSource.PlayOneShot(sound);
    }

    private void StartWalking()
    {
        if (_walkAudioSource.isPlaying == false) _walkAudioSource.Play();
    }

    private void StopWalking()
    {
        if (_walkAudioSource.isPlaying) _walkAudioSource.Stop();
    }
}
