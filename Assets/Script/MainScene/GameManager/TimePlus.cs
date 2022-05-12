using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TimePlus : MonoBehaviour
{
    public TextMeshProUGUI _text;
    public SliderController _sliderController;
    public int _activeTime = 3;

    // Start is called before the first frame update
    void OnEnable()
    {
        _text.text = "+" + _sliderController._plusTime + "sec".ToString();
        //Debug.Log(transform.localPosition);
        _text.DOFade(1f, 2f);

        var defaultPosition = transform.localPosition;
        transform.localPosition = new Vector2(-150, -150f);
        transform.DOLocalMove(defaultPosition, 1f);

        DOVirtual.DelayedCall(_activeTime, () =>
        {
            this.gameObject.SetActive(false);
        });
    }
}
