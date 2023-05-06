using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    private List<Block> _blockList = new List<Block>();
    private System.Random _rand = null;
    private bool _slotLeft = true;

    public bool SlotLeft
    {
        get { return _slotLeft; }
    }

    public void Init()
    {
        Transform[] childObj = this.transform.GetComponentsInChildren<Transform>();
        for (int i = 0; i < childObj.Length; i++)
        {
            if (childObj[i].name == "FLOOR")
            {
                Block block = Util.GetOrAddComponent<Block>(childObj[i].gameObject);
                _blockList.Add(block);
                block.Init(_blockList.Count - 1, false, 0);
            }
        }

        _rand = new System.Random();
    }


    public void Shoot()
    {
        foreach (Block block in _blockList)
        {
            block.Shoot();
        }
    }


    #region Buy Bunker
    /// <summary>
    /// Buy Button 누를 시 호출되는 함수
    /// </summary>
    public void BuyBunker()
    {
        Block block = ReturnBunkerSlot();

        block?.BuyBunker();
    }

    private Block ReturnBunkerSlot()
    {
        if (!CheckBunkerSlot())
            return null;

        int slotNum = 0;

        while (true)
        {
            slotNum = _rand.Next(0, _blockList.Count);
            if (!_blockList[slotNum].Active)
                break;
        }

        return _blockList[slotNum];
    }

    private bool CheckBunkerSlot()
    {
        _slotLeft = false;

        foreach (var bunker in _blockList)
        {
            if (!bunker.Active)
            {
                _slotLeft = true;
                break;
            }
        }

        return _slotLeft;
    }
    #endregion
}