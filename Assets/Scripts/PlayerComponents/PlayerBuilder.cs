using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


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
    private Player player;
    public void BuildPlayer(int playerNumber)
    {
        player = new Player(playerIcon, maxHealthPoints, maxEnergyPoints, healthPoints, energyPoints, movementSpeed, jumpForce, punchDamage, kickDamage);
        player.Number = playerNumber;
        if (player.Number == Player.PLAYER1_NUMBER)
        {
            player.AtLeftSide = true;
            player.AtRightSide = false;
        }
        if (player.Number == Player.PLAYER2_NUMBER)
        {
            player.AtLeftSide = false;
            player.AtRightSide = true;
        }
        foreach (var playerComponent in GetComponents<IPlayerComponent>())
        {
            playerComponent.InitializeComponent(player);
        }
        foreach (var playerComponent in GetComponentsInChildren<IPlayerComponent>())
        {
            playerComponent.InitializeComponent(player);
        }
    }
    public void InitializeEnemyForPlayer(Transform enemyTransform)
    {
        GetComponent<StateMachineController>().InitializeEnemyForPlayer(enemyTransform);
    }
    public Player GetPlayer()
    {
        return this.player;
    }

}
