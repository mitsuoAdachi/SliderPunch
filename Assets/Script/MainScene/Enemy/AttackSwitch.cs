using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSwitch : MonoBehaviour
{
    ParticleSystem.MainModule _particle;
    ParticleSystem.TrailModule _trail;

    public Collider _collider;
    public ParticleSystem _par;
    public float _attackTime1 = 2f;
    public float _attackTime2 = 1f;
    public int _randomAttribute;

    public bool _red = false;
    public bool _blue = false;
    public bool _yellow = false;

    public bool _fire=false;
    public bool _ice=false;
    public bool _thunder=false;

    public GameObject _attackSE1;
    public GameObject _attackSE2;
    public GameObject _attackSE3;

    // Start is called before the first frame update
    void Start()
    {
        _particle = _par.GetComponent<ParticleSystem>().main;
        _trail = _par.GetComponent<ParticleSystem>().trails;


    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AttackStart()
    {
        OnAttckSE();
        ParticleColorChange();
        StartCoroutine(ParticalColorReturn());
    }

    public void AttackStart_Enemy1()
    {
        OnAttckSE();
        StartCoroutine(ParticalColorReturn());
    }

    public void ParticleColorChange()
    {
        _randomAttribute = Random.Range(0, 100);

        if (_randomAttribute < 33)
        {
            OnParticalColorChange(Color.red);
            _fire = true;
        }
        else if (_randomAttribute < 66)
        {
            OnParticalColorChange(Color.cyan);
            _ice = true;
        }
        else
        {
            OnParticalColorChange(Color.yellow);
            _thunder = true;
        }

    }

    public void AttackModeChange()
    {
        if (_fire == true)
            _red = true;

        if (_ice == true)
            _blue = true;

        if (_thunder == true)
            _yellow = true;

    }
    public void OnParticalColorChange(Color _color)
    {
        _particle.startColor = _color;
        _trail.colorOverTrail = _color;
    }
    private IEnumerator ParticalColorReturn()
    {
        yield return new WaitForSeconds(_attackTime1);
        _collider.enabled = true;

        yield return new WaitForSeconds(_attackTime2);
        OnParticalColorChange(Color.white);
        _fire = false;
        _ice = false;
        _thunder = false;
        _red = false;
        _blue = false;
        _yellow = false;

        _collider.enabled = false;
    }

    public void OnAttckSE()
    {
        _attackSE1.SetActive(true);
        StartCoroutine(AttackSECoroutine());
    }
    private IEnumerator AttackSECoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        _attackSE2.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _attackSE3.SetActive(true);
        yield return new WaitForSeconds(2);
        _attackSE1.SetActive(false);
        _attackSE2.SetActive(false);
        _attackSE3.SetActive(false);
    }
}
