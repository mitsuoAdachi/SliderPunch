using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DamagePoint : MonoBehaviour
{
    private GameObject _parentObject;
    private TextMeshProUGUI _text;

    [SerializeField]
    private SliderController _sliderController;

    [SerializeField]
    private float _jumpPower = 0.01f;

    [SerializeField]
    private float _jumpHigh = 2f;

    [SerializeField]
    private float _activeTime = 1.5f;

    [SerializeField]
    private float _damageHigh=1;

    void OnEnable()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = _sliderController._panchDamage.ToString();

        //DOJump　開始座標(parentPos)から、メインカメラ位置(cameraPos)に向かってMoveTowardsでダメージを表記
        _parentObject = transform.root.gameObject;
        var parentPos = new Vector3(_parentObject.transform.position.x, _parentObject.transform.position.y+_damageHigh, _parentObject.transform.position.z);
        transform.position = parentPos;

        var cameraPos = new Vector3(Camera.main.transform.position.x, _jumpHigh, Camera.main.transform.position.z);

        var jumpPos = Vector3.MoveTowards(parentPos, cameraPos, 1f);
        transform.DOJump(jumpPos, _jumpPower, 2, 1f);

        DOVirtual.DelayedCall(_activeTime, () =>
        {
            this.gameObject.SetActive(false);
        });
    }
    private void Update()
    {
        //テキストをカメラ目線にする
        transform.rotation = Camera.main.transform.rotation;
    }
}
