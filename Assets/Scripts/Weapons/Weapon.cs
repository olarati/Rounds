using UnityEngine;
using System;
using Random = UnityEngine.Random;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _bulletDelay = 0.05f;
    [SerializeField] private int _bulletsInRow = 7;
    [SerializeField] private float _reloadingDuration = 4f;

    private Transform _bulletSpawnPoint;
    private int _currentBulletsInRow;
    private float _bulletTimer;
    private float _reloadingTimer;
    private bool _isShootDelayEnd;
    private bool _isReloading;

    public Action<int, int> OnBulletsInRowChange;
    public Action OnEndReloading;

    public bool IsReloading => _isReloading;

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
        SpawnBullet(_bulletPrefab, _bulletSpawnPoint);
    }

    private void SpawnBullet(Bullet prefab, Transform spawnPoint)
    {
        Bullet bullet = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        InitBullet(bullet);
    }

    private void InitBullet(Bullet bullet)
    {
        bullet.SetDamage(_damage);
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
}
