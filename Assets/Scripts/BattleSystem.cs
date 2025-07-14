
using JetBrains.Annotations;
using UnityEngine;

public enum BattleState { Start, PlayerTurn, EnemyTurn, Win, Lose}
public class BattleSystem : MonoBehaviour
{
    public BattleState State;

    public GameObject Player;
    public GameObject Enemy;

    public Transform PlayerPos;
    public Transform EnemyPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        State = BattleState.Start;
        SetUpBattle();
    }

    void SetUpBattle()
    {
        Instantiate(Player, PlayerPos);
        Instantiate(Enemy, EnemyPos);
    }
}
