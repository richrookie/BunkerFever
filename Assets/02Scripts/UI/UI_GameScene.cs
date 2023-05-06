using UnityEngine.UI;
using TMPro;

public class UI_GameScene : UI_Scene
{
    enum Buttons
    {
        Button_Buy
    }

    enum TextMeshs
    {
        TMP_Buy
    }

    enum Images
    {
        HpBar
    }

    private void Awake()
    {
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(TextMeshs));
        Bind<Image>(typeof(Images));

        base.Init();

        GetButton(Buttons.Button_Buy).onClick.AddListener(() =>
        {
            // === Bunker Buy Button === //
            Managers.Game.blockManager.BuyBunker();
            // === Bunker Buy Button === //
        });

        ResetHpBar();
    }

    public void ResetHpBar()
    {
        GetImage(Images.HpBar).fillAmount = 1f;
    }

    public void SetHpBar(float leftHp)
    {
        if (leftHp <= 0)
        {
            GetImage(Images.HpBar).fillAmount = 0;
            Managers.Game.monsterManager.SpawnMonster();

            return;
        }

        GetImage(Images.HpBar).fillAmount = leftHp;
    }
}
