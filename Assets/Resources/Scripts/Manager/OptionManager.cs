using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public Slider masterVolume;
    public Slider bgmVolume;
    public Slider sfxVolume;

    public TextMeshProUGUI masterVolumeText;
    public TextMeshProUGUI bgmVolumeText;
    public TextMeshProUGUI sfxVolumeText;

    int masterVolumeData;
    int bgmVolumeData;
    int sfxVolumeData;

    private void Awake()
    {
        DefaultSetting();
    }

    public void DeleteData()
    {
        PlayerPrefs.DeleteKey("masterVolume");
        PlayerPrefs.DeleteKey("bgmVolume");
        PlayerPrefs.DeleteKey("sfxVolume");
        PlayerPrefs.DeleteKey("isFirest");
    }

    public void DefaultSetting()
    {
        if (PlayerPrefs.GetInt("isFirest", 0) == 0)
        {
            var tempmasterVolume = 20;
            var tempbgmVolume = 20;
            var tempsfxVolume = 20;

            PlayerPrefs.SetInt("masterVolume", tempmasterVolume);
            PlayerPrefs.SetInt("bgmVolume", tempbgmVolume);
            PlayerPrefs.SetInt("sfxVolume", tempsfxVolume);
            PlayerPrefs.SetInt("isFirest", 1);
            PlayerPrefs.Save();
        }
    }

    public void SaveVolumeData()
    {
        int tempmasterVolume = (int)(masterVolume.value * 100);
        var tempbgmVolume = (int)(bgmVolume.value * 100);
        var tempsfxVolume = (int)(sfxVolume.value * 100);

        PlayerPrefs.SetInt("masterVolume", tempmasterVolume);
        PlayerPrefs.SetInt("bgmVolume", tempbgmVolume);
        PlayerPrefs.SetInt("sfxVolume", tempsfxVolume);
        PlayerPrefs.Save();
    }

    public void LoadVolumeData()
    {
        masterVolumeData = PlayerPrefs.GetInt("masterVolume");
        bgmVolumeData = PlayerPrefs.GetInt("bgmVolume");
        sfxVolumeData = PlayerPrefs.GetInt("sfxVolume");

    }

    public void SetVolumeData()
    {
        masterVolumeText.text = masterVolumeData.ToString();
        bgmVolumeText.text = bgmVolumeData.ToString();
        sfxVolumeText.text = sfxVolumeData.ToString();

        masterVolume.value = (float)(masterVolumeData / 100f);
        bgmVolume.value = (float)(bgmVolumeData / 100f);
        sfxVolume.value = (float)(sfxVolumeData / 100f);

        Debug.Log("로드 마스터값 : " + masterVolumeData + "!@!");
        Debug.Log("테스트 값 : " + masterVolume.value + "!@!");
    }

    public void ChangeVolume()
    {
        masterVolumeText.text = masterVolume.value.ToString();
        bgmVolumeText.text = bgmVolume.value.ToString();
        sfxVolumeText.text = sfxVolume.value.ToString();
    }


}
