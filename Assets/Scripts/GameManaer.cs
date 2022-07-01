using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManaer : MonoBehaviour
{
    [SerializeField] private int _totalKilledMobs;
    [SerializeField] private int _totalSpawnedMobs;
    [SerializeField] private int _mobsOnFieldCount;
    [SerializeField] private int _currentScore;
    [SerializeField] private List<GameObject> _mobsOnField;
    [SerializeField] private List<PlayerRecord> _records;
    [SerializeField] private string _name;
    [SerializeField] private SaveLoadData _sLData;

    [Header("DifficultySettings")]
    [SerializeField] private float _hpMultyConst;
    [SerializeField] private float _speedMultyConst;
    [SerializeField] private float _moneyMultyConst;
    private int _hpAddit = 1;
    private int _speedddit = 1;
    private int _moneyAddit = 1;


    [Header("OtherSettings")]
    [SerializeField] private float _freezeBuster;

    [Header("Events")]
    [SerializeField] private GameEvent _onLose;
    [SerializeField] private GameEvent _onMobCountChanged;
    [SerializeField] private GameEvent _onScoreChanged;

    #region Properties
    public int Killed
    {
        get
        {
            return _totalKilledMobs;
        }
    }
    public int Money
    {
        get
        {
            return _currentScore;
        }
    }
    public int MobsOnFieldCount
    {
        get 
        {
            return _mobsOnFieldCount;
        }
    }

    public float Freeze
    {
        get 
        {
            return _freezeBuster;
        }
    }
    #endregion

    void Awake()
    {
        _mobsOnField = new List<GameObject>();
        _records = new List<PlayerRecord>();
        InitizlizeGame();
        Time.timeScale = 0;
    }

    #region AddAndRemoveMobsOnField
    public void AddMobToFieldList(GameObject obj)
    {
        _mobsOnField.Add(obj);
        _mobsOnFieldCount = _mobsOnField.Count;
        _onMobCountChanged.EventAction(this.gameObject);
        _totalSpawnedMobs += 1;
        if (_mobsOnFieldCount >= 10)
        {
            _onLose.EventAction(this.gameObject);
            Time.timeScale = 0;
        }
    }

    public void RemoveMobToFieldList(GameObject obj)
    {
        _mobsOnField.Remove(obj);
        _mobsOnFieldCount = _mobsOnField.Count;
        _currentScore += obj.GetComponent<Mob>().Money;
        _totalKilledMobs += 1;
        _onMobCountChanged.EventAction(this.gameObject);
        _onScoreChanged.EventAction(this.gameObject);
    }

    public int[] DifficultyUP()
    {
        int[] args = new int[3] { _hpAddit, _moneyAddit, _speedddit};
        if (_totalSpawnedMobs <= 1)
        {
            return args;
        }
        else
        {
            
            args[0] = _hpAddit += (int)((_totalSpawnedMobs + (_totalSpawnedMobs - 1)) * _hpMultyConst);
            args[1] = _moneyAddit +=(int)((_totalSpawnedMobs + (_totalSpawnedMobs - 1)) * _moneyMultyConst);
            args[2] = _speedddit+=(int)((_totalSpawnedMobs + (_totalSpawnedMobs - 1)) * _speedMultyConst);
            return args;
        }
    }
    #endregion
    private void CheckThePosionOnRecords()
    {
        PlayerRecord temp = new PlayerRecord();
        foreach (var record in _records)
        {
            if (_currentScore >= record.Score)
            {
                temp = record;
                record.Name = _name;

            }
        }
    }

    private void InitizlizeGame()
    {
        foreach (var mob in _mobsOnField)
        {
            mob.SetActive(false);
        }
        _mobsOnField.Clear();
        _records.Clear();
        _mobsOnFieldCount = _mobsOnField.Count;
        _currentScore = 0;
        _totalSpawnedMobs = 0;
        _totalKilledMobs = 0;
        _onScoreChanged.EventAction(this.gameObject);
        _onMobCountChanged.EventAction(this.gameObject);
        for (int i = 0; i < 10; i++)
        {
            PlayerRecord player = new PlayerRecord();
            player.Place = i + 1;
            _records.Add(player);
        }
        
    }

    #region CommonApplicationMethods
    public void StartGame()
    {
        InitizlizeGame();
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ExitGame()
    {
        _sLData.SaveRecords(_records);
        Application.Quit();
    }

    public void Genocide()
    {
        int dm = 1000000;
        List<GameObject> temp = _mobsOnField;
        for (int i = 0; i < temp.Count; i++)
        {
            temp[i].GetComponent<Mob>().GetDamage(dm);
        }
    }
    #endregion


}
