using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ProjectMixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;

    public void ToggleAudio()
    {
        _mixer.GetFloat("MasterVolume", out float value);
        _mixer.SetFloat("MasterVolume", value > -80 ? -80 : 0);
    }
}
