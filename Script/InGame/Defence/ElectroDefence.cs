using Hero.Script.InGame.Defence.Addition;
using UnityEngine;

namespace Hero.Script.InGame.Defence
{
    public class ElectroDefence : MonoBehaviour, IHeroDefence
    {
        private PowerField _powerField;

        public void SetStats(float damage, PowerField powerFieldPrefab)
        {
            _powerField = Instantiate(powerFieldPrefab, transform);
            _powerField.Initialize(damage);
        }

        public float PrepareResultingDamage(float damage)
        {
            return damage;
        }

        private void OnDestroy()
        {
            Destroy(_powerField);
        }
    }
}