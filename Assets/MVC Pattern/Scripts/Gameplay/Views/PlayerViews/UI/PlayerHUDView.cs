using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDView : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image _icon;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private Slider _energyBar;
    [SerializeField] private Text _score;

    public Slider HealthBar => _healthBar;
    public Slider EnergyBar => _energyBar;

    public void SetCamera(Camera sceneCamera)
    {
        _canvas.worldCamera = sceneCamera;
    }

    public void SetIcon(Sprite iconSprite)
    {
        _icon.sprite = iconSprite;
    }

    public void SetScore(int score)
    {
        _score.text = score.ToString();
    }
}