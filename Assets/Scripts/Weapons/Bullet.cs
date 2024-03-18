using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject _objectHitPrefab;
    [SerializeField] private float _impulse = 30f;
    [SerializeField] private float _lifeTime = 15f;

    private int _damage;

    public void SetDamage(int value)
    {
        _damage = value;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.forward * _impulse, ForceMode.Impulse);
    }

    private void Update()
    {
        ReduceLifeTime();
    }

    private void ReduceLifeTime()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0)
        {
            DestroyBullet();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider)
        {
            Hit(collision);
        }
    }

    private void Hit(Collision collision)
    {
        bool isCharacterHit = CheckCharacterHit(collision);
        CheckPhysicObjectHit(collision);
        if (!isCharacterHit)
        {
            GameObject hitSample = Instantiate(_objectHitPrefab, collision.contacts[0].point, Quaternion.LookRotation(-transform.up, -transform.forward));
        }
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private bool CheckCharacterHit(Collision collision)
    {
        CharacterHealth hitedHealth = collision.collider.GetComponentInParent<CharacterHealth>();
        if (hitedHealth)
        {
            hitedHealth.AddHealthPoints(-_damage);
            return true;
        }
        return false;
    }

    private bool CheckPhysicObjectHit(Collision collision)
    {
        IPhysicHittable hittedPhysicObject = collision.collider.GetComponentInParent<IPhysicHittable>();
        if (hittedPhysicObject != null)
        {
            hittedPhysicObject.Hit(transform.forward * _impulse, collision.contacts[0].point);
            return true;
        }
        return false;
    }

}
