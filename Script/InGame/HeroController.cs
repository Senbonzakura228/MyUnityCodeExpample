using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    private HeroView _heroView;

    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;

    public void Initialize(HeroView heroView)
    {
        _heroView = heroView;
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("up");
            _startTouchPosition = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Debug.Log("down");
            _endTouchPosition = Input.GetTouch(0).position;
            if (_endTouchPosition.y < _startTouchPosition.y) _heroView.SwipeDown();
            if (_endTouchPosition.y > _startTouchPosition.y) _heroView.SwipeUp();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            _heroView.SwipeUp();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _heroView.SwipeDown();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _heroView.MeleeAttack();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _heroView.RangeAttack();
        }
    }

    public void MeleeAttack()
    {
        _heroView.MeleeAttack();
    }

    public void RangeAttack()
    {
        _heroView.RangeAttack();
    }
    
    public void OnTakeDamage()
    {
    }

    public void Heal(float hp)
    {
        _heroView.Heal(hp);
    }

    public void GainEnergy(float energy)
    {
        _heroView.GainEnergy(energy);
    }

    public void StartHookState(float hookSpeed)
    {
        _heroView.StartHookState(hookSpeed);
    }

    public void EndHookState()
    {
        _heroView.EndHook();
    }

    public void TakeDamage(float damage)
    {
        _heroView.TakeDamage(damage);
    }
}