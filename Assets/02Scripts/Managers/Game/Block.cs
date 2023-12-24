using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour
{
    private int _slotNum = -1;
    private bool _active = false;
    private int _level = 1;
    private int _damage = 1;

    private GameObject _bunkerObj = null;
    private Bunker _bunker = null;

    public int Level
    {
        get { return _level; }
    }
    public bool Active
    {
        get { return _active; }
    }


    public void Init(int slotNum, bool active, int level)
    {
        _slotNum = slotNum;
        _active = active;
        _level = level;
        _damage = (int)Mathf.Pow(2, _level);

        _bunkerObj = Managers.Resource.Instantiate("Bunker", this.transform);
        _bunkerObj.transform.localPosition = new Vector3(0f, .15f, 0f);
        _bunkerObj.transform.localRotation = Quaternion.identity;
        _bunkerObj.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

        _bunker = Util.GetOrAddComponent<Bunker>(_bunkerObj);
        _bunker.Init(this.GetComponent<Block>());
    }

    public void Shoot()
    {
        if (_active)
        {
            _bunker.transform.localPosition = new Vector3(0, .15f, 0);

            GameObject effect = Managers.Resource.Instantiate("Effect_BunkerShoot", this.transform);
            effect.transform.localPosition = new Vector3(0, 1f, 1.4f);
            effect.GetComponent<Poolable>().Timer(1.5f);

            StartCoroutine(CorPressTank());
        }
    }

    private IEnumerator CorPressTank()
    {
        // Lower the object
        while (this.transform.position.y >= -.3f)
        {
            Vector3 newPos = this.transform.position;
            newPos.y -= .15f;
            this.transform.position = newPos;

            yield return null;
        }

        // Raise the object
        while (transform.position.y <= .15f)
        {
            Vector3 newPos = this.transform.position;
            newPos.y += .15f;
            this.transform.position = newPos;
            yield return null;
        }
    }


    public void BuyBunker()
    {
        _active = true;

        _bunker.BuyBunker();
    }


    public bool CheckMerge(Block block)
    {
        if (_level >= 11)
            return false;

        if (_level == block._level)
        {
            GameObject effect = Managers.Resource.Instantiate("Effect_Merge", this.transform);
            effect.transform.localPosition = new Vector3(0, .5f, 0);
            effect.GetComponent<Poolable>().Timer(1.5f);

            _level += 1;
            _bunker.Merge(_level);
            block.Clear();

            return true;
        }

        block.ResetPos();

        return false;
    }

    public void Moved(Block block)
    {
        GameObject effect = Managers.Resource.Instantiate("Effect_Moved", this.transform);
        effect.transform.localPosition = new Vector3(0, .5f, 0);
        effect.GetComponent<Poolable>().Timer(1.5f);

        _active = true;
        _level = block.Level;

        _bunker.Moved(_level);
        block.Clear();
    }

    private void ResetPos()
    {
        _bunker.transform.localPosition = new Vector3(0f, .15f, 0f);
    }

    public void Clear()
    {
        _active = false;
        _level = 0;
        _damage = (int)Mathf.Pow(2, _level);

        _bunker.Clear();
    }
}
