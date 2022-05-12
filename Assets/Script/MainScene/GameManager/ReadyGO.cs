using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ReadyGO : MonoBehaviour
{
    public RawImage _image;
    public float _activeTime = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        _image.DOFade(1f, 2f);

        var defaultPosition = transform.localPosition;
        transform.localPosition = new Vector2(0, -120f);
        transform.DOLocalMove(defaultPosition, 1f);

        DOVirtual.DelayedCall(_activeTime, () =>
        {
            this.gameObject.SetActive(false);
        });


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
