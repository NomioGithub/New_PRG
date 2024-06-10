using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUnit : BattleUnit
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] Text hpText;
    [SerializeField] Text atText;

    public override void Setup(Battler battler)
    {
        base.Setup(battler);
        //player：ステータスの設定
        nameText.text = battler.Base.Name;
        levelText.text = $"Level:{battler.Level}";
        hpText.text = $"HP:{battler.HP}/{battler.MaxHP}";
        atText.text = $"AT:{battler.AT}";

    }

    public override void UpdateUI()
    {
        levelText.text = $"Level:{Battler.Level}";
        hpText.text = $"HP:{Battler.HP}/{Battler.MaxHP}";
        atText.text = $"AT:{Battler.AT}";
    }
}
