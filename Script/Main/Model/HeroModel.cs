using UnityEngine;


[CreateAssetMenu(fileName = "Hero", menuName = "Data/Hero", order = 1)]
public class HeroModel : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private int greenWalletCost;
    [SerializeField] private int purpleWalletCost;
    [SerializeField] private float hp;
    [SerializeField] private float rangeWeaponDamage;
    [SerializeField] private float meleeWeaponDamage;
    [SerializeField] private Sprite icon;
    [SerializeField] private Sprite preview;
    [SerializeField] private RuntimeAnimatorController  animatorController;
    [SerializeField] [TextArea] private string additionalInfo;
    [SerializeField] private int maxUpgradeIndex;

    [SerializeField] public StatUpgrade hpUpgrade;
    [SerializeField] public StatUpgrade rangeWeaponDamageUpgrade;
    [SerializeField] public StatUpgrade meleeWeaponDamageUpgrade;
    [SerializeField] public StatUpgrade energyUpgrade;

    public string Name => name;
    public Sprite Icon => icon;
    public Sprite Preview => preview;
    public RuntimeAnimatorController  AnimatorController => animatorController;

    public string AdditionalInfo => additionalInfo;

    public int GreenWalletCost => greenWalletCost;
    public int PurpleWalletCost => purpleWalletCost;

    public int MaxUpgradeIndex => maxUpgradeIndex;
}