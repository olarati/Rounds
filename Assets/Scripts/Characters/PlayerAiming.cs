using UnityEngine;

public class PlayerAiming : CharacterAiming
{
    private Camera _mainCamera;

    protected override void OnInit()
    {
        base.OnInit();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!IsActive)
        {
            return;
        }
        Aiming();
    }

    private void Aiming()
    {
        float characterZDelta = transform.position.z - _mainCamera.transform.position.z;
        Vector3 mouseInWorldPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * characterZDelta);
        Weapon.transform.LookAt(mouseInWorldPosition);
    }
}
