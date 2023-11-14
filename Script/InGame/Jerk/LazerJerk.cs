using UnityEngine;

namespace Hero.Script.InGame.Jerk
{
    public class LazerJerk : MonoBehaviour, IHeroJerk
    {
        private float _jerkSpeed;
        public float jerkSpeed => _jerkSpeed;
        private float _energyGain;
        private HeroController _heroController;

        public void SetStats(float energy, float jerkSpeed)
        {
            _energyGain = energy;
            this._jerkSpeed = jerkSpeed;
            _heroController = gameObject.GetComponentInParent<HeroController>();
        }

        public void DoJerk()
        {
            _heroController.GainEnergy(_energyGain);
        }

        public void EndJerk()
        {
        }
    }
}