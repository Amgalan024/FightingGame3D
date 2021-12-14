using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IPlayerComponent
{
    Player Player { set; get; }
    void InitializeComponent(Player player);
}
