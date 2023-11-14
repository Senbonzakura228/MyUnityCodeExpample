using UnityEngine;

public class ElectroCartridge : HeroCartridge
{
    private float _electroDamage;
    
    public new void SetParams(ElectroMadaraCartridgeParams instantiateParams)
    {
        bulletSpeed = bulletSpeed + instantiateParams.speedBoost;
        base.SetParams(instantiateParams);
    }
}