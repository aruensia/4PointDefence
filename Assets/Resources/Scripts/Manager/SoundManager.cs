using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject optionManager;

    public float sfxVolume;
    public float bgmVolume;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = optionManager.GetComponent<OptionManager>().masterVolumeData / 100f;
        sfxVolume = optionManager.GetComponent<OptionManager>().sfxVolumeData / 100f;
        bgmVolume = optionManager.GetComponent<OptionManager>().bgmVolumeData / 100f;
    }
}
