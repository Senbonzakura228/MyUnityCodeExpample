using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider energyBar;
    [SerializeField] private int maxHPvalue;
    [SerializeField] private int maxEnergyValue;

    public void Initialize(float maxHP, float maxEnergy)
    {
        hpBar.maxValue = maxHPvalue;
        energyBar.maxValue = maxEnergyValue;
        hpBar.value = maxHP;
        energyBar.value = maxEnergy;
    }

    public void SetHP(float value)
    {
        hpBar.value = value;
    }
    
    public void SetEnergy(float value)
    {
        energyBar.value = value;
    }
}
