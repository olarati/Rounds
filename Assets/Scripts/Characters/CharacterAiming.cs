using UnityEngine;

public abstract class CharacterAiming : CharacterPart
{
    private Weapon _weapon;

    protected Weapon Weapon => _weapon;

    protected override void OnInit()
    {
        _weapon = GetComponentInChildren<Weapon>();
    }

}
