
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        State = BattleState.Start;
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

        PlayerTurn();
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
    void EndBattle()
    {
        if(State == BattleState.Win)
        {
            UItext.text = "You have won!";
        }
        else
        {
            UItext.text = "You lost...";
        }
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
