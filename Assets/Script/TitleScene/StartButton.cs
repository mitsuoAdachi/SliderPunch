using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public AudioSource _startSE1;
    public AudioSource _startSE2;
    public Slider _slider;

    public void GameStartButton()
    {
        if (_slider.value > 30 && _slider.value < 70)
        {
            _startSE1.Play();
            _startSE2.Play();
            StartCoroutine(LoadSceneCoroutine());
        }
    }
    private IEnumerator LoadSceneCoroutine()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainScene");
    }
}
