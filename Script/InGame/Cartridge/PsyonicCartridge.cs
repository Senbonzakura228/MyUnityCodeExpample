using System.Threading.Tasks;
using UnityEngine;

public class PsyonicCartridge : HeroCartridge
{
    [SerializeField] private int damageDelay;
    private float _afterDamage;

    public new void SetParams(PsyinicMadaraCartridgeInstantiateParams instantiateParams)
    {
        _afterDamage = instantiateParams.afterDamage;
        base.SetParams(instantiateParams);
    }

    protected override void DoDamage(UnitLogic enemy)
    {
        CreateContactExplosion();
        enemy.TakeDamage(damage);
        DoAfterDamage(enemy);
        DestroyCartridge();
    }

    private async void DoAfterDamage(UnitLogic enemy)
    {
        await Task.Delay(damageDelay);
        if (!enemy) return;
        enemy.TakeDamage(_afterDamage);
    }
}