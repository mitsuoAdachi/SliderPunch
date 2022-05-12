using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TouchMove : MonoBehaviour
{
    Vector2 startPos, currentPos;
    Animator _animator;
    NavMeshAgent _agent;
    PlayerController _player;

    public float _limitSpeedX = 130f;
    public float _limitSpeedZ = 120f;
    public float _runPower = 2000;
    public float _walkPower = 3000;
    public float _cameraSpeed = 1f;

    int _moveFingerId;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        foreach (var touch in Input.touches)
        {
            if (touch.position.x < 800)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    _moveFingerId = touch.fingerId;
                    startPos = touch.position;
                }
                if (touch.phase == TouchPhase.Moved)
                {
                    currentPos = touch.position;
                    _animator.SetFloat("running", 1);
                }

                if (touch.fingerId == _moveFingerId)
                {
                    //Y軸への入力を画面奥へのベクトルに変換
                    var correctiveMotion = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);

                    Vector2 _move = currentPos - startPos;
                    float _moveX = Mathf.Clamp(_move.x, -_limitSpeedX, _limitSpeedX);
                    float _moveZ = Mathf.Clamp(_move.y, -_limitSpeedZ, _limitSpeedZ);
                    Vector3 _move3 = new Vector3(_moveX, 0, _moveZ);

                    _agent.isStopped = false;

                    if (_player._playerFine == true) 
                    {
                        _agent.Move(correctiveMotion * _move3 / _runPower);
                    }

                }

                if (touch.phase == TouchPhase.Ended)
                {
                    _agent.isStopped = true;
                    _animator.SetFloat("running", 0);
                }
            }
        }
    }
}
