using System;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    public bool IsAlive { get; private set; } = true;

    Vector3 _dir;
    float _speed;
    float _maxDist;
    LayerMask _hitMask;
    float _traveled;
    float _radius;
    Action<GameObject> _onHit;

    public void Initialize(Vector3 dir, float speed, float maxDist, LayerMask hitMask, Action<GameObject> OnHit, float radius)
    {
        _dir = dir.normalized;
        _speed = speed;
        _maxDist = maxDist;
        _hitMask = hitMask;
        _onHit = OnHit;
        _radius = Mathf.Max(0.01f, radius);
        transform.localScale *= radius;
        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    void Update()
    {
        if (!IsAlive) return;

        float step = _speed * Time.deltaTime;

        transform.position += _dir * step;
        _traveled += step;

        if (_traveled >= _maxDist)
            Die();
    }
     
    void OnTriggerEnter(Collider other)
    {
        if (!IsAlive) return;
        if ((_hitMask.value & (1 << other.gameObject.layer)) != 0 )
        {
            _onHit?.Invoke(other.gameObject);
            Die();
        }
    }

    void Die()
    {
        IsAlive = false;
        Destroy(gameObject);
    }
}