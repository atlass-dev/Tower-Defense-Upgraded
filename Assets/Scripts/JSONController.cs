﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONController : MonoBehaviour
{
    private Stats _playerStats;
    public Stats PlayerStats { get { return _playerStats; }
        set { _playerStats = value; }
    }

    private void Awake()
    {
        GetStats();  
        
        if (_playerStats.TurretLevel == 0)
        {
            _playerStats.TurretLevel = 1;
            _playerStats.MinerLevel = 1;

            SaveStats();
        }
    }

    public void GetStats()
    {
        _playerStats = JsonUtility.FromJson<Stats>(
            File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "PlayerStats.json")));
    }

    public void SaveStats()
    {
        File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "PlayerStats.json"), 
            JsonUtility.ToJson(_playerStats));
    }
    
}

[System.Serializable]
public class Stats
{
    [SerializeField] private int _unlockedLevelsCount;
    public int UnlockedLevelsCount 
    { get { return _unlockedLevelsCount; }
      set { _unlockedLevelsCount = value; }
    }
    [SerializeField] private int _coinsAmmount;
    public int CoinsAmmount
    {
        get { return _coinsAmmount; }
        set { _coinsAmmount = value; }
    }

    [SerializeField] private int _turretLevel;

    public int TurretLevel
    {
        get { return _turretLevel; }
        set { _turretLevel = value; }
    }

    [SerializeField] private int _minerLevel;

    public int MinerLevel
    {
        get { return _minerLevel; }
        set { _minerLevel = value; }
    }
}

