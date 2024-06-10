using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleSystem : MonoBehaviour
{
    enum State
    {
        Start,
        ActionSelection,
        MovenSelection,
        RunTurns,
        BattleOver
    }

    State state;

    [SerializeField] ActionSelectionUI actionSelectionUI;
    [SerializeField] MoveSelectionUI moveSelectionUI;
    [SerializeField] BattleDialog battleDialog;

    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;

    public UnityAction OnBattleOver;



    public void BattleStart(Battler player, Battler enemy)
    {
        state = State.Start;
        Debug.Log("バトル開始");
        actionSelectionUI.Init();
        moveSelectionUI.Init();
        actionSelectionUI.Close();
        StartCoroutine(SetupBattle(player,enemy));
    }

    IEnumerator SetupBattle(Battler player,Battler enemy)
    {
        playerUnit.Setup(player);
        enemyUnit.Setup(enemy);

        //yield return battleDialog.TypeDialog("雪だるまが現れた！\nコマンド？");
        yield return battleDialog.TypeDialog($"{enemy.Base.Name}が現れた！\nどうする ? ");
        ActionSelection();
    }


    void BattleOver()
    {
        OnBattleOver?.Invoke();
    }

    void ActionSelection()
    {
        state = State.ActionSelection;
        actionSelectionUI.Open();
    }

    void MoveSelection()
    {
        state = State.MovenSelection;
        moveSelectionUI.Open();
    }

    IEnumerator RunTurns()
    {
        state = State.RunTurns;
        yield return RunMove(playerUnit, enemyUnit);
        if(state == State.BattleOver)
        {
            yield return battleDialog.TypeDialog($"{enemyUnit.Battler.Base.Name}を倒した！");
            BattleOver();
            yield break;
        }

        yield return RunMove(enemyUnit, playerUnit);

        if (state == State.BattleOver)
        {
            yield return battleDialog.TypeDialog($"{playerUnit.Battler.Base.Name}は倒れてしまった！");
            BattleOver();
            yield break;
        }

        yield return battleDialog.TypeDialog("コマンド？", auto: false);
        ActionSelection();
    }

    IEnumerator RunMove(BattleUnit sourceUnit,BattleUnit targetUnit)
    {
        int damage = targetUnit.Battler.TakeDamage(sourceUnit.Battler);
        yield return battleDialog.TypeDialog($"{sourceUnit.Battler.Base.Name}の攻撃\n{targetUnit.Battler.Base.Name}は{damage}のダメージ", auto: false);
        targetUnit.UpdateUI();

        if(targetUnit.Battler.HP <= 0)
        {
            state = State.BattleOver;
        }

    }


    public void Update()
    {
        //if(Input.GetKeyDown(KeyCode.R))
        //{
        //    gameController.EndBatle();
        //}
        switch (state)
        {
            case State.Start:
                break;
            case State.ActionSelection:
                HandleActionSelection();
                break;
            case State.MovenSelection:
                HandleMoveSelection();
                break;
            case State.BattleOver:
                break;

        }
    }

    void HandleActionSelection()
    {
        actionSelectionUI.HandleUpdate();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (actionSelectionUI.SelectedIndex == 0)
            {
                MoveSelection();

            }
            else if (actionSelectionUI.SelectedIndex == 1)
            {
                //逃げる
                BattleOver();
            }

        }
    }
    void HandleMoveSelection()
    {
        moveSelectionUI.HandleUpdate();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //技の実行をする
            actionSelectionUI.Close();
            moveSelectionUI.Close();
            StartCoroutine(RunTurns());
            RunTurns();

        }
        else if (Input.GetKeyDown(KeyCode.X))
        {

            moveSelectionUI.Close();
            ActionSelection();
        }

    }
}
