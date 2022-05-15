
//３つのスライダーを制御、ダメージ計算、制限時間にボーナス、プレイヤーの攻撃開始

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField]
    private Timer _timer;

    [SerializeField]
    private GameObject _punchButton;

    [SerializeField]
    private GameObject _reloadButton;

    [SerializeField]
    private GameObject _timePlus;
    [SerializeField]
    private Slider[] _slider = new Slider[3];
    [SerializeField]
    private bool[] _onSlide = new bool[3];
    [SerializeField]
    private float[] _speed = new float[3];

    private Animator _playerAnimator;

    public CollisionDetector _colDete;

    public float _panchDamage;
    public float _plusTime;

    public bool _panchMode;
    private bool _sliderMode = true;

    // Start is called before the first frame update
    void Start()
    {
        _playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //スライダーが動くモード
        if (_sliderMode == true)
        {
            _speed[0] = 80f;
            _speed[1] = 120f;
            _speed[2] = 160f;
            RollSlider();
        }
        //攻撃できる状態のモード
        if (_panchMode == true)
        { 
            _punchButton.SetActive(true);
            _reloadButton.SetActive(false);
        }
    }
    /// <summary>
    /// スライダーを止める　→　ダメージを計算する　→　プレイヤーが攻撃を開始する。
    /// </summary>
    public void OnStopSliderButton()
    {
        float[] _sliderDiff = new float[3];

        _panchMode = false;
        _sliderMode = false;

        for(int i = 0; i<_speed.Length; i++)
        _speed[i] = 0;

        _punchButton.SetActive(false);
        _reloadButton.SetActive(true);

        StartCoroutine(PanchModeCoroutine());

        //一番上スライダーと敵弱点数との差値
        if (_colDete._weakNumber._weakNumber < _slider[0].value)
        {
            _sliderDiff[0] = _slider[0].value - _colDete._weakNumber._weakNumber;
        }
        else
        {
            _sliderDiff[0] = _colDete._weakNumber._weakNumber - _slider[0].value;
        }

        //中央スライダーと敵弱点数との差値
        if (_colDete._weakNumber._weakNumber < _slider[1].value)
        {
            _sliderDiff[1] = _slider[1].value - _colDete._weakNumber._weakNumber;
        }
        else
        {
            _sliderDiff[1] = _colDete._weakNumber._weakNumber - _slider[1].value;
        }

        //一番下スライダーと敵弱点数との差値
        if (_colDete._weakNumber._weakNumber < _slider[2].value)
        {
            _sliderDiff[2] = _slider[2].value - _colDete._weakNumber._weakNumber;
        }
        else
        {
            _sliderDiff[2] = _colDete._weakNumber._weakNumber - _slider[2].value;
        }

        _panchDamage = 100 - _sliderDiff[0] - _sliderDiff[1] - _sliderDiff[2];

        if (_panchDamage < 0)
            _panchDamage = 0;

        _plusTime = _panchDamage - 40;
        if (_plusTime > 0)
        {
            _timer._timer += _plusTime;
            _timePlus.SetActive(true);
        }

        //攻撃モーションの開始
        _playerAnimator.SetTrigger("attack1");
    }
    private IEnumerator PanchModeCoroutine()
    {
        yield return new WaitForSeconds(3);
        _sliderMode = true;
        yield return new WaitForSeconds(2);
        _panchMode = true;
    }

    /// <summary>
    /// ３つのスライダーが左右に違う速度で自動で動くよう制御
    /// </summary>
    public void RollSlider()
    {
        for (int i = 0; i < _slider.Length; i++)
            if (_slider[i].value == 100 || _onSlide[i] == false)
            {
                _onSlide[i] = false;

                if (_onSlide[i] == false)
                    _slider[i].value -= _speed[i] * Time.deltaTime;
            }

        for (int i = 0; i < _slider.Length; i++)
            if (_slider[i].value == 0 || _onSlide[i] == true)
            {
                _onSlide[i] = true;

                if (_onSlide[i] == true)
                   _slider[i].value += _speed[i] * Time.deltaTime;
            }
    }
}
