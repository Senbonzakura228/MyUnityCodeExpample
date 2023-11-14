using UnityEngine;

namespace Hero.Script.InGame.Defence
{
    public class PsyonicDefence : MonoBehaviour, IHeroDefence
    {
        private float _damageResist;

        public void SetStats(float damageResist)
        {
            _damageResist = damageResist;
        }
        
        public float PrepareResultingDamage(float damage)
        {
            var resultDamage = damage - _damageResist;
            return resultDamage > 0 ? resultDamage : 0;
        }
    }
}