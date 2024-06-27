using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private List<SpawnPoint> _spawnPoints;
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
        Enemy obj = Instantiate(_prefab);
        var movePlayer = obj.GetComponent<Movement>();

        if (movePlayer != null)
        {
            movePlayer.SetPool(_pool);
            movePlayer.SetEnemy(obj);
        }

        return obj;
    }

    private void ActionOnGet(Enemy obj)
    {
        int randomIndex = Random.Range(0, _spawnPoints.Count);
        SpawnPoint spawnPoint = _spawnPoints[randomIndex];

        obj.transform.position = spawnPoint.transform.position;
        obj.GetComponent<Renderer>().material.color = spawnPoint.ColorEnemy;
        obj.SetTarget(spawnPoint.TargetPosition.position);
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
}
