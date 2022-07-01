using UnityEngine;

[System.Serializable]
public class PlayerRecord
{
    private string _name;
    private int _place;
    private int _score;

    public string Name
    {
        get 
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }
    public int Place
    {
        get
        {
            return _place;
        }
        set
        {
            _place = value;
        }
    }
    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
        }
    }
    public PlayerRecord()
    {
        _name = string.Empty;
        _place = 0;
        _score = 0;
    }
}
