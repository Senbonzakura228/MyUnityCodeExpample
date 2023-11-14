using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    protected float radius = 3;
    protected Animator _animator;
    protected Transform _attackPosition;
    protected float _damage;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        _animator = gameObject.GetComponentInParent<Animator>();
    }

    public virtual void Attack(float damage, Transform position)
    {
        _damage = damage;
        _attackPosition = position;
        _animator.SetBool("isSwordAttack", true);
    }

    public virtual void OnAttackAction()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_attackPosition.position, radius);
        List<UnitLogic> enemies = new List<UnitLogic>();
        foreach (var coll in colliders)
        {
            var enemy = coll.GetComponent<UnitLogic>();
            if (!enemy) continue;
            if (enemy.GetComponent<SpriteRenderer>().sortingLayerName ==
                gameObject.GetComponentInParent<SpriteRenderer>().sortingLayerName)
            {
                enemy.TakeDamage(_damage);
            }
        }
    }

    public void OnEndAttack()
    {
        _animator.SetBool("isSwordAttack", false);
    }
}