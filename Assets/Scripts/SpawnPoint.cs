using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Transform _target;

    public Color ColorEnemy => _target.GetComponent<Renderer>().material.color;
    public Transform Target => _target;
}