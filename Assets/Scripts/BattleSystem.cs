
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public enum BattleState { Start, PlayerTurn, EnemyTurn, Win, Lose}
public class BattleSystem : MonoBehaviour
{
    public BattleState State;

    [SerializeField] public TextMeshProUGUI UItext;

    public HudControl PlayerHUD;
    public HudControl EnemyHUD;

    public GameObject PlayerPf;
    public GameObject EnemyPf;

    public Transform PlayerPos;
    public Transform EnemyPos;

    Unit PlayerU;
    Unit EnemyU;

    public bool IsOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        State = BattleState.Start;
        IsOver = false; 
        StartCoroutine(SetUpBattle());
    }

    IEnumerator SetUpBattle()
    {
        GameObject playerOb = Instantiate(PlayerPf, PlayerPos);
        GameObject enemyOb = Instantiate(EnemyPf, EnemyPos);

        PlayerU = playerOb.GetComponent<Unit>();
        EnemyU = enemyOb.GetComponent<Unit>();

        PlayerHUD.SetHUD(PlayerU);
        EnemyHUD.SetHUD(EnemyU);

        UItext.text = "You have enter a battle against " + EnemyU.UnitName + "!";

        yield return new WaitForSeconds(5f);
        if (PlayerU.Speed >= EnemyU.Speed)
        {
            State = BattleState.PlayerTurn;
        }
        else
        {
            State = BattleState.EnemyTurn;
        }

        while(IsOver==false)
        {
            CheckState();
        }
        
    }

    public void CheckState()
    {
        switch(State)
        {
            case BattleState.PlayerTurn:
                PlayerTurn();
                break;

            case BattleState.EnemyTurn:
                EnemyTurn();
                break;

            case BattleState.Win:
                EndBattleWin();
                break;

            case BattleState.Lose:
                EndBattleLose();
                break;
        }
    }

    IEnumerator PlayerAttack()
    {
        bool IsDead = EnemyU.TakeDmg(PlayerU.Damage);
        UItext.text = "Attack has hit!";

        yield return new WaitForSeconds(2);

        EnemyHUD.HpUpdate(EnemyU.CurHealth);
        if (IsDead)
        {
            State = BattleState.Win;
        }
        else
        {
            State = BattleState.EnemyTurn;
        }
    }

    void PlayerTurn()
    {
        UItext.text = "Choose your action this turn";
    }

    void EnemyTurn()
    {
        if(PlayerU.CurHealth > PlayerU.MaxHealth/2 || EnemyU.CurHealth > EnemyU.MaxHealth/2)
        {
            //enemy attack
        }
        else if(EnemyU.CurHealth < EnemyU.MaxHealth/2)
        {
            //enemy heal
        }
        else if(EnemyU.CurHealth < EnemyU.MaxHealth/2 && EnemyU.Potions == 0)
        {
            //Enemy defend
        }
    }
    void EndBattleWin()
    {
        UItext.text = "You have won!";
        IsOver = true;
    }

    void EndBattleLose()
    {
        UItext.text = "You lost...";
        IsOver = true;
    }

    public void OnAttack()
    {
        if (State != BattleState.PlayerTurn)
        {
            return;
        }

        StartCoroutine(PlayerAttack());


    }
}
