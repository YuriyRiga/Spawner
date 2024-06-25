using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 _direction;

    public Vector3 Direction
    {
        get { return _direction; }
        private set {}
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
