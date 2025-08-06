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
    public int CurHealth;//current health

    public bool TakeDmg(int dmg)
    {
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
}
