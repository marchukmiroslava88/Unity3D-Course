using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExplosionComponent : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 600f;
    [SerializeField] private float _radius = 120f;
    [SerializeField] private float _timeToExplosion = 3f;
    [SerializeField] private Transform _explosionStartPoint;
    
    private float _timeRemaining;
    private bool _timerIsRunning;
    public TextMeshProUGUI TimeText;
    
    private void Explosion()
    {
        var colliders = Physics.SphereCastAll(_explosionStartPoint.position, _radius, _explosionStartPoint.up);

        foreach (var collider in colliders)
        {
            if (collider.collider.TryGetComponent<Rigidbody>(out var rigidbody))
            {
                rigidbody.AddExplosionForce(_explosionForce, _explosionStartPoint.position, _radius);
            }
        }
    }
    
    private void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);  
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        TimeText.text = $"{minutes:00}:{seconds:00}";
    }

    private void Start()
    {
        _timeRemaining = _timeToExplosion;
        DisplayTime(_timeRemaining);
    }

    private void Update()
    {
        if (!_timerIsRunning) return;
        if (_timeRemaining > 0)
        {
            DisplayTime(_timeRemaining);
            _timeRemaining -= Time.deltaTime;
        }
    }
    
    public void StartExplosionTimer()
    {
        _timerIsRunning = true;
        StartCoroutine(ExplosionTimer());
    }
    
    private IEnumerator ExplosionTimer()
    {
        yield return new WaitForSeconds(_timeToExplosion);
        _timerIsRunning = false;
        Explosion();
    }
}
