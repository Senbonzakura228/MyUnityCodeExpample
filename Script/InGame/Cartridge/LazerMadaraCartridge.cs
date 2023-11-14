using System.Threading.Tasks;
using System.Timers;
using UnityEngine;

public class LazerMadaraCartridge : HeroCartridge
{
    [SerializeField] private int damageInterval;
    [SerializeField] private int iterationCount;
    private float _periodDamage;
    private UnitLogic _enemy;

    public new void SetParams(LazerMadaraCartridgeInstantiateParams instantiateParams)
    {
        _periodDamage = instantiateParams.periodDamage;
        base.SetParams(instantiateParams);
    }

    protected override void DoDamage(UnitLogic enemy)
    {
        CreateContactExplosion();
        enemy.TakeDamage(damage);
        StartPeriodDamage(enemy);
    }

    private async void StartPeriodDamage(UnitLogic enemy)
    {
        _enemy = enemy;
        _enemy.DieNotify += EndDamage;
        gameObject.SetActive(false);
        for (int i = 0; i < iterationCount; i++)
        {
            await Task.Delay(damageInterval);
            DoPeriodDamage();
        }

        EndDamage();
    }

    private void DoPeriodDamage()
    {
        if (!_enemy) return;
        _enemy.TakeDamage(_periodDamage);
    }

    private void EndDamage()
    {
        _enemy.DieNotify -= EndDamage;
        DestroyCartridge();
    }
}