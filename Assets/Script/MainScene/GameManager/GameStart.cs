using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameObject _tutrialPanel;
    public GameObject _readyText;
    public GameObject _startSE;
    public GameObject _canvasUI;

    // Start is called before the first frame update
    public void OffTutrialPanel()
    {
        _tutrialPanel.SetActive(false);
        StartCoroutine(GameStartCoroutine());
    }
    private IEnumerator GameStartCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        _readyText.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        _startSE.SetActive(true);
        _canvasUI.SetActive(true);
    }
}
