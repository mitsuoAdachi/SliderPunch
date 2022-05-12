using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Image _gameOverUI;
    public GameObject _gameOverSE1;
    public GameObject _gameOverSE2;
    public float _drawTime=1;
    public PlayerController _player;

    // Start is called before the first frame update
    void Start()
    {
        _gameOverUI.DOFade(1, _drawTime);
        _gameOverSE1.SetActive(true);

        if (_player._playerFine == false)
        {
            StartCoroutine(OverCoroutine());
        }
        else
        {
            StartCoroutine(ClearCoroutine());
        }

    }

    private IEnumerator OverCoroutine()
    {
        yield return new WaitForSeconds(2);
        _gameOverSE2.SetActive(true);

        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("TitleScene");
    }
    private IEnumerator ClearCoroutine()
    {
        yield return new WaitForSeconds(2);
        _gameOverSE2.SetActive(true);

        yield return new WaitForSeconds(9);
        SceneManager.LoadScene("TitleScene");
    }

}
