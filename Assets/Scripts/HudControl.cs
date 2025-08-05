using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HudControl:MonoBehaviour 
{
    [SerializeField] public TextMeshProUGUI NameLvl;
    public Slider HPslider;

    public void SetHUD(Unit unit)
    {
        NameLvl.text = unit.UnitName + " Lvl." + unit.UnitLvl;
        HPslider.maxValue = unit.MaxHealth;
        HPslider.value = unit.CurHealth;
    }
}
