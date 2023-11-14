using System;
using System.Collections;
using MainMenu.Pages.Locations.Script;
using UnityEngine;
using Zenject;

public class ProgressTracker : MonoBehaviour
{
    [Inject] private HeroModule _heroModule;
    [SerializeField] private float progressKoeff;
    [SerializeField] private float GreenWalletReceiveKoeff;
    [SerializeField] private float gainPointResourcesLocationScaleKoeff;
    [SerializeField] private float updateInterval;
    private int _greenWalletReceived;
    private int _purpleWalletReceived;
    private readonly JsonDataSaveService _saveService = new JsonDataSaveService();
    private const string SAVE_PATH = "location";

    public float GainPointResourcesLocationScaleKoeff
    {
        get => gainPointResourcesLocationScaleKoeff;
    }

    public int GreenWalletReceived
    {
        get => (int) (currentProgress * GreenWalletReceiveKoeff) + _greenWalletReceived;
        set => _greenWalletReceived = +value;
    }

    public int PurpleWalletReceived
    {
        get => _purpleWalletReceived;
        set => _purpleWalletReceived = +value;
    }

    public float currentProgress { get; private set; }

    private void Awake()
    {
        var locationUserData = _saveService.LoadData<LocationUserDataJson[]>(SAVE_PATH);
        for (int i = 0; i < locationUserData.Length; i++)
        {
            if (locationUserData[i].name != User.Value.selectedLocation) continue;
            gainPointResourcesLocationScaleKoeff *= i + 1;
            break;
        }

        StartCoroutine(UpdateCurrentProgress());
    }


    private IEnumerator UpdateCurrentProgress()
    {
        for (;;)
        {
            currentProgress = _heroModule.GetHeroTransform().position.x * progressKoeff;
            yield return new WaitForSeconds(updateInterval);
        }
    }
}