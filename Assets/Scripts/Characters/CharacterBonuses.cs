using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBonuses : CharacterPart
{
    [SerializeField] private List<BonusType> _existingBonusTypes;

    private BonusApplyer[] _bonusApplyers = new BonusApplyer[]
    {
        new ShootCountBonusApplyer()
    };

    public void AddBonus(BonusType type)
    {
        _existingBonusTypes.Add(type);
    }

    protected override void OnInit()
    {
        for (int i = 0; i < _bonusApplyers.Length; i++)
        {
            _bonusApplyers[i].ApplyBonus(_existingBonusTypes, gameObject);
        }
    }
}
