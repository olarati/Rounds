using System.Collections.Generic;
using UnityEngine;

public abstract class BonusApplyer
{
    public abstract void ApplyBonus(List<BonusType> existingBonusTypes, GameObject root);
}
