using System;
using UnityEngine;

public abstract class CharacterHealth : CharacterPart
{
    [SerializeField] private int _startHealthPoints = 100;

    private int _healthPoints;
    private bool _isDead;

    public Action OnDie;
    public Action<CharacterHealth> OnDieWithObject;
    public Action OnAddHealthPoints;

    public void AddHealthPoints(int value)
    {
        if (_isDead)
        {
            return;
        }

        _healthPoints += value;
        Mathf.Clamp(_healthPoints, 0, _startHealthPoints);
        OnAddHealthPoints?.Invoke();

        if (_healthPoints <= 0)
        {
            Die();
        }
    }

    public int GetStartHealthPoints()
    {
        return _startHealthPoints;
    }
    public int GetHealthPoints()
    {
        return _healthPoints;
    }

    protected override void OnInit()
    {
        _healthPoints = _startHealthPoints;
        _isDead = false;
    }

    private void Die()
    {
        _isDead = true;
        OnDie?.Invoke();
        OnDieWithObject?.Invoke(this);
    }


}
