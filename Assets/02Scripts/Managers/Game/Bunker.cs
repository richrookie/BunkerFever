using UnityEngine;

public class Bunker : MonoBehaviour
{
    private GameObject[] _bunkers = null;
    private Block _block = null;

    public Block block
    {
        get { return _block; }
    }

    public void Init(Block block)
    {
        _block = block;

        _bunkers = new GameObject[this.transform.childCount];
        for (int i = 0; i < _bunkers.Length; i++)
        {
            _bunkers[i] = this.transform.GetChild(i).gameObject;
        }

        this.gameObject.SetActive(false);
    }

    public void BuyBunker()
    {
        this.gameObject.SetActive(true);

        _bunkers[0].SetActive(true);
    }

    public void Merge(int levelUp)
    {
        _bunkers[levelUp - 1].SetActive(false);
        _bunkers[levelUp].SetActive(true);
    }

    public void Moved(int level)
    {
        this.gameObject.SetActive(true);
        _bunkers[level].SetActive(true);
    }

    public void Clear()
    {
        for (int i = 0; i < _bunkers.Length; i++)
        {
            _bunkers[i].SetActive(false);
        }

        this.transform.localPosition = new Vector3(0f, .15f, 0f);
        this.gameObject.SetActive(false);
    }
}
