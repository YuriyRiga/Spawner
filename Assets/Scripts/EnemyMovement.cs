using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _timeLife = 5f;

    private Enemy _enemy;
    private ObjectPool<Enemy> _objectPool;
    private Coroutine _lifeTimerCoroutine;
    private Transform _target;

    public void SetPool(ObjectPool<Enemy> objectPool)
    {
        _objectPool = objectPool;
    }

    public void SetEnemy(Enemy enemy)
    {
        _enemy = enemy;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }

    private void OnEnable()
    {
        if (_lifeTimerCoroutine == null)
        {
            _lifeTimerCoroutine = StartCoroutine(ReturnToPoolAfterTime());
        }
    }

    private void Update()
    {
        MoveTowardsTarget();
    }

    private IEnumerator ReturnToPoolAfterTime()
    {
        yield return new WaitForSeconds(_timeLife);

        if (_objectPool != null)
        {
            _objectPool.Release(_enemy);
        }
    }

    private void OnDisable()
    {
        if (_lifeTimerCoroutine != null)
        {
            StopCoroutine(_lifeTimerCoroutine);
            _lifeTimerCoroutine = null;
        }
    }
}
