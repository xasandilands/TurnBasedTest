
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Unity.VisualScripting;
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
            PlayerTurn();
        }
        else
        {
            State = BattleState.EnemyTurn;
            EnemyTurn();
        }

      
        
    }

    void PlayerTurn()
    {
        UItext.text = "Choose your action this turn";
    }

    IEnumerator PlayerAttack()
    {
        bool IsDead = EnemyU.TakeDmg(PlayerU.Damage);
        UItext.text = "Attack has hit!";

        yield return new WaitForSeconds(2);
        if(EnemyU.IsGuarding)
        {
            UItext.text = EnemyU.UnitName + " braced for the attack!";
            yield return new WaitForSeconds(2);
        }

        EnemyHUD.HpUpdate(EnemyU.CurHealth);
        EnemyHUD.SetHUD(EnemyU);
        if (IsDead)
        {
            State = BattleState.Win;
            EndBattleWin();
        }
        else
        {
            State = BattleState.EnemyTurn;
            EnemyTurn();

        }
    }

    IEnumerator PlayerGuard()
    {
        PlayerU.IsGuarding = true;
        UItext.text = "You Brace yourself";
        yield return new WaitForSeconds(2);

        EnemyTurn();
    }

    IEnumerator PlayerHeal()
    {
        if (PlayerU.Potions <= 0)
        {
            UItext.text = "You have no potions left!";
            yield return new WaitForSeconds(1);
            PlayerTurn();
        }
        else
        {
            bool IsMax = PlayerU.heal();

            if (IsMax)
            {
                UItext.text = "You are already at full HP!";
                yield return new WaitForSeconds(1);
                PlayerTurn();
            }
            else
            {
                UItext.text = "You used a potion to heal yourself!";
                yield return new WaitForSeconds(2);
                EnemyTurn();
            }
        }
    }

    void EnemyTurn()
    {

        if(PlayerU.CurHealth > PlayerU.MaxHealth/2 || EnemyU.CurHealth > EnemyU.MaxHealth/2)
        {
            StartCoroutine(EnemyAttack());
        }
        else if(EnemyU.CurHealth < EnemyU.MaxHealth/2)
        {
            StartCoroutine(EnemyHeal());
        }
        else if(EnemyU.CurHealth < EnemyU.MaxHealth/2 && EnemyU.Potions == 0)
        {
            StartCoroutine(EnemyGuard());
        }
    }

    IEnumerator EnemyAttack()
    {
        bool IsDead = PlayerU.TakeDmg(EnemyU.Damage);
        UItext.text = EnemyU.UnitName + " Attacks!";

        yield return new WaitForSeconds(2);

        PlayerHUD.HpUpdate(PlayerU.CurHealth);
        PlayerHUD.SetHUD(PlayerU);
        if (IsDead)
        {
            State = BattleState.Lose;
        }
        else
        {
            State = BattleState.PlayerTurn;
            PlayerTurn();
        }
    }

    IEnumerator EnemyGuard()
    {
        EnemyU.IsGuarding = true;
        UItext.text = EnemyU.UnitName + " Braces";
        yield return new WaitForSeconds(2);

        PlayerTurn();
    }

    IEnumerator EnemyHeal()
    {
        bool IsMax = EnemyU.heal();
        if(IsMax)
        {
            UItext.text = EnemyU.UnitName + " Tries to heal but is at full hp!";
            yield return new WaitForSeconds(2);
            EnemyTurn();
        }

        UItext.text = EnemyU.UnitName + " uses a potion to heal";
        yield return new WaitForSeconds(2);
        PlayerTurn();
    }

    void EndBattleWin()
    {
        UItext.text = "You have won!";
        //add player EXP
        //end game
    }

    void EndBattleLose()
    {
        UItext.text = "You lost...";
        //prompt player to try again
    }

    public void OnAttack()
    {
        if (State != BattleState.PlayerTurn)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    public void OnGuard()
    {
        if(State != BattleState.PlayerTurn)
        {
            return;
        }

        StartCoroutine (PlayerGuard());
    }

    public void OnHeal()
    {
        if (State != BattleState.PlayerTurn)
        {
            return;
        }

        StartCoroutine(PlayerHeal());
    }
}
