using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DamagePoint : MonoBehaviour
{
    GameObject _parentObject;
    TextMeshProUGUI _text;
    public SliderController _sliderController;
    public float _jumpPower = 0.01f;
    public float _jumpHigh = 2f;
    public float _activeTime = 1.5f;
    public float _damageHigh=1;

    // Start is called before the first frame update
    void OnEnable()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = _sliderController._panchDamage.ToString();

        //this.transform.DOScale(new Vector2(1.5f,1.5f),1);

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
        transform.rotation = Camera.main.transform.rotation;
    }
}
