using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _timeLife = 5f;
    
    private Enemy _enemy;
    private ObjectPool<Enemy> _objectPool;
    private Coroutine _coroutine;

    public void SetPool(ObjectPool<Enemy> objectPool)
    {
        _objectPool = objectPool;
    }

    public void SetEnemy(Enemy enemy)
    {
        _enemy = enemy;
    }

    private void OnEnable()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(ReturnToPoolAfterTime());
        }
    }

    private void Update()
    {
        if (_enemy != null)
        {
            MoveInDirection(_enemy.Direction);
        }
    }

    private void MoveInDirection(Vector3 direction)
    {
        transform.position += direction * _speed * Time.deltaTime;
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
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}
