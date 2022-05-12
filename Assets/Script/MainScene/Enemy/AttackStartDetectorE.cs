using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStartDetectorE : MonoBehaviour
{
    Animator _animator;
    public Collider _collider;
    public EnemyController _enemy;
    public bool _attackMode=true;

    //public PlayerController _player;

    // Start is called before the first frame update
    void Start()
    {
        _animator = transform.parent.gameObject.GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player1")
        {
            _animator.SetTrigger("attack1");
            _attackMode = true;
        }
    }
    private void Update()
    {
        if (_enemy._life < 1)
        {
            _collider.enabled = false;
            _animator.ResetTrigger("attack1");
        }
        if (_attackMode == false)
        {
            MalfanctionPrevention();
            StartCoroutine(AttackModeCoroutine());
        }
    }

    public void MalfanctionPrevention()
    {
        _attackMode = false;
        _animator.ResetTrigger("attack1");
    }
    private IEnumerator AttackModeCoroutine()
    {
        yield return new WaitForSeconds(2);
        _attackMode = true;
    }

}
