using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    private Monster _monster = null;
    public Monster monster
    {
        get { return _monster; }
    }

    public void Init()
    {
        SpawnMonster();
    }

    public void SpawnMonster()
    {
        if (_monster != null)
        {
            Managers.Data.MonsterNum += 1;

            Destroy(_monster.gameObject);
            _monster = null;
        }

        GameObject monster = Managers.Resource.Instantiate($"Monster{Managers.Data.MonsterNum.ToString("D3")}", this.transform);
        _monster = monster.GetComponent<Monster>();
    }

    public void Damaged()
    {
        if (_monster != null)
        {
            _monster.Damaged();
        }
        else
        {
            _monster = FindObjectOfType<Monster>() as Monster;
        }
    }
}
