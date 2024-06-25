using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private List<GameObject> _spawnPoints;
    [SerializeField] private float _repeatRate = 2f;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => InstantiateAndSetup(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private Enemy InstantiateAndSetup()
    {
        Enemy obj = Instantiate(_prefab).GetComponent<Enemy>();
        var movePlayer = obj.GetComponent<MovePlayer>();

        if (movePlayer != null)
        {
            movePlayer.SetPool(_pool);
            movePlayer.SetEnemy(obj);
            Vector3 direction = GetRandomDirection();
            obj.SetDirection(direction);
        }

        return obj;
    }

    private void ActionOnGet(Enemy obj)
    {
        int randomIndex = Random.Range(0, _spawnPoints.Count);
        obj.transform.position = _spawnPoints[randomIndex].transform.position;
        obj.SetActive(true);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetPlayer), 0.0f, _repeatRate);
    }

    private void GetPlayer()
    {
        _pool.Get();
    }

    private Vector3 GetRandomDirection()
    {
        return new Vector3(Random.value, 0, Random.value).normalized; ;
    }
}
