using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HudControl:MonoBehaviour 
{

    [SerializeField] public TextMeshProUGUI UnitName;
    [SerializeField] public TextMeshProUGUI Health;


    public Unit UnitPf;

    void Start()
    {
        HUDSpawn();
    }

    void HUDSpawn()
    {
        UnitName.text = UnitPf.UnitName + " Lvl." + UnitPf.UnitLvl;
        Health.text = UnitPf.MaxHealth + "/" + UnitPf.MaxHealth;
    }
}
