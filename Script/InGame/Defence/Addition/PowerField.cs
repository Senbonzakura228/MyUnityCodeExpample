using System;
using UnityEngine;

namespace Hero.Script.InGame.Defence.Addition
{
    public class PowerField : MonoBehaviour
    {
        private float _damage;
        private HeroView _heroView;

        public void Initialize(float damage)
        {
            _damage = damage;
            _heroView = gameObject.GetComponentInParent<HeroView>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            var enemy = TryDetectEnemy(other);
            if (!enemy) return;
            enemy.TakeDamage(_damage);
        }

        private UnitLogic TryDetectEnemy(Collider2D collider2D)
        {
            var enemy = collider2D.GetComponent<UnitLogic>();
            if (!enemy) return null;
            return enemy.GetComponent<SpriteRenderer>().sortingLayerName !=
                   _heroView.GetComponent<SpriteRenderer>().sortingLayerName
                ? null
                : enemy;
        }
    }
}