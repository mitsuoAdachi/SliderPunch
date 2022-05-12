using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetector : MonoBehaviour
{
    public SliderController _sliderController;
    public CollisionDetector _colDete;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Enemy")
        _colDete._eneCon.EnemyDamage(_sliderController._panchDamage);
    }
}
