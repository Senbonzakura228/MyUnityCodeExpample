using System;
using UnityEngine;
using Zenject;

public class Ability : MonoBehaviour
{
    [Inject] protected HeroModule _heroModule;
    [Inject] protected HeroAbilityModel _heroAbilityModel;
    [SerializeField] protected AbilitySchollType _schollType;
    [SerializeField] protected HeroAbilityType _abilityType;
    [SerializeField] protected string abilityName;
    [SerializeField] [TextAreaAttribute] protected string description;
    [SerializeField] protected Sprite icon;
    protected AbilityValue _value;
    protected int _level;

    public Sprite Icon => icon;
    public string AbilityName => abilityName;
    public string Description => description;

    public int Level => _level;
    public AbilitySchollType SchollType => _schollType;
    public HeroAbilityType AbilityType => _abilityType;

    protected void Awake()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        _value = _heroAbilityModel.GetAbilityValue(_schollType, _abilityType);
    }

    public virtual void Upgrade()
    {
        _value.Value = _value.Value + _value.scaleValue;
        _value.speedBoost = _value.speedBoost + _value.speedBoostScale;
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }
}