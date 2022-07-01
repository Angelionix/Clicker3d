using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _mobCountOnFieldText;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _killedText;
    [SerializeField] private GameManaer _gm;

    public void UpdateMobCount()
    {
        _mobCountOnFieldText.text = _gm.MobsOnFieldCount.ToString();
    }

    public void UpdateScore()
    {
        _scoreText.text = _gm.Money.ToString();
    }

    public void UpdateKilled()
    {
        _killedText.text = _gm.Killed.ToString();
    }
}
