using UnityEngine;
using UnityEngine.Events;


public class Mob : MonoBehaviour, IDamageAble<int>
{
    [SerializeField] private string _name;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currHealth;
    [SerializeField] private int _dropMoney;
    [SerializeField] private float _speed;
    [SerializeField] private int _type;

    [Header("Events")]
    [SerializeField] private GameEvent _onMobDeath;
    [SerializeField] private GameEvent _onMobHit;


    public int Type
    {
        get 
        {
            return _type;
        }
    }

    public int HP
    {
        set 
        {
            _maxHealth = value;
        }
        get
        {
            return _maxHealth;
        }
    }

    public float HPPercent
    {
        get 
        {
            return (float)_currHealth / _maxHealth;
        }
    }

    public int Money
    {
        set
        {
            _dropMoney = value;
        }
        get
        {
            return _dropMoney;
        }
    }

    public float Speed
    {
        get 
        {
            return _speed;
        }
        set
        {
            _speed = value;
        }
    }

    private void OnEnable()
    {
        _currHealth = _maxHealth;
    }

    public void InitMob(int hp, int dropMoney, float speed)
    {
        _maxHealth = hp;
        _dropMoney = dropMoney;
        _speed = speed;
    }

    public void GetDamage(int damage)
    {
        _currHealth -= damage;
        _onMobHit.EventAction(this.gameObject);
        if (_currHealth <= 0)
        {
            _onMobDeath.EventAction(this.gameObject);
        }
    }
}