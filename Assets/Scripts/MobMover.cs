using UnityEngine;

public class MobMover : MonoBehaviour
{
    [SerializeField] private Mob mob;
    [SerializeField] private float _movingSpeed;
    [SerializeField] private Vector3 _movingDirection;
    private Rigidbody _rb;

    private void OnEnable()
    {
        _movingDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
    }

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        MobMove();

    }

    private void MobMove()
    {
        _rb.velocity = _movingDirection * _movingSpeed;
    }
}
