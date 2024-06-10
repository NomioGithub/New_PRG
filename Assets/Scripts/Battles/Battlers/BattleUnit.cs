using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    //UI‚ÌŠÇ—
    //Battler‚ÌŠÇ—

    public Battler Battler { get; set; }


    public virtual void Setup(Battler battler)
    {
        Battler = battler;
       //UI‚Ì‰Šú‰»
    }

    public virtual void UpdateUI()
    {

    }
}
