﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySettings : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    private int _currentHealth;
    [SerializeField, Range(0, 1f)] private float _chanceOfDrop;
    [SerializeField] private int _dropAmmount;

    [SerializeField] private GameObject _healthBar;
    [SerializeField] private Image _healthBarImage;

    private WaveSpawner _waveSpawner;
    private LineEnemyDetector _lineEnemyDetector;

    private ResourceCounter _resourceCounter;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _waveSpawner = WaveSpawner.Instance;
        _lineEnemyDetector = _waveSpawner.LineControllers[(int)(transform.position.z / 2)];
        _resourceCounter = ResourceCounter.Instance;
        _healthBar.SetActive(false);

    }

    public void ReceiveDamage(int damage)
    {
        if (_currentHealth == _maxHealth)
            _healthBar.SetActive(true);

        _currentHealth -= damage;
        _healthBarImage.fillAmount = (float)_currentHealth / (float)_maxHealth;

        if (_currentHealth < 1)
        {
            Destroy(this.gameObject);
            if (Random.value < _chanceOfDrop)
                _resourceCounter.ReceiveResources(_dropAmmount);
        }
    }

    private void OnDestroy()
    {
        if (_waveSpawner != null)
        {
            _lineEnemyDetector.EnemiesAlive--;
            int enemiesLeft = 0;
            enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
            if (enemiesLeft == 0)
                _waveSpawner.LaunchWave();
        }
    }
}
