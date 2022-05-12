using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController_Title : MonoBehaviour
{
    public Slider _slider;
    public float _speed;
    bool _upSlider =true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_slider.value == 100 || _upSlider == false)
        {
            _upSlider = false;
            _slider.value -= _speed * Time.deltaTime;
        }

        if (_slider.value == 0 || _upSlider == true)
        {
            _upSlider = true;
            _slider.value += _speed * Time.deltaTime;
        }

    }
}
