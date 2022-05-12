using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomCountText : MonoBehaviour
{
    public TextMeshProUGUI _text;
    public int _roomCount;

    // Update is called once per frame
    void Update()
    {
        _text.text = "Room：" + _roomCount.ToString();
    }
}
