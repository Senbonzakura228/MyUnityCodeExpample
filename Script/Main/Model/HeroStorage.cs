using System;
using UnityEngine;

[Serializable]
public class HeroStorage
{
    public Action StorageChangeNotify;

    [SerializeField] private HeroModel[] storage;
    public HeroModel[] Storage => storage;

    public void InitialiseUserHeroUpgrades(UserHeroUpgrade[] userHeroUpgrades)
    {
        foreach (var heroModel in storage)
        {
            foreach (var userHeroUpgrade in userHeroUpgrades)
            {
                if (userHeroUpgrade.HeroName != heroModel.Name) continue;
                heroModel.hpUpgrade.SetCurrentResearchedIndex(userHeroUpgrade.hpIndex);
                heroModel.meleeWeaponDamageUpgrade.SetCurrentResearchedIndex(userHeroUpgrade.meleeWeaponDamageIndex);
                heroModel.rangeWeaponDamageUpgrade.SetCurrentResearchedIndex(userHeroUpgrade.rangeWeaponDamageIndex);
                heroModel.energyUpgrade.SetCurrentResearchedIndex(userHeroUpgrade.ultimateIndex);
                heroModel.hpUpgrade.ChangeNotify += Notify;
                heroModel.meleeWeaponDamageUpgrade.ChangeNotify += Notify;
                heroModel.rangeWeaponDamageUpgrade.ChangeNotify += Notify;
                heroModel.energyUpgrade.ChangeNotify += Notify;
            }
        }

        StorageChangeNotify?.Invoke();
    }
    
    public void UnsubscribeNotifications()
    {
        foreach (var heroModel in storage)
        {
            heroModel.hpUpgrade.ChangeNotify -= Notify;
            heroModel.meleeWeaponDamageUpgrade.ChangeNotify -= Notify;
            heroModel.rangeWeaponDamageUpgrade.ChangeNotify -= Notify;
            heroModel.energyUpgrade.ChangeNotify -= Notify;
        }
    }

    public void InitialiseUserHeroUpgrades()
    {
        foreach (var heroModel in storage)
        {
            heroModel.hpUpgrade.SetCurrentResearchedIndex(0);
            heroModel.meleeWeaponDamageUpgrade.SetCurrentResearchedIndex(0);
            heroModel.rangeWeaponDamageUpgrade.SetCurrentResearchedIndex(0);
            heroModel.energyUpgrade.SetCurrentResearchedIndex(0);
            heroModel.hpUpgrade.ChangeNotify += Notify;
            heroModel.meleeWeaponDamageUpgrade.ChangeNotify += Notify;
            heroModel.rangeWeaponDamageUpgrade.ChangeNotify += Notify;
            heroModel.energyUpgrade.ChangeNotify += Notify;
        }
    }

    private void Notify()
    {
        StorageChangeNotify.Invoke();
    }
}