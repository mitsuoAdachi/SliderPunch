using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Titlestart : MonoBehaviour
{
    public RawImage _backGround;
    public RawImage _character;
    public RawImage _titleName;
    public GameObject _startButton;
    public AudioSource _paintSE;

    //RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        var _titleNameDefaultPos = _titleName.rectTransform.position;
        _titleName.transform.localPosition = new Vector2(-300, 143.83f);
        _titleName.transform.DOMove(_titleNameDefaultPos, 2f);

        var _characterDefaultPos = _character.rectTransform.position;
        _character.transform.localPosition = new Vector2(800, -200);
        _character.transform.DOMove(_characterDefaultPos, 2f);

        StartCoroutine(OnPaintCoroutine());
    }
    private IEnumerator OnPaintCoroutine()
    {
        yield return new WaitForSeconds(2.5f);
        _backGround.DOFade(1, 0.3f);
        _paintSE.Play();
        _startButton.SetActive(true);
    }

}
