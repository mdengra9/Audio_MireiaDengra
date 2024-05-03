using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _volumeParameter;
    private Slider _volumeSlider;
    private Toggle _muteToggle;
    private bool muted;

    void Awake()
    {
        _volumeSlider = GetComponent<Slider>();
        _muteToggle = GetComponentInChildren<Toggle>();

        _volumeSlider.onValueChanged.AddListener(ChangeVolume);
        _muteToggle.onValueChanged.AddListener(Mute);
    }

    // Start is called before the first frame update
    void Start()
    {
        _volumeSlider.value = PlayerPrefs.GetFloat(_volumeParameter, _volumeSlider.maxValue);
        
        string muteValued = PlayerPrefs.GetString(_volumeParameter + "Mute", "False");

        if(muteValued == "False")
        {
            muted = false;
        }
        else if(muteValued == "True")
        {
            muted = true;
        }

        _muteToggle.isOn = !muted;
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat(_volumeParameter, _volumeSlider.value);
        PlayerPrefs.SetString(_volumeParameter + "Mute", muted.ToString());
    }

    void ChangeVolume(float value)
    {
        _mixer.SetFloat(_volumeParameter, Mathf.Log10(value) * 20);
    }

    void Mute(bool SoundEnabled)
    {
        if(SoundEnabled)
        {
            float lastVolume = PlayerPrefs.GetFloat(_volumeParameter, _volumeSlider.maxValue);
            _mixer.SetFloat(_volumeParameter, Mathf.Log10(lastVolume) * 20);
            muted = false;
        }
        else
        {
            PlayerPrefs.SetFloat(_volumeParameter, _volumeSlider.value);
            _mixer.SetFloat(_volumeParameter, Mathf.Log10(_volumeSlider.minValue) * 20);
            muted = true;
        }
    }
}
