using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Transform _targetPosition;

    public Color ColorEnemy { get { return _targetPosition.GetComponent<Renderer>().material.color; } private set { } }
    public Transform TargetPosition { get { return _targetPosition; } private set { } }
}
