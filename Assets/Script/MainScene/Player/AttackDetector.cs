using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetector : MonoBehaviour
{
    public SliderController _sliderController;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && _sliderController._panchMode==true)
        {
            if (Input.GetKey(KeyCode.RightShift))
            {
                _sliderController.OnStopSliderButton();
            }
        }
    }
}
