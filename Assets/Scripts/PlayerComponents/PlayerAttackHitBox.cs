using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class PlayerAttackHitBox : MonoBehaviour, IPlayerComponent
{
    [SerializeField] private int damage;
    private Player player;
    private void OnDamageChanged(int obj)
    {
        damage = obj;
    }
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.GetComponent<PlayerBuilder>())
        {
            collision.gameObject.GetComponent<PlayerBuilder>().GetPlayer().TakeDamage(damage);
        }
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
