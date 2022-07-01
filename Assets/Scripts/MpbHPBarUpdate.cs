using UnityEngine;
using UnityEngine.UI;

public class MpbHPBarUpdate : MonoBehaviour
{
    [SerializeField] private Image _hpBarBackground;
    [SerializeField] private Image _hpBar;
    [SerializeField] private float _procent;
    [SerializeField] private Mob _mob;

    private void OnEnable()
    {
        _hpBar.fillAmount = 1;

    }
    public void HPBarUpdate(GameObject go)
    {
        _procent = _mob.HPPercent;
        _hpBar.fillAmount = _procent;
    }
}
