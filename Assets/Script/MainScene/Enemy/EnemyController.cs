using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent _agent;
    RaycastHit[] _raycastHit=new RaycastHit[1];
    Animator _animator;
    public AttackStartDetectorE _attackStartDetectorE;
    public Collider _moveCollider;
    public Collider _damageCollider;
    public GameObject _weakNumber;
    public GameObject _damagePoint;
    public LayerMask _layer;
    public float _life;
    public float _lifeMax = 100;
    public float _lifeTdb;

    public bool _enemyFine = true;

    float _animeFloat;

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
        //ダメージによってアニメを変える
        _animator.SetFloat("damage50L", _animeFloat);
        _animator.SetFloat("damage50H", _animeFloat);

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
            var _posDiff = other.transform.position - transform.position;
            var _distance = _posDiff.magnitude;
            var _direction = _posDiff.normalized;

            var hitCount =Physics.RaycastNonAlloc(transform.position, _direction, _raycastHit, _distance,_layer);
            Debug.Log("HitCount"+hitCount);
            if(hitCount==1 || hitCount==0)
            {
                _agent.isStopped = false;
                _agent.destination = other.transform.position;
                _animator.SetFloat("walk", _agent.velocity.magnitude);
            }
            else
            {
                _agent.isStopped = true;
            }

        }
    }
}
