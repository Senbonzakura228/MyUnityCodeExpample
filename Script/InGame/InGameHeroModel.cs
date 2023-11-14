using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameHeroModel : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float swipeSpeed;
    public string heroName { private set; get; }
    public float meleeDamage { private set; get; }
    public float rangeDamage { private set; get; }
    public float heatPoint { private set; get; }

    public float energy { private set; get; }

    public void Initialize(HeroModel heroModel)
    {
        heroName = heroModel.Name;
        meleeDamage = heroModel.meleeWeaponDamageUpgrade.GetCurrentValue();
        rangeDamage = heroModel.rangeWeaponDamageUpgrade.GetCurrentValue();
        heatPoint = heroModel.hpUpgrade.GetCurrentValue();
        energy = heroModel.energyUpgrade.GetCurrentValue();
    }

    public float Speed => speed;

    public float SwipeSpeed => swipeSpeed;
}