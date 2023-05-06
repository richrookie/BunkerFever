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

    private int _curHp = 250;
    private int _maxHp = 250;

    public float CurHpValue
    {
        get { return _curHp / (float)_maxHp; }
    }

    public void Init()
    {
        _curHp = _maxHp = 250 * Managers.Data.MonsterNum;
    }

    public void Damaged(int dmg)
    {
        if (_curHp - dmg <= 0)
            _curHp = 0;
        else
            _curHp -= dmg;

        float leftHp = _curHp / (float)_maxHp;
        Managers.Game.uiGameScene.SetHpBar(leftHp);

        if (_cols != null && _cols.Length > 0)
        {
            for (int i = 0; i < _cols.Length; i++)
            {
                _rigids[i].AddExplosionForce(power, explosionRoot.position, radius);
            }
        }
    }
}
