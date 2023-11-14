using System;
using System.Collections;
using System.Collections.Generic;
using Hero.Script.InGame.Jerk;
using UnityEngine;

public class HeroMovable : MonoBehaviour
{
    public Action SwipeNotify;
    private float _speed;
    private float _swipeSpeed;
    private bool isSwipe;
    private Rigidbody2D _heroRigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private RoadSections currentSection = RoadSections.Middle;
    private IHeroJerk _heroJerk;

    public void Initialize(float speed, float swipeSpeed)
    {
        _speed = speed;
        _swipeSpeed = swipeSpeed;
        _heroRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void SetJerk(IHeroJerk jerk)
    {
        _heroJerk = jerk;
    }

    public void StartRun()
    {
        _heroRigidbody2D.velocity = new Vector2(_speed, 0);
    }

    public void StopRun()
    {
        _heroRigidbody2D.velocity = new Vector2(0, 0);
    }

    public void SwipeUp()
    {
        if (currentSection == RoadSections.Top) return;
        if (_heroJerk != null) _heroJerk.DoJerk();
        StartCoroutine(Swipe(true));
        SwipeNotify.Invoke();
    }

    public void SwipeDown()
    {
        if (currentSection == RoadSections.Bottom) return;
        if (_heroJerk != null) _heroJerk.DoJerk();
        StartCoroutine(Swipe(false));
        SwipeNotify.Invoke();
    }

    public void StartHookState(float speed)
    {
        if (isSwipe) return;

        _heroRigidbody2D.velocity = new Vector2(speed, 0);

        gameObject.GetComponent<Animator>().speed = 0;
    }

    public void EndHookState()
    {
        if (isSwipe) return;
        StartRun();
        gameObject.GetComponent<Animator>().speed = 1;
    }

    private IEnumerator Swipe(bool isUpDirection)
    {
        if (isSwipe) yield break;
        var currentSwipeSpeed = _heroJerk != null ? _heroJerk.jerkSpeed : _swipeSpeed;
        isSwipe = true;
        currentSection += isUpDirection ? -1 : 1;
        UpdateSortingLayer();
        _heroRigidbody2D.velocity = new Vector2(_speed, isUpDirection ? currentSwipeSpeed : -currentSwipeSpeed);
        var startPosition = gameObject.transform.position.y;
        var endPosition = startPosition +
                          (isUpDirection
                              ? RoadSectionsValuesStorage.sectionHeigh
                              : -RoadSectionsValuesStorage.sectionHeigh);

        for (;;)
        {
            if (isUpDirection)
            {
                if (endPosition <= gameObject.transform.position.y)
                {
                    EndSwipe();
                    yield break;
                }
            }
            else
            {
                if (endPosition >= gameObject.transform.position.y)
                {
                    EndSwipe();
                    yield break;
                }
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private void EndSwipe()
    {
        if(_heroJerk != null) _heroJerk.EndJerk();
        isSwipe = false;
        StartRun();
        SwipeNotify.Invoke();
    }

    private void UpdateSortingLayer()
    {
        _spriteRenderer.sortingOrder = 2;
        switch ((int) currentSection)
        {
            case 1:
            {
                _spriteRenderer.sortingLayerName = "GameplayTop";
                break;
            }
            case 2:
            {
                _spriteRenderer.sortingLayerName = "GameplayMiddle";
                break;
            }
            case 3:
            {
                _spriteRenderer.sortingLayerName = "GameplayBottom";
                break;
            }
        }
    }
}