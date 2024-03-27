
using UnityEngine;

public class ExplosionBulletHit : BulletHit
{
    private const float HitRange = 1f;

    public ExplosionBulletHit(int finalHitType, GameObject hitPrefab) : base(finalHitType, hitPrefab) { }

    public override void Hit(Collision collision, Transform bulletTransform)
    {
        Collider[] collidersInRange = Physics.OverlapSphere(collision.contacts[0].point, HitRange * FinalHitType);
        CheckCharacterHit(collidersInRange);
        CheckPhysicObjectHit(collidersInRange, bulletTransform, collision.contacts[0].point);

        GameObject hitSample = GameObject.Instantiate(HitPrefab, collision.contacts[0].point, Quaternion.identity);
        hitSample.transform.localScale = Vector3.one * FinalHitType;
    }

    protected bool CheckCharacterHit(Collider[] colliders)
    {
        bool isHit = false;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (CheckCharacterHit(colliders[i]))
            {
                isHit = true;
            }
        }
        return isHit;
    }


    private bool CheckPhysicObjectHit(Collider[] colliders, Transform bulletTransform, Vector3 point)
    {
        bool isHit = false;
        for (int i = 0; i < colliders.Length; i++)
        {
            Vector3 direction = (colliders[i].ClosestPoint(point) - point).normalized;
            if (CheckPhysicObjectHit(colliders[i], direction, point))
            {
                isHit = true;
            }
        }
        return isHit;
    }
}
