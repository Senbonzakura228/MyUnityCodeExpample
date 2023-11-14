using System;
using System.Threading.Tasks;
using UnityEngine;

public class ElectroMeleeAttack : MeleeAttack
{
    private float _damageBoost;
    private float _blinkRange;
    private bool _onBlink;
    private BoxCollider2D _boxCollider2D;

    public void SetStats(float damageBoost, float blinkRange)
    {
        _damageBoost = damageBoost;
        _blinkRange = blinkRange;
        _boxCollider2D = gameObject.AddComponent<BoxCollider2D>();
        _boxCollider2D.size = new Vector2(0.1f, 0.1f);
        _boxCollider2D.offset = new Vector2(3, 0);
        _boxCollider2D.isTrigger = true;
    }

    public override void Attack(float damage, Transform position)
    {
        Blink();
        _damage = damage * (_damageBoost / 10);
        _attackPosition = position;
        _animator.SetBool("isSwordAttack", true);
    }

    public override void OnAttackAction()
    {
    }

    private async void Blink()
    {
        _onBlink = true;
        _boxCollider2D.size = new Vector2(8, 5);
        var heroVelocity = gameObject.GetComponentInParent<Rigidbody2D>().velocity;
        gameObject.GetComponentInParent<Rigidbody2D>().velocity = new Vector2(heroVelocity.x + 100, heroVelocity.y);
        await Task.Delay((int) _blinkRange);
        gameObject.GetComponentInParent<Rigidbody2D>().velocity = new Vector2(heroVelocity.x,
            gameObject.GetComponentInParent<Rigidbody2D>().velocity.y);
        _boxCollider2D.size = new Vector2(0, 0);
        _onBlink = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_onBlink) return;
        var enemy = other.gameObject.GetComponent<UnitLogic>();
        if (!enemy) return;
        if (enemy.GetComponent<SpriteRenderer>().sortingLayerName ==
            gameObject.GetComponentInParent<SpriteRenderer>().sortingLayerName)
        {
            enemy.TakeDamage(_damage);
        }
    }
}