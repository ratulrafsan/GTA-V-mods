// catShooter V 0.4 
// UPDATE V 1.0 : https://github.com/ratulrafsan/GTA-V-mods/blob/master/AnimalShooter.cs
//                https://www.gta5-mods.com/scripts/cat-cannon

using System;
using System.Windows.Forms;
using GTA;
using GTA.Native;

public class catShooter : Script
{
    public catShooter()
    {
        Tick += onTick;
        KeyDown += onKeyDown;
        KeyUp += onKeyUp;
    }

    Ped player = Game.Player.Character;
    int counter = 0;
    bool bActivated = false;

    private void onTick(object sender, EventArgs e)
    {   
        if (counter > 300)
        {
            Function.Call(Hash.CLEAR_AREA, player.Position.X, player.Position.Y, player.Position.Z, 200 , false );
            counter = 0;
        }
        if (player.IsShooting && bActivated) shootCats();


    }
    private void onKeyDown(object sender, KeyEventArgs e)
    {

    }
    private void onKeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F12)
        {
            if (!bActivated)
            {
                bActivated = true;

                GTA.UI.Notify("Cat shooter activated");
            }
            else if (bActivated)
            {
                bActivated = false;

                GTA.UI.Notify("Cat Shooter deactivated");
            }
        }
    }

    public void shootCats()
    {   
        
        
        
        var pedCat = World.CreatePed(PedHash.Cat, Game.Player.Character.GetOffsetInWorldCoords(new GTA.Math.Vector3(0, 0.5f, 0)));
        
        Function.Call(Hash.SET_PED_TO_RAGDOLL, pedCat, 9000, 9000, 0, 1, 1);
        pedCat.Velocity.Equals(10);
        pedCat.ApplyForce(player.ForwardVector * 50);
        pedCat.MarkAsNoLongerNeeded();
        player.Weapons.Current.Ammo += 2;

        pedCat.Task.FleeFrom(Game.Player.Character.Position);

    }

}
using System;
using System.Windows.Forms;
using GTA;
using GTA.Native;

public class catShooter : Script
{
    public catShooter()
    {
        Tick += onTick;
        KeyDown += onKeyDown;
        KeyUp += onKeyUp;
    }

    Ped player = Game.Player.Character;
    int counter = 0;
    bool bActivated = false;

    private void onTick(object sender, EventArgs e)
    {   
        if (counter > 300)
        {
            Function.Call(Hash.CLEAR_AREA, player.Position.X, player.Position.Y, player.Position.Z, 200 , false );
            counter = 0;
        }
        if (player.IsShooting && bActivated) shootCats();


    }
    private void onKeyDown(object sender, KeyEventArgs e)
    {

    }
    private void onKeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F12)
        {
            if (!bActivated)
            {
                bActivated = true;

                GTA.UI.Notify("Cat shooter activated");
            }
            else if (bActivated)
            {
                bActivated = false;

                GTA.UI.Notify("Cat Shooter deactivated");
            }
        }
    }

    public void shootCats()
    {   
        
        
        
        var pedCat = World.CreatePed(PedHash.Cat, Game.Player.Character.GetOffsetInWorldCoords(new GTA.Math.Vector3(0, 0.5f, 0)));
        
        Function.Call(Hash.SET_PED_TO_RAGDOLL, pedCat, 9000, 9000, 0, 1, 1);
        pedCat.Velocity.Equals(10);
        pedCat.ApplyForce(player.ForwardVector * 50);
        pedCat.MarkAsNoLongerNeeded();
        player.Weapons.Current.Ammo += 2;

        pedCat.Task.FleeFrom(Game.Player.Character.Position);

    }

}
