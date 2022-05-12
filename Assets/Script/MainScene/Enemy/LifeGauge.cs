using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGauge : MonoBehaviour
{
    EnemyController _eneCon;
    Image _fillImage;

    // Start is called before the first frame update
    void Start()
    {
        _eneCon = transform.root.gameObject.GetComponent<EnemyController>();
        _fillImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        _fillImage.fillAmount = _eneCon._life / _eneCon._lifeMax;

        transform.rotation = Camera.main.transform.rotation;
    }
}
