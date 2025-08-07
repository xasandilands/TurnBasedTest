using UnityEngine;

public class Unit : MonoBehaviour
{
    public string UnitName;
    public int UnitLvl;

    public int Damage;
    public int Speed;

    public int Potions;
    public int Healing;

    public int MaxHealth;
    public int CurHealth;

    public bool IsGuarding;

    public bool TakeDmg(int dmg)
    {
        if (IsGuarding)
        {
            CurHealth -= dmg / 2;
        }

        CurHealth -= dmg;
        
        if (CurHealth <= 0 )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool heal()
    {
        if (CurHealth == MaxHealth)
        {
            return false;
        }

        CurHealth += Healing;
        if (CurHealth > MaxHealth)
        {
            CurHealth = MaxHealth;
        }
        return true;
    }
}
