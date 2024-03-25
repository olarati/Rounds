using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] private float _impulse = 30f;
    [SerializeField] private float _lifeTime = 15f;

    private BulletHit _hit;

    public void Init(int damage, BulletHit hit)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.forward * _impulse, ForceMode.Impulse);

        _hit = hit;
        hit.Init(damage, _impulse);
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
            _hit.Hit(collision, transform);
            DestroyBullet();
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

}
