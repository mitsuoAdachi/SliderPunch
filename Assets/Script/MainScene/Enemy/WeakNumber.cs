using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeakNumber : MonoBehaviour
{
    public int _weakNumber;
    TextMeshProUGUI _text;
    public bool _confirm;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_confirm == false)
        {
            _weakNumber = Random.Range(1, 100);
            _text.text = _weakNumber.ToString();
        }

        transform.rotation = Camera.main.transform.rotation;
    }
}
