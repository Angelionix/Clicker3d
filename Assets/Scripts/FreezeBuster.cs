using UnityEngine;

public class FreezeBuster :Buster
{
    [SerializeField] private int _cost;
    [SerializeField] private float _timeOfActivating;

    public override void Activate()
    {
        throw new System.NotImplementedException();
    }
}
