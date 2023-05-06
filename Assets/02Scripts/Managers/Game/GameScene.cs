using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        Managers.UI.ShowSceneUI<UI_GameScene>();
        // Managers.Resource.Instantiate("@Assets");
    }

    public override void Clear()
    {
    }
}
