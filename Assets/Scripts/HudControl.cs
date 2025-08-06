using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HudControl:MonoBehaviour 
{
    [SerializeField] public TextMeshProUGUI NameLvl;
    [SerializeField] public TextMeshProUGUI HpText;
    public Slider HPslider;

    public void SetHUD(Unit unit)
    {
        NameLvl.text = unit.UnitName + " Lvl." + unit.UnitLvl;
        HpText.text = unit.CurHealth + "/" + unit.MaxHealth;
        HPslider.maxValue = unit.MaxHealth;
        HPslider.value = unit.CurHealth;
    }

    public void HpUpdate(int hp)
    {
        HPslider.value = hp;
    }
}
