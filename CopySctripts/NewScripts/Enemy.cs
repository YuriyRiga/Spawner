using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 _targetDirection;

    public Vector3 TargetDirection
    {
        get { return _targetDirection; }
        private set {}
    }

    public void SetTarget(Vector3 targetDirection)
    {
        _targetDirection = targetDirection;
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
