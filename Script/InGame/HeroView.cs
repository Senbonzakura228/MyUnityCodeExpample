using System.Collections;
using System.Threading.Tasks;
using Hero.Script.InGame.Defence;
using Hero.Script.InGame.Jerk;
using UnityEngine;
using Zenject;

public class HeroView : MonoBehaviour
{
    [Inject] private MainInGameCanvas _mainInGameCanvas;
    private InGameHeroModel heroModel;
    [SerializeField] private HeroMovable movable;
    [SerializeField] private HeroAttacking attacking;
    [SerializeField] private float energyRegenerationKoeff;
    [SerializeField] private float shotEnergyCost;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color damageTakingColor;
    [SerializeField] private int damageTakingColorizeTime;
    [SerializeField] private HeroSoundModule _heroSoundModule;
    public HeroAttacking Attacking => attacking;
    public HeroSoundModule HeroSoundModule => _heroSoundModule;

    private IHeroDefence _heroDefence;
    private float _hp;
    private float _energy;
    private bool _hasStunned;
    private bool _onSwipe;
    private bool _onDie;
    private bool _isDamageTaking;

    public float energy => _energy;

    private void OnDestroy()
    {
        movable.SwipeNotify -= ChangeSwipeStatus;
    }

    public void Initialize(InGameHeroModel heroModel)
    {
        this.heroModel = heroModel;
        _hp = this.heroModel.heatPoint;
        _energy = this.heroModel.energy;
        movable.Initialize(this.heroModel.Speed, this.heroModel.SwipeSpeed);
        movable.StartRun();
        movable.SwipeNotify += ChangeSwipeStatus;
        _mainInGameCanvas.statsUI.Initialize(_hp, _energy);
        StartCoroutine(EnergyRegeneration());
    }

    public void SetHeroDefence(IHeroDefence heroDefence)
    {
        _heroDefence = heroDefence;
    }
    
    public void SetHeroJerk(IHeroJerk heroJerk)
    {
        movable.SetJerk(heroJerk);
    }

    public void TakeEnergy(float count)
    {
        _energy -= count;
        if (_energy < 0)
        {
            _energy = 0;
        }
        _mainInGameCanvas.statsUI.SetEnergy(_energy);
    }
    
    public void SwipeUp()
    {
        if (CheckAvailabilityStatus()) return;
        movable.SwipeUp();
    }

    public void SwipeDown()
    {
        if (CheckAvailabilityStatus()) return;
        movable.SwipeDown();
    }

    public void MeleeAttack()
    {
        if (CheckAvailabilityStatus()) return;
        attacking.MeleeAttack(heroModel.meleeDamage);
    }

    public void RangeAttack()
    {
        if (CheckAvailabilityStatus()) return;
        if (_energy < shotEnergyCost) return;
        if (attacking.RangeAttack(heroModel.rangeDamage))
        {
            _energy -= shotEnergyCost;
            _mainInGameCanvas.statsUI.SetEnergy(_energy);
        }
    }

    public void Heal(float hp)
    {
        if (hp + _hp > heroModel.heatPoint)
        {
            this._hp = heroModel.heatPoint;
            _mainInGameCanvas.statsUI.SetHP(this._hp);
            return;
        }

        this._hp += hp;
        _mainInGameCanvas.statsUI.SetHP(this._hp);
    }
    
    public void GainEnergy(float energy)
    {
        if (energy + _energy > heroModel.energy)
        {
            this._energy = heroModel.energy;
            _mainInGameCanvas.statsUI.SetEnergy(this._energy);
            return;
        }

        this._energy += energy;
        _mainInGameCanvas.statsUI.SetEnergy(this._energy);
    }
    
    private async void AnimateDamageTaking()
    {
        _isDamageTaking = true;
        var oldColor = spriteRenderer.color;
        spriteRenderer.color = damageTakingColor;
        await Task.Delay(damageTakingColorizeTime);
        if (!spriteRenderer) return;
        spriteRenderer.color = oldColor;
        _isDamageTaking = false;
    }

    public void StartHookState(float hookSpeed)
    {
        if (CheckAvailabilityStatus()) return;
        _hasStunned = true;
        movable.StartHookState(hookSpeed);
    }

    public void EndHook()
    {
        _hasStunned = false;
        movable.EndHookState();
    }

    public void TakeDamage(float damage)
    {
        if (!_isDamageTaking) AnimateDamageTaking();
        if (_heroDefence != null)
        {
            damage = _heroDefence.PrepareResultingDamage(damage);
        }

        _heroSoundModule.PlayDamageTakingSound();
        _hp -= damage;
        _mainInGameCanvas.statsUI.SetHP(_hp);
        if (_hp <= 0)
        {
            StartDie();
        }
    }

    private IEnumerator EnergyRegeneration()
    {
        for (;;)
        {
            if (_energy < heroModel.energy)
            {
                _energy += heroModel.energy * energyRegenerationKoeff;
            }

            if (_energy > heroModel.energy)
            {
                _energy = heroModel.energy;
            }
            
            _mainInGameCanvas.statsUI.SetEnergy(_energy);

            yield return new WaitForFixedUpdate();
        }
    }

    private void StartDie()
    {
        if(_onDie) return;
        _onDie = true;
        Time.timeScale = 0;
          _mainInGameCanvas.ShowGameOverScreen();
    }

    private bool CheckAvailabilityStatus()
    {
        return _hasStunned || _onSwipe;
    }

    private void ChangeSwipeStatus()
    {
        _onSwipe = !_onSwipe;
    }

    private void DetectPoint(Collider2D collider)
    {
        var point = collider.GetComponent<LevelPoint>();
        if (point == null) return;
        if (collider.GetComponent<SpriteRenderer>().sortingLayerName !=
            gameObject.GetComponent<SpriteRenderer>().sortingLayerName) return;
        point.Take();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        DetectPoint(collider);
    }
}