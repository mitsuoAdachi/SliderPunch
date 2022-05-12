using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ResultTime : MonoBehaviour
{
    public TextMeshProUGUI _text;
    public Timer _timer;
    public Image _score;

    // Start is called before the first frame update
    void Start()
    {
        _text.text = _timer._timer.ToString("F2");
        _text.DOFade(1, 7f);
        _score.DOFade(1, 7f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
