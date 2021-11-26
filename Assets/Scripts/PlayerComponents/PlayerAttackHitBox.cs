using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class PlayerAttackHitBox : MonoBehaviour, IPlayerComponent
{
    public int Damage {private set; get; }
    private Player player;
    private void OnDamageChanged(int obj)
    {
        Damage = obj;
    }
    private void OnTriggerEnter(Collider collision)
    {
        //if (collision.gameObject.GetComponent<StateMachineController>())
        //{
        //    collision.gameObject.GetComponent<StateMachineController>().GetPlayer().TakeDamage(Damage);
        //}
    }
    public Player GetPlayer()
    {
        return this.player;
    }
    public void InitializeComponent(Player player)
    {
        this.player = player;
        player.OnDamageChanged += OnDamageChanged;
    }

}
