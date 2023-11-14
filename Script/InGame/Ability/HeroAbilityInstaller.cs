using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HeroAbilityInstaller : MonoBehaviour
{
    [Inject] private HeroModule _heroModule;
    [Inject] private DiContainer _diContainer;
    private Dictionary<HeroAbilityType, Ability> _currentAbilities = new Dictionary<HeroAbilityType, Ability>();

    public void AddAbility(Ability ability)
    {
        if (_currentAbilities.ContainsKey(ability.AbilityType))
        {
            if (_currentAbilities[ability.AbilityType].SchollType == ability.SchollType)
            {
                UpgradeAbility(_currentAbilities[ability.AbilityType]);
            }
            else
            {
                _currentAbilities[ability.AbilityType].Destroy();
                _currentAbilities.Remove(ability.AbilityType);
                CreateAbility(ability);
            }
        }
        else
        {
            CreateAbility(ability);
        }
    }

    private void CreateAbility(Ability ability)
    {
        var createdAbility = _diContainer.InstantiatePrefab(ability, _heroModule.Hero.transform)
            .GetComponent<Ability>();
        _currentAbilities.Add(createdAbility.AbilityType, createdAbility);
    }

    private void UpgradeAbility(Ability ability)
    {
        Debug.Log("upgrade");
        _currentAbilities[ability.AbilityType].Upgrade();
    }
}