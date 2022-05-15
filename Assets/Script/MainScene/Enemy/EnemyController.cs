
//敵の移動(RayCastNonAlloc,NavMeshAgentで制御)、体力、被ダメ、死亡処理、それらに付随するモーションを制御

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private RaycastHit[] _raycastHit=new RaycastHit[1];

    private Animator _animator;
    private float _animeFloat;

    [SerializeField]
    private AttackStartDetectorE _attackStartDetectorE;

    [SerializeField]
    private Collider _moveCollider;

    [SerializeField]
    private Collider _damageCollider;

    [SerializeField]
    private GameObject _weakNumber;

    [SerializeField]
    private GameObject _damagePoint;

    [SerializeField]
    private LayerMask _layer;

    public float _life;
    public float _lifeMax = 100;
    public float _lifeTdb;

    public bool _enemyFine = true;

    // Start is called before the first frame update
    void Start()
    {
        _life = _lifeMax;
        _lifeTdb = _lifeMax;

        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //敵被ダメージ量によってアニメを変える
        _animator.SetFloat("damage50L", _animeFloat);
        _animator.SetFloat("damage50H", _animeFloat);

        //敵ライフが変動した時(プレイヤーの攻撃を受けた時)攻撃モーションをリセット
        if(_life != _lifeTdb)
        {
            _animator.ResetTrigger("attack1");
            _lifeTdb = _life;
        }
    }

    public void EnemyDamage(float _damage)
    {
        _life -= _damage;
        _damagePoint.SetActive(true);

        if (_life < 1)
        {
            OnDieEnemy();
            _enemyFine = false;
        }
        //ダメージ値を被ダメ時のアニメパラメーターのfloatに代入
        _animeFloat = _damage;

        StartCoroutine(AnimeFloatResetCoroutine());
    }
    private IEnumerator AnimeFloatResetCoroutine()
    {
        yield return new WaitForSeconds(1);
        _animeFloat = 0;
    }

    private void OnDieEnemy()
    {
        _animator.SetTrigger("death");
        _moveCollider.enabled = false;
        _agent.isStopped = true;
        _weakNumber.SetActive(false);
        StartCoroutine(DestroyCoroutine());    
    }
    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(3.5f);
        Destroy(this.gameObject);
    }

    public void OnChildTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player1")
        {
            Debug.Log("プレイヤー発見");
            var _posDiff = other.transform.position - transform.position;　//自身と接触コライダーまでの座標距離
            var _distance = _posDiff.magnitude;　//自身と接触コライダーまでの距離を直線距離にする
            var _direction = _posDiff.normalized;　//自身から見た接触コライダーの方向

            var hitCount =Physics.RaycastNonAlloc(transform.position, _direction, _raycastHit, _distance,_layer);
            Debug.Log("HitCount"+hitCount);
            if(hitCount==1 || hitCount==0)
            {
                _agent.isStopped = false;
                _agent.destination = other.transform.position;　//自動追尾:destination
                _animator.SetFloat("walk", _agent.velocity.magnitude);
            }
            else
            {
                _agent.isStopped = true;
            }

        }
    }
}
