using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    private Animator anim;

    public AudioMixer mixer;

    public Slider mastSlider, musicSlider, sfxSlider;

    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(LateStart(0.01f));
    }

    public void PlayGame()
    {
        anim.SetTrigger("Tutorial");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        float vol = 0f;
        mixer.GetFloat("MasterVol", out vol);
        mastSlider.value = vol;
        mixer.GetFloat("MusicVol", out vol);
        musicSlider.value = vol;
        mixer.GetFloat("SFXVol", out vol);
        sfxSlider.value = vol;
    }

    public void SetMasterVol()
    {
        mixer.SetFloat("MasterVol", mastSlider.value);
        if (mastSlider.value == -30)
        {
            mixer.SetFloat("MasterVol", -80);
        }
        PlayerPrefs.SetFloat("MasterVol", mastSlider.value);
    }

    public void SetMusicVol()
    {
        mixer.SetFloat("MusicVol", musicSlider.value);
        if (musicSlider.value == -30)
        {
            mixer.SetFloat("MusicVol", -80);
        }
        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
    }

    public void SetSFXVol()
    {
        mixer.SetFloat("SFXVol", sfxSlider.value);
        if (sfxSlider.value == -30)
        {
            mixer.SetFloat("SFXVol", -80);
        }
        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
    }
}
