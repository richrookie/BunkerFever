using System.Collections;
using UnityEngine;

public class UserController : MonoBehaviour
{
    private LayerMask _layerBunker = 1 << 6;
    private LayerMask _layerBlock = 1 << 7;
    private GameObject _dragBunker = null;
    private Block _dragBlock = null;
    private Block _dropBlock = null;
    private Camera _cam = null;


    public void Init()
    {
        _layerBunker = 1 << 6;
        _layerBlock = 1 << 7;
        _cam = Camera.main;

        StartCoroutine(CorBunkerShoot());
    }

    private void FixedUpdate()
    {
        if (Managers.Game.GameStatePlay)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerBunker))
                {
                    _dragBunker = hit.transform.gameObject;
                    _dragBlock = hit.transform.GetComponent<Bunker>().block;
                }

                Shoot();
            }
            else if (Input.GetMouseButton(0))
            {
                MoveBunker();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                DropBunker();
            }
        }
    }


    private IEnumerator CorBunkerShoot()
    {
        yield return Util.WaitGet(2f);

        while (true)
        {
            Shoot();

            yield return Util.WaitGet(2f);
        }
    }

    private void MoveBunker()
    {
        if (_dragBunker != null)
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Vector3 movePos = Vector3.zero;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                movePos = hit.point;

            movePos.y = .6f;

            _dragBunker.transform.position = movePos;
        }
    }

    private void DropBunker()
    {
        if (_dragBunker != null)
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerBlock))
            {
                _dropBlock = hit.transform.GetComponent<Block>();

                if (_dropBlock.Active)
                {// block에 bunker가 있을 경우
                    // === Check Merge === //
                    _dropBlock.CheckMerge(_dragBlock);
                    // === Check Merge === //

                    Managers.Game.blockManager.SetDamage();
                }
                else
                {// block에 bunker가 없을 경우
                    // === Move Bunker Slot === //
                    _dropBlock.Moved(_dragBlock);
                    // === Move Bunker Slot === //
                }
            }
            else
            {
                _dragBlock.Clear();
            }
        }

        _dragBunker = null;
        _dropBlock = null;
    }

    private void Shoot()
    {
        if (Managers.Game.blockManager.DmgSum > 0)
        {
            Managers.Game.monsterManager.Damaged(Managers.Game.blockManager.DmgSum);
            Managers.Game.blockManager.Shoot();
        }
    }
}
