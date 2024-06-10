using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    //UI�̊Ǘ�
    //Battler�̊Ǘ�

    public Battler Battler { get; set; }


    public virtual void Setup(Battler battler)
    {
        Battler = battler;
       //UI�̏�����
    }

    public virtual void UpdateUI()
    {

    }
}
