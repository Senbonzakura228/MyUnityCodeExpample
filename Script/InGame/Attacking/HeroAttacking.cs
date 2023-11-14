using System;
using UnityEngine;

public class HeroAttacking : MonoBehaviour
{
    [SerializeField] private Transform rangeAttackPosition;
    [SerializeField] private Transform meleeAttackPosition;
    [SerializeField] private HeroCartridge cartridge;
    [SerializeField] private MeleeAttack _meleeAttack;
    [SerializeField] private RangeAttack _rangeAttack;

    private delegate void EndRangeAttackNotify();

    private delegate void RangeAttackActionNotify();

    private delegate void EndMeleeAttackNotify();

    private delegate void MeleeAttackActionNotify();

    private RangeAttackActionNotify _rangeAttackActionNotify;
    private EndRangeAttackNotify _endRangeAttackNotify;

    private MeleeAttackActionNotify _meleeAttackActionNotify;
    private EndMeleeAttackNotify _endMeleeAttackNotify;
    private bool _isCooldown;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _rangeAttack.Initialize();
        _meleeAttack.Initialize();
        _rangeAttackActionNotify += _rangeAttack.OnAttackAction;
        _meleeAttackActionNotify += _meleeAttack.OnAttackAction;
        _endRangeAttackNotify += _rangeAttack.OnEndAttack;
        _endMeleeAttackNotify += _meleeAttack.OnEndAttack;
    }

    public bool RangeAttack(float damage)
    {
        if (_isCooldown) return false;
        _isCooldown = true;
        _rangeAttack.Attack(damage, rangeAttackPosition, cartridge);
        return true;
    }

    public void MeleeAttack(float damage)
    {
        if (_isCooldown) return;
        _isCooldown = true;
        _meleeAttack.Attack(damage, meleeAttackPosition);
    }

    public void ChangeRangeAttack(RangeAttack newRangeAttack, HeroCartridge cartridge)
    {
        _rangeAttackActionNotify -= _rangeAttack.OnAttackAction;
        _endRangeAttackNotify -= _rangeAttack.OnEndAttack;
        Destroy(_rangeAttack.gameObject);
        _rangeAttack = newRangeAttack;
        _rangeAttack.Initialize();
        _rangeAttackActionNotify += _rangeAttack.OnAttackAction;
        _endRangeAttackNotify += _rangeAttack.OnEndAttack;
        this.cartridge = cartridge;
    }
    
    public void ChangeMeleeAttack(MeleeAttack newMeleeAttack)
    {
        _meleeAttackActionNotify -= _meleeAttack.OnAttackAction;
        _endMeleeAttackNotify -= _meleeAttack.OnEndAttack;
        Destroy(_meleeAttack.gameObject);
        _meleeAttack = newMeleeAttack;
        _meleeAttack.Initialize();
        _meleeAttackActionNotify += _meleeAttack.OnAttackAction;
        _endMeleeAttackNotify += _meleeAttack.OnEndAttack;
    }

    [SerializeField]
    private void OnRangeAttackAction()
    {
        _rangeAttackActionNotify.Invoke();
    }

    [SerializeField]
    private void OnMeleeAttackAction()
    {
        _meleeAttackActionNotify.Invoke();
    }

    [SerializeField]
    private void OnEndRangeAttack()
    {
        _isCooldown = false;
        _endRangeAttackNotify.Invoke();
    }

    [SerializeField]
    private void OnEndMeleeAttack()
    {
        _isCooldown = false;
        _endMeleeAttackNotify.Invoke();
    }
}