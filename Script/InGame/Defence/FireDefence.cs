using System;
using System.Collections;
using UnityEngine;

namespace Hero.Script.InGame.Defence
{
    public class FireDefence : MonoBehaviour, IHeroDefence
    {
        private float _hpRegenerationCount;
        private HeroView _heroView;
        private Coroutine _regeneration;
        
        
        public void SetStats(float hpRegenerationCount)
        {
            _hpRegenerationCount = hpRegenerationCount;
            _heroView = gameObject.GetComponentInParent<HeroView>();
            _regeneration = StartCoroutine(Regeneration());
        }
        
        public float PrepareResultingDamage(float damage)
        {
            return damage;
        }

        private IEnumerator Regeneration()
        {
            for (;;)
            {
                if(_heroView == null) yield break;
                _heroView.Heal(_hpRegenerationCount);
                yield return new WaitForSeconds(1);
            }
        }

        private void OnDestroy()
        {
            if (_regeneration != null)
            {
                StopCoroutine(_regeneration);
            }
        }
    }
}