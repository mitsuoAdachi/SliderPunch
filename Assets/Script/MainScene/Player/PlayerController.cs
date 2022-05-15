using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;
    private Vector3 _latestPos;

    [Header("攻撃・防御エフェクト")]
    [SerializeField]
    private ParticleSystem _guardPS, _panchPS;

    [Header("音響")]
    [SerializeField]
    private GameObject[] _audio;

    [Header("プレイヤー体力")]
    [SerializeField]
    private float _life;
    private float _lifeMax = 1;

    [Header("移動・回転速度")]
    [SerializeField]
    private float _rotSpeed, _limitSpeedX, _limitSpeedY;

    [SerializeField]
    private GameObject _canvasUI, _gameOverUI;

    [SerializeField]
    private Collider _damageCollider;

    [SerializeField]
    private CollisionDetector _collisionDetector;

    private bool _playerGuard = false;
    private bool _noise = false;

    public bool _playerFine = true;

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

        //攻撃時にカメラを振動させる
        if (_noise == true)
        {
            var _noise = GetComponent<CinemachineImpulseSource>();
            _noise.GenerateImpulse();
            StartCoroutine(OffNoiseCoroutine());
        }

        //キーボード入力(攻撃ボタンはAttackDetectorで処理)
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
    }

    //被ダメージ判定
    public void Damage(int _damage)
    {
        if (_playerFine && _playerGuard==false)　//ガード中はダメージを受けない
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
        _audio[0].SetActive(true);
        _damageCollider.enabled = true;
        _panchPS.Play();
        _noise = true;
        StartCoroutine(OffAttack());
    }
    private IEnumerator OffAttack()
    {
        yield return new WaitForSeconds(0.2f);
        _damageCollider.enabled = false;

        yield return new WaitForSeconds(1f);
        _audio[0].SetActive(false);
    }

    //防御処理:プレイヤーが生きていて、対応した敵攻撃属性がtrueならガードする
    public void OnGuard(bool _attribute)
    {
        if (_playerFine & _attribute)
        {
            _playerGuard = true;
            _animator.SetTrigger("guard");
            _guardPS.Play();
            _audio[1].SetActive(true);
            StartCoroutine(OffPlayerGuard());
        }
    }

    /// <summary>
    /// 敵の攻撃タイプをAttackSwitchクラスより取得し、対応した各ボタンへ紐付けする
    /// </summary>
    public void OnRedGuard()
    {
        OnGuard(_collisionDetector._attackSwitch._attackType==AttackType.Fire);
    }
    public void OnBlueGuard()
    {
        OnGuard(_collisionDetector._attackSwitch._attackType == AttackType.Ice);
    }
    public void OnYellowGuard()
    {
        OnGuard(_collisionDetector._attackSwitch._attackType == AttackType.Thunder);
    }
    private IEnumerator OffPlayerGuard()
    {
        yield return new WaitForSeconds(1);
        _playerGuard = false;
        _audio[1].SetActive(false);
    }
}
