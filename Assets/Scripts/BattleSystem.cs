
using JetBrains.Annotations;
using UnityEngine;

public enum BattleState { Start, PlayerTurn, EnemyTurn, Win, Lose}
public class BattleSystem : MonoBehaviour
{
    public BattleState State;

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
        SetUpBattle();
    }

    void SetUpBattle()
    {
       GameObject playerOb = Instantiate(PlayerPf, PlayerPos);
       GameObject enemyOb = Instantiate(EnemyPf, EnemyPos);

       PlayerU = playerOb.GetComponent<Unit>();
       EnemyU = enemyOb.GetComponent<Unit>();
       
       // EnemyU.unitName
    }
}
