using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCount : MonoBehaviour
{
    public RoomCountText _roomCountText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1")
        {
            _roomCountText._roomCount += 1;
            Destroy(this.gameObject);
        }
    }
}
