using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class PlayerAttackHitBox : MonoBehaviour, IPlayerComponent
{
    public int Damage { private set; get; }

    public Player Player { set; get; }
    private void OnDamageChanged(int damage)
    {
        Damage = damage;
    }
    public void InitializeComponent(Player player)
    {
        this.Player = player;
        player.OnDamageChanged += OnDamageChanged;
    }

}
