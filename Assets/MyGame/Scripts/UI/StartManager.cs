using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[AddComponentMenu("LeiFeng/StartManager")]

public class StartManager : MonoBehaviour
{
    public GameObject panelMain;
    public GameObject panelOption;
    public Slider sliderMusic;
    public Slider sliderSfx;
    // Start is called before the first frame update
    void Start()
    {
        GetDataVolume();
        panelMain.SetActive(true);
        panelOption.SetActive(false);
    }
    void GetDataVolume()
    {
        sliderMusic.value = DataManager.DataMusic;
        sliderSfx.value = DataManager.DataSfx;
        AudioManager.Instance.SetMusicVolume(sliderMusic.value);
        AudioManager.Instance.SetSfxVolume(sliderSfx.value);
    }

    public void OnOptionClick()
    {
        panelMain.SetActive(false);
        panelOption.SetActive(true);
    }
    public void OnOptionClickExit()
    {
        panelMain.SetActive(true);
        panelOption.SetActive(false);
    }
    public void OnPlayClick(string level)
    {
        SceneManager.LoadScene(level);
    }
    public void SetMusicVolume(float volume)
    {
        DataManager.DataSfx = volume;
        AudioManager.Instance.SetMusicVolume(volume);
    }
    public void SetSfxVolume(float volume)
    {
        AudioManager.Instance.SetSfxVolume(volume);
    }








    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
        Application.OpenURL("http://www.unity.com/");
#else
        Application.Quit();
#endif
    }
}