﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundMusicButton : MonoBehaviour {
    public Button musicOn;
    public Button musicOff;
    public Button sfxOn;
    public Button sfxOff;

    public AudioSource sfxSrc;
    public AudioSource musicSrc;

    // Use this for initialization
    void Start ()
    {
        musicOn.onClick.AddListener(ToggleMusic);
        musicOff.onClick.AddListener(ToggleMusic);

        sfxOn.onClick.AddListener(ToggleSfx);
        sfxOff.onClick.AddListener(ToggleSfx);
        
        if (SaveManager.Instance.muteBGM)
        {
            MusicManager.muteMusic = false;
            ToggleMusic();
        }
        else if (!SaveManager.Instance.muteBGM)
        {
            MusicManager.muteMusic = true;
            ToggleMusic();


            switch (musicOn.gameObject.scene.name)
            {
                case ("Fight scene"):
                case ("Fight scene 1"):
                case ("Fight scene 2"):
                    MusicManager.PlayBGM("BGM2");
                    break;
                default:
                    MusicManager.PlayBGM("BGM1");
                    break;
            }
        }

        if (SaveManager.Instance.muteSfx)
        {
            SfxManager.muteSfx = false;
            ToggleSfx();
        }
        else if (!SaveManager.Instance.muteSfx)
        {
            SfxManager.muteSfx = true;
            ToggleSfx();
        }
    }

    private void Update()
    {
        if ((!SaveManager.Instance.muteBGM && MusicManager.muteMusic) || (SaveManager.Instance.muteBGM && !MusicManager.muteMusic))// supposed to play
        {
            ToggleMusic();
        }
        if ((!SaveManager.Instance.muteSfx && SfxManager.muteSfx) || (SaveManager.Instance.muteSfx && !SfxManager.muteSfx)) // supposed to play
        {
            ToggleSfx();
        }
    }

    public void ToggleMusic()
    {
        MusicManager.muteMusic = !MusicManager.muteMusic;
        if (MusicManager.muteMusic)
        {
            musicSrc.Stop();
            musicSrc.volume = 0f;
            musicSrc.mute = true;
            SaveManager.Instance.muteBGM = true;

            musicOff.gameObject.SetActive(true);
            musicOn.gameObject.SetActive(false);
        }
        else
        {
            musicSrc.mute = false;
            SaveManager.Instance.muteBGM = false;
            musicSrc.volume = 0.5f;

            switch (gameObject.scene.name)
            {
                case ("Fight scene"):
                case ("Fight scene 1"):
                case ("Fight scene 2"):
                    MusicManager.PlayBGM("BGM2");
                    break;
                default:
                    MusicManager.PlayBGM("BGM1");
                    break;
            }

            musicOn.gameObject.SetActive(true);
            musicOff.gameObject.SetActive(false);
        }
    }

    public void ToggleSfx()
    {
        SfxManager.muteSfx = !SfxManager.muteSfx;
        if (SfxManager.muteSfx)
        {
            sfxSrc.Stop();
            sfxSrc.mute = true;
            sfxSrc.volume = 0f;
            SaveManager.Instance.muteSfx = true;

            sfxOff.gameObject.SetActive(true);
            sfxOn.gameObject.SetActive(false);
        }
        else
        {
            sfxSrc.mute = false;
            sfxSrc.volume = 1f;
            SaveManager.Instance.muteSfx = false;
            sfxOn.gameObject.SetActive(true);
            sfxOff.gameObject.SetActive(false);
        }
    }
}
