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

    void Awake()
    {
        _volumeSlider = GetComponent<Slider>();
        _volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void ChangeVolume(float value)
    {
        _mixer.SetFloat(_volumeParameter, value);
    }
}
