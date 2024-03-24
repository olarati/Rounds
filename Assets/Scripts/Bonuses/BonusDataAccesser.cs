using System.Collections.Generic;
using UnityEngine;

public class BonusDataAccesser : MonoBehaviour
{
    [SerializeField] private BonusData[] _data;

    public BonusData GetRandomBonus(List<BonusType> existingBonusTypes)
    {
        List<BonusData> possibleData = GetPossibleData(existingBonusTypes);
        float sumChance = GetSumChance(possibleData);
        float rand = Random.Range(0, sumChance);

        for (int i = 0; i < possibleData.Count; i++)
        {
            if (rand <= possibleData[i].Chance)
            {
                return possibleData[i];
            }
            rand -= possibleData[i].Chance;
        }
        return null;
    }

    private List<BonusData> GetPossibleData(List<BonusType> existingBonusTypes)
    {
        List<BonusData> possibleData = new List<BonusData>();
        for (int i = 0; i < _data.Length; i++)
        {
            if (!existingBonusTypes.Contains(_data[i].Type))
            {
                possibleData.Add(_data[i]);
            }
        }
        return possibleData;
    }

    private float GetSumChance(List<BonusData> data)
    {
        float sumChance = 0;
        for (int i = 0; i < data.Count; i++)
        {
            sumChance += data[i].Chance;
        }
        return sumChance;
    }
}
