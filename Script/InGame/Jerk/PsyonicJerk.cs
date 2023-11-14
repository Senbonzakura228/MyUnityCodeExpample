using UnityEngine;

namespace Hero.Script.InGame.Jerk
{
    public class PsyonicJerk : MonoBehaviour, IHeroJerk
    {
        private float _jerkSpeed;
        public float jerkSpeed => _jerkSpeed;
        private float _heal;
        private HeroController _heroController;

        public void SetStats(float heal, float jerkSpeed)
        {
            _heal = heal;
            this._jerkSpeed = jerkSpeed;
            _heroController = gameObject.GetComponentInParent<HeroController>();
        }

        public void DoJerk()
        {
            _heroController.Heal(_heal);
        }

        public void EndJerk()
        {
        }
    }
}