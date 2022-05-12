using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    Animator _playerAnimator;
    public Timer _timer;
    public CollisionDetector _colDete;
    public GameObject _punchButton;
    public GameObject _reloadButton;
    public GameObject _timePlus;

    public Slider[] _slider = new Slider[3];
    public bool[] _upSlide = new bool[3];
    public bool _panchMode=true;
    bool _sliderMode = true;

    public float _speed01;
    public float _speed2;
    public float _panchDamage;
    public float _plusTime;

    float _slider0diff;
    float _slider1diff;
    float _slider2diff;

    // Start is called before the first frame update
    void Start()
    {
        _playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_sliderMode == true)
        {
            _speed01 = 50f;
            _speed2 = 30f;
            RollSlider();

        if (_panchMode == true)
            _punchButton.SetActive(true);
            _reloadButton.SetActive(false);
        }
    }

    public void OnStopSliderButton()
    {
        _panchMode = false;
        _sliderMode = false;

        _speed01 = 0;
        _speed2 = 0;
        _punchButton.SetActive(false);
        _reloadButton.SetActive(true);

        StartCoroutine(PanchModeCoroutine());

        //一番上スライダーと敵弱点数との差値
        if (_colDete._weakNumber._weakNumber < _slider[0].value)
        {
                _slider0diff = _slider[0].value - _colDete._weakNumber._weakNumber;
        }
        else
        {
            _slider0diff = _colDete._weakNumber._weakNumber - _slider[0].value;
        }

        //????????????????????
        if (_colDete._weakNumber._weakNumber < _slider[1].value)
        {
            _slider1diff = _slider[1].value - _colDete._weakNumber._weakNumber;
        }
        else
        {
            _slider1diff = _colDete._weakNumber._weakNumber - _slider[1].value;
        }

        //?????????????????????
        if (_colDete._weakNumber._weakNumber < _slider[2].value)
        {
            _slider2diff = _slider[2].value - _colDete._weakNumber._weakNumber;
        }
        else
        {
            _slider2diff = _colDete._weakNumber._weakNumber - _slider[2].value;
        }

        _panchDamage = 100 - _slider0diff - _slider1diff - _slider2diff;
        //Mathf.Clamp(_panchDamage, 0, 100);
        if (_panchDamage < 0)
            _panchDamage = 0;

        _plusTime = _panchDamage - 40;
        if (_plusTime > 0)
        {
            _timer._timer += _plusTime;
            _timePlus.SetActive(true);
        }

        //Debug.Log("PanchDamage"+_panchDamage);
        _playerAnimator.SetTrigger("attack1");
    }
    private IEnumerator PanchModeCoroutine()
    {
        yield return new WaitForSeconds(3);
        _sliderMode = true;
        yield return new WaitForSeconds(2);
        _panchMode = true;
    }

    public void RollSlider()
    {
        for (int i = 0; i < _slider.Length; i++)
            if (_slider[i].value == 100 || _upSlide[i] == false)
            {
                _upSlide[i] = false;
                _slider[i].value -= _speed01 * Time.deltaTime;
                _slider[2].value -= _speed2 * Time.deltaTime;
            }

        for (int i = 0; i < _slider.Length; i++)
            if (_slider[i].value == 0 || _upSlide[i] == true)
            {
                _upSlide[i] = true;
                _slider[i].value += _speed01 * Time.deltaTime;
                _slider[2].value += _speed2 * Time.deltaTime;
            }
    }
}
