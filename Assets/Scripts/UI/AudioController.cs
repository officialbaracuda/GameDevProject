using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioClip coin, star,activate,jump,die,kill;

    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        music.volume = StorageController.GetFloat(Constants.Music);
        sfx.volume = StorageController.GetFloat(Constants.SFX);
        musicSlider.value = StorageController.GetFloat(Constants.Music);
        sfxSlider.value = StorageController.GetFloat(Constants.SFX);
    }

    public void UpdateMusicVolume()
    {
        music.volume = musicSlider.value;
        StorageController.SaveFloat(Constants.Music, musicSlider.value);
    }

    public void UpdateSFXVolume()
    {
        sfx.volume = sfxSlider.value;
        StorageController.SaveFloat(Constants.SFX, sfxSlider.value);
    }

    public void PlaySFX(AudioClip clip)
    {
        sfx.clip = clip;
        sfx.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        music.clip = clip;
        music.Play();
    }

    public void StopMusic()
    {
        music.Stop();
    }

    public void StopSFX()
    {
        music.Stop();
    }

}
