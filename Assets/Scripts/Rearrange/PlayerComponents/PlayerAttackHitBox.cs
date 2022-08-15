using UnityEngine;

class PlayerAttackHitBox : MonoBehaviour, IPlayerComponent
{
    public int Damage { private set; get; }

    public PlayerModel PlayerModel { private set; get; }

    private void OnDamageChanged(int damage)
    {
        Damage = damage;
    }

    public void InitializeComponent(PlayerModel playerModel)
    {
        PlayerModel = playerModel;
        playerModel.OnDamageChanged += OnDamageChanged;
    }
}