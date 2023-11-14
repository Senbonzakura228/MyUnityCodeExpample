using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

public class HeroModule : MonoBehaviour
{
    [SerializeField] private InGameHeroModel heroModel;
    [SerializeField] private Transform heroSpawnPosition;
    [SerializeField] private HeroAbilityInstaller heroAbilityInstaller;
    private HeroView _heroView;
    public HeroController heroController { get; private set; }

    [HideInInspector] public DiContainer _diContainer;
    [HideInInspector] public HeroView Hero => _heroView;
    [HideInInspector] public HeroAbilityInstaller HeroAbilityInstaller => heroAbilityInstaller;

    public Transform GetHeroTransform()
    {
        return _heroView.transform;
    }

    public void Initialize()
    {
        InitializeModel();
        InitializeView();
        InitializeController();
    }

    private void InitializeModel()
    {
        var selectedHeroModel = Resources.Load<HeroModel>("ScriptableObjects/HeroModels/" + User.Value.selectedHero);
        heroModel.Initialize(selectedHeroModel);
    }

    private void InitializeView()
    {
        var selectedHeroView = Resources.Load<HeroView>("Prefabs/Heroes/" + User.Value.selectedHero);
        _heroView = _diContainer.InstantiatePrefab(selectedHeroView, transform.parent).GetComponent<HeroView>();
        _heroView.transform.position = heroSpawnPosition.position;
        _heroView.Initialize(heroModel);
    }

    private void InitializeController()
    {
        heroController = _heroView.gameObject.AddComponent<HeroController>();
        heroController.Initialize(_heroView);
    }
}