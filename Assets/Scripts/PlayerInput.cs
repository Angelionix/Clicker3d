using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Vector2 _mouseScreenPosition;
    [SerializeField] private Camera _cam;
    [SerializeField] private int _damage;

    void Start()
    {
        _cam = GetComponent<Camera>();
    }

    void Update()
    {
        _mouseScreenPosition = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
            {
                OnMouseClick();
            }
    }

    private void OnMouseClick()
    {
        RaycastHit hit;
        Ray ray = _cam.ScreenPointToRay(_mouseScreenPosition);
        if (Physics.Raycast(ray, out  hit, Mathf.Infinity)&&Time.timeScale>=1)
        {
            IDamageAble<int> mob;
            if (hit.collider.gameObject.TryGetComponent<IDamageAble<int>>(out mob))
            {
                mob.GetDamage(_damage);
            }
        }

    }
    private void OnMouseDownDrag()
    {

    }

}
