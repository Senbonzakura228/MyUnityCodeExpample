using System;
using UnityEngine;

[Serializable]
public struct StatUpgrade
{
    public Action ChangeNotify;
    [SerializeField] private int currentResearchedIndex;
    [SerializeField] private int startCost;
    [SerializeField] private int costIncrement;
    [SerializeField] private int baseValue;
    [SerializeField] private int levelValueIncrement;

    public int CurrentResearchedIndex
    {
        get => currentResearchedIndex;
        set => currentResearchedIndex = value;
    }

    public int GetCurrentCost()
    {
        return startCost + (currentResearchedIndex * costIncrement);
    }

    public int GetCurrentValue()
    {
        return baseValue + (currentResearchedIndex * levelValueIncrement);
    }

    public void SetCurrentResearchedIndex(int index)
    {
        currentResearchedIndex = index;
        ChangeNotify?.Invoke();
    }
}