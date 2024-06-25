using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _timeLife = 5f;

    private ObjectPool<GameObject> _objectPool;
    private Coroutine _coroutine;

    public void SetPool(ObjectPool<GameObject> objectPool)
    {
        _objectPool = objectPool;
    }
    private void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;

        if(_coroutine == null)
        {
            _coroutine = StartCoroutine(ReturnToPoolAfterRandomTime());
        }
    }
    private IEnumerator ReturnToPoolAfterRandomTime()
    {
        float waitTime = _timeLife;
        yield return new WaitForSeconds(waitTime);

        _coroutine = null;
        _objectPool.Release(gameObject);
    }
}
