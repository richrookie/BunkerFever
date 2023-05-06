using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    private Transform explosionRoot = null;
    [SerializeField]
    private Collider[] _cols = null;
    [SerializeField]
    private Rigidbody[] _rigids = null;
    private float power = 3000f;
    private float radius = 100f;

    public void Damaged()
    {
        if (_cols != null && _cols.Length > 0)
        {
            for (int i = 0; i < _cols.Length; i++)
            {
                _rigids[i].AddExplosionForce(power, explosionRoot.position, radius);
            }
        }
    }
}
