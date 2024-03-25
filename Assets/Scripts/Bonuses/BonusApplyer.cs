using System.Collections.Generic;
using UnityEngine;

public interface BonusApplyer
{
    void ApplyBonus(List<BonusType> existingBonusTypes, GameObject root);
}
