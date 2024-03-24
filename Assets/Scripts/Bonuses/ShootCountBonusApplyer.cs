using System.Collections.Generic;
using UnityEngine;

public class ShootCountBonusApplyer : BonusApplyer
{
    public override void ApplyBonus(List<BonusType> existingBonusTypes, GameObject root)
    {
        int finalShootCount = 1;
        for (int i = 0; i < existingBonusTypes.Count; i++)
        {
            int shootCount = 1;
            switch (existingBonusTypes[i])
            {
                case BonusType.DoubleShoot:
                    shootCount = 2;
                    break;
                case BonusType.TripleShoot:
                    shootCount = 3;
                    break;
                case BonusType.QuadrupleShoot:
                    shootCount = 4;
                    break;
            }

            // добавлено на случай, если в массиве бонусов будет несколько бонусов, и при этом в порядке не по возрастанию 
            if(finalShootCount < shootCount)
            {
                finalShootCount = shootCount;
            }
        }

        IShootCountBonusDependent dependent = root.GetComponentInChildren<IShootCountBonusDependent>();
        dependent.SetShootCount(finalShootCount);
    }
}
