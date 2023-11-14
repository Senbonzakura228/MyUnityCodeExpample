
    using Hero.Script.InGame.Defence;
    using Hero.Script.InGame.Defence.Addition;
    using UnityEngine;

    public class ElectroDefenceAbility: Ability
    {
        [SerializeField] private PowerField powerFieldPrefab;
        public override void Initialize()
        {
            base.Initialize();
            ApplyToHero();
        }

        private void ApplyToHero()
        {
            var defence = gameObject.AddComponent<ElectroDefence>();
            defence.SetStats(_value.Value, powerFieldPrefab);
            _heroModule.Hero.SetHeroDefence(defence);
        }
    }
