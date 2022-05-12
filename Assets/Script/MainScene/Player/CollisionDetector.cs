using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class CollisionDetector : MonoBehaviour
{
    public Transform _player;
    public AttackSwitch _attackSwitch;
    public EnemyController _eneCon;
    public GameObject _sliderFixVule;
    public GameObject _vCam;
    private CinemachineVirtualCamera _vCamTarget;
    public WeakNumber _weakNumber;
    public bool _batleMode = false;

    // Start is called before the first frame update
    void Start()
    {
        _vCamTarget = _vCam.GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (_batleMode == false)
        {
            _vCamTarget.LookAt = _player;
        }

        if (_eneCon!=null && _eneCon._life < 1)
        {
            _batleMode = false;
        }

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            _batleMode = true;
            //範囲に入ると敵を注視&弱点数が確定する
            _vCamTarget.LookAt=other.transform;
            _weakNumber = other.transform.Find("Canvas_Number/WeekNumber").GetComponent<WeakNumber>();
            _weakNumber._confirm=true;
            _sliderFixVule.SetActive(true);

            //範囲に入った敵の情報
            _attackSwitch = other.gameObject.GetComponent<AttackSwitch>();
            _eneCon = other.gameObject.GetComponent<EnemyController>();

        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            _vCamTarget.LookAt = _player;
            _weakNumber._confirm = false;
            _sliderFixVule.SetActive(false);
        }
    }
}
