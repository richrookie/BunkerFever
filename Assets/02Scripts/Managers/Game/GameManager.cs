using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Define.eGameState _cureGameState = Define.eGameState.Ready;
    public bool GameStateReady => _cureGameState == Define.eGameState.Ready;
    public bool GameStatePlay => _cureGameState == Define.eGameState.Play;
    public bool GameStateEnd => _cureGameState == Define.eGameState.End;


    private UI_GameScene _uiGameScene = null;
    private BlockManager _blockManager = null;
    private MonsterManager _monsterManager = null;
    private UserController _userController = null;

    public UI_GameScene uiGameScene { get { CheckNull(); return _uiGameScene; } }
    public BlockManager blockManager { get { CheckNull(); return _blockManager; } }
    public MonsterManager monsterManager { get { CheckNull(); return _monsterManager; } }
    public UserController userController { get { CheckNull(); return _userController; } }


    public void Init()
    {
        CheckNull();

        Managers.Resource.Instantiate("Blocks");
        Managers.Resource.Instantiate("Directional Light");

        blockManager.Init();

        GameObject mgr1 = new GameObject { name = "@MonsterManager " };
        mgr1.transform.SetParent(this.transform);
        Util.GetOrAddComponent<MonsterManager>(mgr1);
        monsterManager.Init();

        GameObject mgr2 = new GameObject { name = "@UserController " };
        mgr2.transform.SetParent(this.transform);
        Util.GetOrAddComponent<UserController>(mgr2);
        userController.Init();

        _cureGameState = Define.eGameState.Play;
    }

    private void CheckNull()
    {
        if (_uiGameScene == null) _uiGameScene = FindObjectOfType<UI_GameScene>() as UI_GameScene;
        if (_blockManager == null) _blockManager = FindObjectOfType<BlockManager>() as BlockManager;
        if (_monsterManager == null) _monsterManager = FindObjectOfType<MonsterManager>() as MonsterManager;
        if (_userController == null) _userController = FindObjectOfType<UserController>() as UserController;
    }

    public void Clear()
    {

    }
}
