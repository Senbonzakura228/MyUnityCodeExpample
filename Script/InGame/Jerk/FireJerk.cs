using Hero.Script.InGame.Defence.Addition;
using UnityEngine;

namespace Hero.Script.InGame.Jerk
{
    public class FireJerk : MonoBehaviour, IHeroJerk
    {
        private float _jerkSpeed;
        public float jerkSpeed { get => _jerkSpeed; }

        private float _powerFieldDamage;
        private PowerField _powerFieldPrefab;
        private PowerField lastCreatedPowerField;

        public void SetStats(float damage, float jerkSpeed, PowerField powerFieldPrefab)
        {
            _powerFieldDamage = damage;
            _powerFieldPrefab = powerFieldPrefab;
            this._jerkSpeed = jerkSpeed;
        }

        public void DoJerk()
        {
            if (lastCreatedPowerField != null) return;
            lastCreatedPowerField = Instantiate(_powerFieldPrefab, transform);
            lastCreatedPowerField.Initialize(_powerFieldDamage);
        }

        public void EndJerk()
        {
            if(lastCreatedPowerField == null) return;
            DestroyImmediate(lastCreatedPowerField.gameObject);
            lastCreatedPowerField = null;
        }
    }
}