using UnityEngine;
using System;

public abstract class Weapon : MonoBehaviour, IShootCountBonusDependent, IHitTypeBonusDependent
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _bulletDelay = 0.05f;
    [SerializeField] private int _bulletsInRow = 7;
    [SerializeField] private float _reloadingDuration = 4f;

    private Transform _bulletSpawnPoint;
    private BulletHit _bulletHit;
    private int _currentBulletsInRow;
    private float _bulletTimer;
    private float _reloadingTimer;
    private bool _isShootDelayEnd;
    private bool _isReloading;
    private int _shootCount;

    public Action<int, int> OnBulletsInRowChange;
    public Action OnEndReloading;

    public bool IsReloading => _isReloading;

    public void SetShootCount(int value)
    {
        _shootCount = value;
    }

    public void SetHit(BulletHit hit)
    {
        _bulletHit = hit;
    }

    public void Init()
    {
        _bulletSpawnPoint = GetComponentInChildren<BulletSpawnPoint>().transform;
        FillBulletsToRow();
    }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
        OnBulletsInRowChange?.Invoke(_currentBulletsInRow, _bulletsInRow);
    }

    public void Shoot()
    {
        if (!_isShootDelayEnd || !CheckHasBulletsInRow())
        {
            return;
        }
        _bulletTimer = 0;
        DoShoot();
        _currentBulletsInRow--;
        OnBulletsInRowChange?.Invoke(_currentBulletsInRow, _bulletsInRow);
    }

    public void Reload()
    {
        if (_isReloading)
        {
            return;
        }
        _isReloading = true;
    }

    public bool CheckHasBulletsInRow()
    {
        return _currentBulletsInRow > 0;
    }

    protected void DoShoot()
    {
        for (int i = 0; i < _shootCount; i++)
        {
            SpawnBullet(_bulletPrefab, _bulletSpawnPoint, GetShootAngle(i, _shootCount));
        }
    }

    private void SpawnBullet(Bullet prefab, Transform spawnPoint, float extraAngle)
    {
        Bullet bullet = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

        Vector3 bulletEulerAngles = bullet.transform.eulerAngles;
        bulletEulerAngles.x += extraAngle;
        bullet.transform.eulerAngles = bulletEulerAngles;

        bullet.Init(_damage, _bulletHit);
    }

    private void Update()
    {
        ShootDelaying();
        Reloading();
    }

    private void ShootDelaying()
    {
        _bulletTimer += Time.deltaTime;
        _isShootDelayEnd = _bulletTimer >= _bulletDelay;
    }

    private void Reloading()
    {
        if (_isReloading)
        {
            _reloadingTimer += Time.deltaTime;
            if (_reloadingTimer >= _reloadingDuration)
            {
                FillBulletsToRow();
                OnEndReloading?.Invoke();
            }
        }
    }

    private void FillBulletsToRow()
    {
        _isReloading = false;
        _reloadingTimer = 0;
        _currentBulletsInRow = _bulletsInRow;
        OnBulletsInRowChange?.Invoke(_currentBulletsInRow, _bulletsInRow);
    }

    private float GetShootAngle(int shootId, int shootCount)
    {
        float startAngle = 0;
        float stepAngle = 0;
        switch (shootCount)
        {
            case 2:
                startAngle = -3;
                stepAngle = 6;
                break;
            case 3:
                startAngle = -5;
                stepAngle = 5;
                break;
            case 4:
                startAngle = -6;
                stepAngle = 4;
                break;
            default:
                return 0;
        }
        return startAngle + stepAngle * shootId;
    }
}
