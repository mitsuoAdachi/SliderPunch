
//敵の攻撃モードを制御、攻撃時の色の変化、攻撃時のSE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// private関数
/// Start() , AttackStart() , ParticleColorSwitch() , AttackModeChange()
/// ParticleColorChange(int index) , OnAttckSE();
/// 
/// public関数
/// enum AttackType
///
/// コルーチン
/// ParticleColorRetrun() , AttackSECoroutine()
///
/// アニメーションイベント
/// AttackStart()
/// 
/// </summary>

public enum AttackType
{
    None,
    Fire,
    Ice,
    Thunder
}

public class AttackSwitch : MonoBehaviour
{
    private ParticleSystem.MainModule _particle;
    private ParticleSystem.TrailModule _trail;

    [SerializeField]
    private Collider _damageCollider;

    [SerializeField]
    private ParticleSystem _parObj;
    [SerializeField]
    private Color[] _parColor;

    [SerializeField]
    private AudioClip[] _audio;

    [SerializeField]
    private float _attackTime1 = 2f, _attackTime2 = 1f;

    public AttackType _attackType = AttackType.None;

    // Start is called before the first frame update
    void Start()
    {
        _particle = _parObj.GetComponent<ParticleSystem>().main;
        _trail = _parObj.GetComponent<ParticleSystem>().trails;
    }

    /// <summary>
    /// アニメーションイベントAttackStart()
    /// </summary>
    private void AttackStart()
    {
        OnAttckSE(0);
        ParticleColorSwitch();
        StartCoroutine(ParticalColorReturn());
    }
        private void OnAttckSE(int index)
        {
           AudioSource.PlayClipAtPoint(_audio[index], transform.position);
           StartCoroutine(AttackSECoroutine());
        }
            private IEnumerator AttackSECoroutine()
            {
                yield return new WaitForSeconds(0.5f);
                AudioSource.PlayClipAtPoint(_audio[1], transform.position);

                yield return new WaitForSeconds(0.5f);
                AudioSource.PlayClipAtPoint(_audio[2], transform.position);
            }

        private void ParticleColorSwitch()
        {
            int _randomAttribute = Random.Range(0, 100);

            if (_randomAttribute < 33)
            {
                ParticalColorChange(1);
                _attackType = AttackType.Fire;

            }
            else if (_randomAttribute < 66)
            {
                ParticalColorChange(2);
                _attackType = AttackType.Ice;
            }
            else
            {
                ParticalColorChange(3);
                _attackType = AttackType.Thunder;
            }
        }
            private IEnumerator ParticalColorReturn()
            {
                yield return new WaitForSeconds(_attackTime1);
                _damageCollider.enabled = true;

                yield return new WaitForSeconds(_attackTime2);
                ParticalColorChange(0);
                _attackType = AttackType.None;
                _damageCollider.enabled = false;
            }

                private void ParticalColorChange(int index)
                {
                    _particle.startColor = _parColor[index];
                    _trail.colorOverTrail = _parColor[index];
                }

}
