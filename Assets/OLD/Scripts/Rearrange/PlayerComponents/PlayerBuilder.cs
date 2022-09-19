using UnityEngine;

public class PlayerBuilder : MonoBehaviour
{
    [SerializeField] private Sprite playerIcon;
    [SerializeField] private int maxHealthPoints;
    [SerializeField] private int maxEnergyPoints;
    [SerializeField] private int healthPoints;
    [SerializeField] private int energyPoints;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private int punchDamage;
    [SerializeField] private int kickDamage;
    public PlayerModel PlayerModel { private set; get; }

    public void BuildPlayer(int playerNumber)
    {
        PlayerModel = new PlayerModel(playerNumber, playerIcon, maxHealthPoints, maxEnergyPoints, healthPoints,
            energyPoints, movementSpeed, jumpForce, punchDamage, kickDamage);

        if (PlayerModel.Number == PlayerModel.PLAYER1_NUMBER)
        {
            PlayerModel.AtLeftSide = true;
        }

        if (PlayerModel.Number == PlayerModel.PLAYER2_NUMBER)
        {
            PlayerModel.AtLeftSide = false;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -transform.localScale.z);
        }

        foreach (var playerComponent in GetComponents<IPlayerComponent>())
        {
            playerComponent.InitializeComponent(PlayerModel);
        }

        foreach (var playerComponent in GetComponentsInChildren<IPlayerComponent>())
        {
            playerComponent.InitializeComponent(PlayerModel);
        }
    }

    public void InitializeEnemyForPlayer(Transform enemyTransform)
    {
        GetComponent<PlayerStateMachineController>().InitializeEnemyForPlayer(enemyTransform);
    }
}