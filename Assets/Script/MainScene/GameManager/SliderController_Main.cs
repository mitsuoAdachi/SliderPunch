using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderController_Main : MonoBehaviour
{
    public Slider _slider;
    public TextMeshProUGUI _text;
    public CollisionDetector _colDete;

    // Update is called once per frame
    void OnEnable()
    {
        _text.text = _colDete._weakNumber._weakNumber.ToString();
        _slider.value = _colDete._weakNumber._weakNumber;

    }
    private void Update()
    {
        if (_colDete._batleMode==false)
        {
            this.gameObject.SetActive(false);
        }
    }
}
