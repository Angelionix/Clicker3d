using UnityEngine;

public abstract class Buster : MonoBehaviour
{
    public int _cost;
    public float _timeOfActing;

    public abstract void Activate();

}
