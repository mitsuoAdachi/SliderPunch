using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetectorE : MonoBehaviour
{
    public EnemyController _eneCon;

    private void OnTriggerEnter(Collider other)
    {
        var _player=other.GetComponent<PlayerController>();
        if(_player != null && _eneCon._enemyFine)
        _player.Damage(100);
    }
}
