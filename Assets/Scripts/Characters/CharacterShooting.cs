using UnityEngine;

public abstract class CharacterShooting : CharacterPart
{

    private Weapon _weapon;

    protected abstract void Shooting();
    protected abstract void Reloading();

    protected override void OnInit()
    {
        _weapon = GetComponentInChildren<Weapon>();
        _weapon.Init();
    }

    protected void Shoot()
    {
        _weapon.Shoot();
    }

    protected bool CheckHasBulletsInRow()
    {
        return _weapon.CheckHasBulletsInRow();
    }

    protected void Reload()
    {
        _weapon.Reload();
    }

    private void Update()
    {
        if (!IsActive)
        {
            return;
        }
        Shooting();
        Reloading();
    }

}
