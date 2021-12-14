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
    public Player Player { private set; get; }
    public void BuildPlayer(int playerNumber)
    {
        Player = new Player(playerNumber, playerIcon, maxHealthPoints, maxEnergyPoints, healthPoints, energyPoints, movementSpeed, jumpForce, punchDamage, kickDamage);
        if (Player.Number == Player.PLAYER1_NUMBER)
        {
            Player.AtLeftSide = true;
            Player.AtRightSide = false;
        }
        if (Player.Number == Player.PLAYER2_NUMBER)
        {
            Player.AtLeftSide = false;
            Player.AtRightSide = true;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, -transform.localScale.z);
        }
        foreach (var playerComponent in GetComponents<IPlayerComponent>())
        {
            playerComponent.InitializeComponent(Player);
        }
        foreach (var playerComponent in GetComponentsInChildren<IPlayerComponent>())
        {
            playerComponent.InitializeComponent(Player);
        }
    }
    public void InitializeEnemyForPlayer(Transform enemyTransform)
    {
        GetComponent<PlayerStateMachineController>().InitializeEnemyForPlayer(enemyTransform);
    }
}
