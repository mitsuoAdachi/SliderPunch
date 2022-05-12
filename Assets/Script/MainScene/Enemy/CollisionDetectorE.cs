using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionDetectorE : MonoBehaviour
{

    public EnemyController _eneCon;

    public void OnTriggerStay(Collider other)
    {
        //if(other.gameObject.tag=="Player1")
        _eneCon.OnChildTriggerStay(other);
    }
}
