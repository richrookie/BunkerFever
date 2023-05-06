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

        GetImage(Images.HpBar).fillAmount = 1f;
    }

    public void SetHpBar(float dmg)
    {
        if (GetImage(Images.HpBar).fillAmount - dmg <= 0)
            Managers.Game.monsterManager.SpawnMonster();

        GetImage(Images.HpBar).fillAmount -= dmg;
    }
}
