using UnityEngine;

public class MobAI : MonoBehaviour
{
    [SerializeField] Vector3 _direction;
    [SerializeField] private float _maxTimeToChangeDir;
    [SerializeField] private float _timeTochangeDir;

    // Start is called before the first frame update
    private void OnEnable()
    {
        _timeTochangeDir = _maxTimeToChangeDir;
        _direction = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
