using System.Collections.Generic;
using UnityEngine;

public class HitTypeBonusApplyer : MonoBehaviour, BonusApplyer
{
    [SerializeField] private GameObject _defaultHitPrefab;
    [SerializeField] private GameObject _explosionHitPrefab;

    public void ApplyBonus(List<BonusType> existingBonusTypes, GameObject root)
    {
        BulletHit hit = GetHit(existingBonusTypes);
        Apply(hit, root);

    }

    private BulletHit GetHit(List<BonusType> existingBonusTypes)
    {
        int finalHitType = 0;
        for (int i = 0; i < existingBonusTypes.Count; i++)
        {
            int hitType = 0;
            switch (existingBonusTypes[i])
            {
                case BonusType.SmallExplosionHit:
                    hitType = 1;
                    break;
                case BonusType.MediumExplosionHit:
                    hitType = 2;
                    break;
                case BonusType.LargeExplosionHit:
                    hitType = 3;
                    break;
            }

            // добавлено на случай, если в массиве бонусов будет несколько бонусов, и при этом в порядке не по возрастанию 
            if (finalHitType < hitType)
            {
                finalHitType = hitType;
            }
        }
        BulletHit hit = null;
        if (finalHitType == 0)
        {
            hit = new DefaultBulletHit(finalHitType, _defaultHitPrefab);
        }
        else
        {
            hit = new ExplosionBulletHit(finalHitType, _explosionHitPrefab);
        }
        return hit;
    }

    private void Apply(BulletHit hit, GameObject root)
    {
        IHitTypeBonusDependent[] dependents = root.GetComponentsInChildren<IHitTypeBonusDependent>();
        for (int i = 0; i < dependents.Length; i++)
        {
            dependents[i].SetHit(hit);
        }
    }
}
