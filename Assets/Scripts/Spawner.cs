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
    
    private ObjectPool<GameObject> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: () => InstantiateAndSetup(),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private GameObject InstantiateAndSetup()
    {
        var obj = Instantiate(_prefab);
        var movePlayer = obj.GetComponent<MovePlayer>();

        if (movePlayer != null)
        {
            movePlayer.SetPool(_pool);
        }

        return obj;
    }

    private void ActionOnGet(GameObject obj)
    {
        int randomIndex = Random.Range(0, _spawnPoints.Count);
        obj.transform.position = _spawnPoints[randomIndex].transform.position;
        obj.transform.eulerAngles = SetRandomRotation();
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

    private Vector3 SetRandomRotation()
    {
        float minRange = 0f;
        float maxRange = 360f;
        float randomYRotation = Random.Range(minRange, maxRange);

        Vector3 direction = new Vector3(transform.eulerAngles.x, randomYRotation, transform.eulerAngles.z);

        return direction;
    }
}
