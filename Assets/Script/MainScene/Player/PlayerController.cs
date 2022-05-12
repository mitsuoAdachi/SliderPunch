using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    NavMeshAgent _agent;
    Animator _animator;
    Vector3 _latestPos;
    public ParticleSystem _guardPS;
    public ParticleSystem _panchPS;
    public GameObject _attackSE;
    public GameObject _guardSE;
    public GameObject _canvasUI;
    public GameObject _gameOverUI;
    public Collider _damageCollider;
    public CollisionDetector _collisionDetector;

    public float _rotSpeed;
    public float _limitSpeedX;
    public float _limitSpeedY;
    public bool _playerFine = true;
    private bool _playerGuard = false;
    private bool _noise = false;

    float _lifeMax = 1;
    public float _life;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _life = _lifeMax;
    }

    // Update is called once per frame
    void Update()
    {
        var _horizontal = Input.GetAxis("Horizontal");
        var _vertical = Input.GetAxis("Vertical");

        float MoveX = Mathf.Clamp(_horizontal, -_limitSpeedX, _limitSpeedX);
        float MoveY = Mathf.Clamp(_vertical, -_limitSpeedY, _limitSpeedY);

        //Y軸に対する入力をカメラ奥へのベクトルに変換
        var correctiveMotion = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);

        //NavMeshで移動処理
        _agent.Move(correctiveMotion * new Vector3(MoveX,0,MoveY));

        //移動方向を向く
        Vector3 _diff = transform.position - _latestPos;
        _latestPos = transform.position;

        if (_diff.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_diff), Time.deltaTime * _rotSpeed);
        }

        //走るアニメーション
        if (MoveX != 0 || MoveY != 0 && _playerFine)
        {
            _animator.SetFloat("running", 0.5f);
        }
        else
        {
            _agent.isStopped = true;
            _animator.SetFloat("running", 0);

        }

        //パンチの振動
        if (_noise == true)
        {
            var _noise = GetComponent<CinemachineImpulseSource>();
            _noise.GenerateImpulse();
            StartCoroutine(OffNoiseCoroutine());
        }

        //キーボード入力
        if(Input.GetKey(KeyCode.Z))
        {
            OnRedGuard();
        }
        if (Input.GetKey(KeyCode.X))
        {
            OnBlueGuard();
        }

        if (Input.GetKey(KeyCode.C))
        {
            OnYellowGuard();
        }
    }
    private IEnumerator OffNoiseCoroutine()
    {
        yield return new WaitForSeconds(1);
        _noise = false;
        _attackSE.SetActive(false);
    }

    //被ダメージ判定
    public void Damage(int _damage)
    {
        if (_playerFine && _playerGuard==false)
        {
            _life -= _damage;
        }
        if (_life < 1)
        {
            _playerFine = false;
            OnDie();
        }
    }
    //死亡処理
    public void OnDie()
    {
        _animator.SetTrigger("death");
        _canvasUI.SetActive(false);
        _gameOverUI.SetActive(true);

    }

    //攻撃関連処理
    public void OnPanchEffect()
    {
        _attackSE.SetActive(true);
        _damageCollider.enabled = true;
        _panchPS.Play();
        _noise = true;
    }
    public void OffAttack()
    {
        _damageCollider.enabled = false;
    }

    //防御処理:敵の攻撃属性をboolで取得し対応したボタンだけ機能する
    public void OnGuard(bool _attribute)
    {
        if (_playerFine & _attribute)
        {
            _playerGuard = true;
            _animator.SetTrigger("guard");
            _guardPS.Play();
            _guardSE.SetActive(true);
            StartCoroutine(OffPlayerGuard());
        }
    }
    public void OnRedGuard()
    {
        OnGuard(_collisionDetector._attackSwitch._red);
    }
    public void OnBlueGuard()
    {
        OnGuard(_collisionDetector._attackSwitch._blue);
    }
    public void OnYellowGuard()
    {
        OnGuard(_collisionDetector._attackSwitch._yellow);
    }
    private IEnumerator OffPlayerGuard()
    {
        yield return new WaitForSeconds(1);
        _playerGuard = false;
        _guardSE.SetActive(false);
    }
}
