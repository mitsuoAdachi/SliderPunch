using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControoler : MonoBehaviour
{
    public EnemyController _eneCon;
    public GameObject _gameClearUI;
    public GameObject _canvasUI;
    public Timer _timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_eneCon._life < 1)
        {
            StartCoroutine(GameClearCoroutine());
        }
    }
    private IEnumerator GameClearCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        _timer._stop = 0;
        _gameClearUI.SetActive(true);
        _canvasUI.SetActive(false);

    }
}
