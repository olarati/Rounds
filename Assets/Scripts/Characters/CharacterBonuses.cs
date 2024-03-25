using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBonuses : CharacterPart
{
    [SerializeField] private List<BonusType> _existingBonusTypes;

    private List<BonusApplyer> _bonusApplyers = new List<BonusApplyer>()
    {
        new ShootCountBonusApplyer(),
    };

    public void AddBonus(BonusType type)
    {
        _existingBonusTypes.Add(type);
    }

    protected override void OnInit()
    {
        _bonusApplyers.AddRange(GetComponentsInChildren<BonusApplyer>());
        for (int i = 0; i < _bonusApplyers.Count; i++)
        {
            _bonusApplyers[i].ApplyBonus(_existingBonusTypes, gameObject);
        }
    }
}
