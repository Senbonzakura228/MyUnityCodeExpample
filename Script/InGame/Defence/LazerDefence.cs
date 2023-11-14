using UnityEngine;

namespace Hero.Script.InGame.Defence
{
    public class LazerDefence : MonoBehaviour, IHeroDefence
    {
        private float _maxBlockDamage;
        private HeroView _heroView;

        public void SetStats(float maxBlockDamage)
        {
            _maxBlockDamage = maxBlockDamage;
            _heroView = gameObject.GetComponentInParent<HeroView>();
        }

        public float PrepareResultingDamage(float damage)
        {
            if (_maxBlockDamage > _heroView.energy) return damage;
            var blockedDamage =  damage - _maxBlockDamage > 0 ? damage - _maxBlockDamage : 0;
            var energyCost = _maxBlockDamage - damage > 0 ? damage : _maxBlockDamage;
            _heroView.TakeEnergy(energyCost);
            return blockedDamage;
        }
    }
}