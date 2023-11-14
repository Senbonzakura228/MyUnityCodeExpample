
    using Hero.Script.InGame.Defence;

    public class PsyonicDefenceAbility: Ability
    {
        public override void Initialize()
        {
            base.Initialize();
            ApplyToHero();
        }

        private void ApplyToHero()
        {
            var defence = gameObject.AddComponent<PsyonicDefence>();
            defence.SetStats(_value.Value);
            _heroModule.Hero.SetHeroDefence(defence);
        }
    }
