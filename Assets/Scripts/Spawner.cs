using System.Collections.Generic;
using System.Collections;
using UnityEngine;


public class Spawner : MonoBehaviour
{

    [Header("Spawn Properties")]
    [SerializeField] private float _spawnMaxDealy;
    [SerializeField] private float _spawnMinDelay;
    [SerializeField] private Vector3 _spawnCoordinates;
    [SerializeField] private Vector3 _minBoundary;
    [SerializeField] private Vector3 _maxBoundary;

    [Header("Pool Properties")]
    [SerializeField] private GameObject _mob;
    [SerializeField] private List<PoolItem> _mobs;
    [SerializeField] private List<GameObject> _pooledMobs;

    [Header("Events")]
    [SerializeField] private GameEvent _onMobSpawned;

    private Color _boundariesColor;
    [SerializeField] private bool _isSpawnFreeze;
    [SerializeField] private GameManaer _gm;

    [SerializeField]private float _currentDelay;
    [SerializeField] private float _freezeTime;

    void Start()
    {
        _boundariesColor = Color.red;
        _currentDelay = Random.Range(_spawnMinDelay, _spawnMaxDealy);
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        DrawBoundaries();
        if (!_isSpawnFreeze)
        {
            if (_currentDelay <= 0)
            {
                _currentDelay = Random.Range(_spawnMinDelay, _spawnMaxDealy);
                _spawnCoordinates = new Vector3(Random.Range(_minBoundary.x, _maxBoundary.x), 1, Random.Range(_minBoundary.z, _maxBoundary.z));
                GetFromPool(_mobs[Random.Range(0, _mobs.Count)], _spawnCoordinates);
            }
            else
            {
                _currentDelay -= Time.deltaTime;
            }
        }
        else
        {
            _freezeTime -= Time.deltaTime;
            if (_freezeTime <= 0)
            {
                _isSpawnFreeze = false;
            }
        }
    }

    private void Initialize()
    {
        _pooledMobs = new List<GameObject>();
        foreach (PoolItem mob in _mobs)
        {
            for (int i = 0; i < mob.ammount; i++)
            {
                GameObject obj = Instantiate(mob.prefab);
                obj.transform.parent = this.transform;
                obj.SetActive(false);
                _pooledMobs.Add(obj);
            }
        }
    }

    private void GetFromPool(PoolItem mob, Vector3 _spawnPoint)
    {
        for (int i = 0; i < _pooledMobs.Count; i++)
        {

            if (!_pooledMobs[i].activeInHierarchy && _pooledMobs[i].GetComponent<Mob>().Type == mob.type)
            {
                Mob currMob = _pooledMobs[i].GetComponent<Mob>();
                int[] multyArray = _gm.DifficultyUP();
                _pooledMobs[i].transform.position = _spawnCoordinates;               
                int hp = (int)(currMob.HP + multyArray[0]);
                int dropMoney = (int)(currMob.Money+multyArray[1]);
                float speed = (int)(currMob.Speed+multyArray[2]);
                _pooledMobs[i].GetComponent<Mob>().InitMob(hp, dropMoney, speed);
                _pooledMobs[i].SetActive(true);
                _onMobSpawned.EventAction(_pooledMobs[i]);
                break;
            }
        }
    }

    public void ReturnToPool(GameObject mob)
    {
        mob.SetActive(false);
        mob.transform.position = this.transform.position;
    }

    public void FreezeSpawn()
    {
        _isSpawnFreeze = true;
        _freezeTime = _gm.Freeze;
    }

    private void DrawBoundaries()
    {
        Debug.DrawLine(_minBoundary, new Vector3(_minBoundary.x,_minBoundary.y,_maxBoundary.z),_boundariesColor);
        Debug.DrawLine(new Vector3(_minBoundary.x, _minBoundary.y, _maxBoundary.z), _maxBoundary, _boundariesColor);
        Debug.DrawLine(_maxBoundary, new Vector3(_maxBoundary.x, _minBoundary.y, _minBoundary.z), _boundariesColor);
        Debug.DrawLine(new Vector3(_maxBoundary.x, _minBoundary.y, _minBoundary.z), _minBoundary, _boundariesColor);
    }
}
