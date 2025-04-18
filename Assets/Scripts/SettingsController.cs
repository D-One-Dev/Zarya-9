using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

public class SettingsController : IInitializable
{
    [Inject(Id = "VolumeLabel")]
    private readonly TMP_Text _volumeLabel;
    [Inject(Id = "VolumeSlider")]
    [SerializeField] private Slider _volumeSlider;
    [Inject(Id = "QualityLabel")]
    [SerializeField] private TMP_Text _qualityLabel;

    private AudioMixerGroup _audioMixer;

    private int _quality;
    private int _volume;

    [Inject]
    public void Construct(AudioMixerGroup audioMixer, EventHandler eventHandler)
    {
        _audioMixer = audioMixer;

        eventHandler.OnChangeVolume += ChangeVolume;
        eventHandler.OnChangeQuality += ChangeQuality;
    }

    public void Initialize()
    {
        _quality = PlayerPrefs.GetInt("Quality", 3);
        _volume = PlayerPrefs.GetInt("Volume", 100);
        _audioMixer.audioMixer.SetFloat("Volume", Mathf.Lerp(-80f, 0f, _volume / 100f));
        Application.targetFrameRate = -1;
        QualitySettings.vSyncCount = 1;
        ApplyVolume();
        ApplyQuality();
    }

    private void ApplyVolume()
    {
        _volumeSlider.value = _volume;
        _volumeLabel.text = "Громкость: " + _volume;
        PlayerPrefs.SetInt("Volume", _volume);
    }

    private void ChangeVolume()
    {
        _volume = (int)_volumeSlider.value;
        _audioMixer.audioMixer.SetFloat("Volume", Mathf.Lerp(-80f, 0f, _volume / 100f));
        ApplyVolume();
    }

    private void ChangeQuality(bool direction)
    {
        if (direction)
        {
            if (_quality <= 2) _quality++;
            else _quality = 0;
        }
        else
        {
            if (_quality > 0) _quality--;
            else _quality = 3;
        }
        PlayerPrefs.SetInt("Quality", _quality);
        ApplyQuality();
    }

    private void ApplyQuality()
    {
        switch (_quality)
        {
            case 0:
                _qualityLabel.text = "Качество графики: минимальное";
                QualitySettings.SetQualityLevel(0);
                break;
            case 1:
                _qualityLabel.text = "Качество графики: среднее";
                QualitySettings.SetQualityLevel(2);
                break;
            case 2:
                _qualityLabel.text = "Качество графики: высокое";
                QualitySettings.SetQualityLevel(4);
                break;
            case 3:
                _qualityLabel.text = "Качество графики: ультра";
                QualitySettings.SetQualityLevel(5);
                break;
            default:
                _qualityLabel.text = "Качество графики: минимальное";
                QualitySettings.SetQualityLevel(0);
                break;
        }

        QualitySettings.vSyncCount = 1;
    }
}