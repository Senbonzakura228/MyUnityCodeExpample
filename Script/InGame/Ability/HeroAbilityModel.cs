using System;
using System.Collections.Generic;
using UnityEngine;

public class HeroAbilityModel : MonoBehaviour
{
    [SerializeField]
    private SchoolAbilitiesValueStorage[] schoolAbilitiesValueStorage = new SchoolAbilitiesValueStorage [4];

    public AbilityValue GetAbilityValue(AbilitySchollType scholl, HeroAbilityType type)
    {
        var value = new AbilityValue();
        foreach (var abilitiesValueStorage in schoolAbilitiesValueStorage)
        {
            if (abilitiesValueStorage.SchollType != scholl) continue;
            foreach (var abilityValue in abilitiesValueStorage.AbilityValues)
            {
                if (abilityValue.Type == type)
                {
                    value = abilityValue;
                }
            }
        }

        return value;
    }
    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
    }
}