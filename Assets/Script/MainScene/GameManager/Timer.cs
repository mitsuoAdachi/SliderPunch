using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI _text;
    public GameObject _gameOverUI;
    public float _timer=90.00f;
    public float _stop = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime*_stop;
        _text.text = _timer.ToString("F2");
        if (_timer < 0.1f)
        {
            _timer = 0;
            _text.text = 0.00f.ToString("F2");
            _gameOverUI.SetActive(true);
            StartCoroutine(GameOverCoroutine());
        }
    }
    private IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("TitleScene");
    }
}
