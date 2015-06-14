//Author : Rafsan Ratul
//Animal cannon V1.1 mod for GTA V , lets you shoot with animals! lol

//There are obviously better way to do this...but meh :v It works!

using System;
using System.Windows.Forms;
using System.Collections.Generic;
using GTA;
using GTA.Math;
using GTA.Native;
using Menu = GTA.Menu;


public class AnimalShooter : Script
{

    public AnimalShooter()
    {
        Tick += onTick;
        KeyDown += onKeyDown;
        KeyUp += onKeyUp;
    }


    GTA.Native.PedHash[] zoo = { PedHash.Cat  , PedHash.Cow      , PedHash.Crow     , PedHash.Chop       , PedHash.MountainLion,      
                                 PedHash.Chimp, PedHash.Husky    , PedHash.Coyote   , PedHash.TigerShark , PedHash.Deer        ,
                                 PedHash.Rat  , PedHash.Pig      , PedHash.Boar     , PedHash.Rhesus     , PedHash.HammerShark ,
                                 PedHash.Hen  , PedHash.Dolphin  , PedHash.Humpback , PedHash.Seagull    , PedHash.ChickenHawk ,
                                 PedHash.Fish , PedHash.Retriever, PedHash.Cormorant, PedHash.KillerWhale, PedHash.Shepherd    ,
                                 PedHash.Pigeon };

    Ped player = Game.Player.Character;
    Random rnd = new Random();
    GTA.Native.PedHash selectedped;
    int counter = 0;
    bool bActivated = false;
    bool bCocktail = false;




    private void onTick(object sender, EventArgs e)
    {   
        if (counter > 300)
        {
            Function.Call(Hash.CLEAR_AREA, player.Position.X, player.Position.Y, player.Position.Z, 6000, false);
            counter = 0;
           
        }

        //if (player.IsShooting && bActivated) shootAnimals();
        if (player.IsShooting && bActivated) shootAnimals2();


    }

    private void onKeyDown(object sender, KeyEventArgs e)
    {

    }

    private void onKeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.F12)
        {
            if (this.View.ActiveMenus == 0)
            {   
                player.IsInvincible = true;
                this.MainMenu();
            }
            else
            {   
                this.View.CloseAllMenus();
            }
        }
    }

    void toggler()
    {

        if (!bActivated)
        {
            bActivated = true;
            GTA.UI.Notify("Animal Cannon Activated!");
        }
        else if (bActivated)
        {
            bActivated = false;
            GTA.UI.Notify("Animal Cannon Deactivated!");
        }
    }

    void MainMenu()
    {
        var menuItems = new List<IMenuItem>();

        var button = new MenuButton("Toggle Animal Cannon", "Toggles Animal Cannon mod");
        button.Activated += (sender, args) => this.toggler();
        menuItems.Add(button);

        button = new MenuButton("Cat Ammo", "Switch to cat ammo type");
        button.Activated += (sender, args) => this.catAmmo();
        menuItems.Add(button);

        button = new MenuButton("Retriever", "Switch to retriever ammo type");
        button.Activated += (sender, args) => this.retrieverAmmo();
        menuItems.Add(button);

        button = new MenuButton("Chop", "Switch to Chop(Dog) ammo type");
        button.Activated += (sender, args) => this.chopAmmo();
        menuItems.Add(button);

        button = new MenuButton("Shepard", "Switch to shepard ammo type");
        button.Activated += (sender, args) => this.shepardAmmo();
        menuItems.Add(button);

        button = new MenuButton("Huskey", "Switch to huskey ammo type");
        button.Activated += (sender, args) => this.huskeyAmmo();
        menuItems.Add(button);

        button = new MenuButton("Coyote", "Switch to coyote ammo type");
        button.Activated += (sender, args) => this.coyoteAmmo();
        menuItems.Add(button);

        button = new MenuButton("Cow", "Switch to cow ammo");
        button.Activated += (sender, args) => this.cowAmmo();
        menuItems.Add(button);

        button = new MenuButton("Rhesus", "Switch to rhesus ammo type");
        button.Activated += (sender, args) => this.rhesusAmmo();
        menuItems.Add(button);

        button = new MenuButton("Cormorant", "Switch  to cormorant ammo type");
        button.Activated += (sender, args) => this.cormorantAmmo();
        menuItems.Add(button);

        button = new MenuButton("Mountain Lion", "Switch to mountain lion ammo type");
        button.Activated += (sender, args) => this.mtlionAmmo();
        menuItems.Add(button);

        button = new MenuButton("Deer", "Switch to deer ammo type");
        button.Activated += (sender, args) => this.deerAmmo();
        menuItems.Add(button);

        button = new MenuButton("Rat", "Switch to rat ammo type");
        button.Activated += (sender, args) => this.ratAmmo();
        menuItems.Add(button);

        button = new MenuButton("Pig ", "Switch to pig ammo type");
        button.Activated += (sender, args) => this.pigAmmo();
        menuItems.Add(button);

        button = new MenuButton("Boar", "Switch to boar ammo type");
        button.Activated += (sender, args) => this.boarAmmo();
        menuItems.Add(button);

        button = new MenuButton("Crow", "Switch to crow ammo");
        button.Activated += (sender, args) => this.crowAmmo();
        menuItems.Add(button);

        button = new MenuButton("Hen", "Switch to hen ammo type");
        button.Activated += (sender, args) => this.henAmmo();
        menuItems.Add(button);

        button = new MenuButton("ChickenHawk", "Switch to chicken-hawk ammo type");
        button.Activated += (sender, args) => this.chickenhawkAmmo();
        menuItems.Add(button);

        button = new MenuButton("Pigeon", "Switch to pigeon ammo type");
        button.Activated += (sender, args) => this.pigeonAmmo();
        menuItems.Add(button);

        button = new MenuButton("Seagull", "Switch to seagull ammo type");
        button.Activated += (sender, args) => this.seagullAmmo();
        menuItems.Add(button);

        button = new MenuButton("Fish", "Switch to fish ammo type");
        button.Activated += (sender, args) => this.fishAmmo();
        menuItems.Add(button);

        button = new MenuButton("Tiger Shark", "Switch to tiger shark ammo type");
        button.Activated += (sender, args) => this.tsAmmo();
        menuItems.Add(button);

        button = new MenuButton("Hammer Shark", "Switch to hammer-shark ammo type");
        button.Activated += (sender, args) => this.hsAmmo();
        menuItems.Add(button);

        button = new MenuButton("Humpback Whale", "Switch to humpback ammo type");
        button.Activated += (sender, args) => this.humpbackAmmo();
        menuItems.Add(button);

        button = new MenuButton("Killer Whale", "Switch to killer-whale ammo type");
        button.Activated += (sender, args) => this.killerwhaleAmmo();
        menuItems.Add(button);

        button = new MenuButton("TOGGLE COCKTAIL", "YOU NEVER KNOW!");
        button.Activated += (sender, args) => this.cocktailAmmo();
        menuItems.Add(button);


        Menu AmmoMenu = new Menu("Animal Cannon", menuItems.ToArray());
        AmmoMenu.ItemHeight = 20;
        AmmoMenu.Width = 300;
        AmmoMenu.FooterCentered = true;

        this.View.AddMenu(AmmoMenu);
        
    }

    void catAmmo()
    {
        selectedped = zoo[0];
    }
    void cowAmmo()
    {
        selectedped = zoo[1];
    }
    void crowAmmo()
    {
        selectedped = zoo[2];
    }
    void chopAmmo()
    {
        selectedped = zoo[3];
    }
    void mtlionAmmo()
    {
        selectedped = zoo[4];
    }
    void chimpAmmo()
    {
        selectedped = zoo[5];
    }
    void huskeyAmmo()
    {
        selectedped = zoo[6];
    }
    void coyoteAmmo()
    {
        selectedped = zoo[7];
    }
    void tsAmmo()
    {
        selectedped = zoo[8];
    }
    void deerAmmo()
    {
        selectedped = zoo[9];
    }
    void ratAmmo()
    {
        selectedped = zoo[10];
    }
    void pigAmmo()
    {
        selectedped = zoo[11];
    }
    void boarAmmo()
    {
        selectedped = zoo[12];
    }
    void rhesusAmmo()
    {
        selectedped = zoo[13];
    }
    void hsAmmo()
    {
        selectedped = zoo[14];
    }
    void henAmmo()
    {
        selectedped = zoo[15];
    }
    void dolphinAmmo()
    {
        selectedped = zoo[16];
    }
    void humpbackAmmo()
    {
        selectedped = zoo[17];
    }
    void seagullAmmo()
    {
        selectedped = zoo[18];
    }
    void chickenhawkAmmo()
    {
        selectedped = zoo[19];
    }
    void fishAmmo()
    {
        selectedped = zoo[20];
    }
    void retrieverAmmo()
    {
        selectedped = zoo[21];
    }
    void cormorantAmmo()
    {
        selectedped = zoo[22];
    }
    void killerwhaleAmmo()
    {
        selectedped = zoo[23];
    }
    void shepardAmmo()
    {
        selectedped = zoo[24];
    }
    void pigeonAmmo()
    {
        selectedped = zoo[25];
    }
    void cocktailAmmo()
    {
        if (!bCocktail)
        {
            bCocktail = true;
        }
        else
        {
            bCocktail = false;
        }
    }


    void shootAnimals2()
    {
        
        Ped pedAnimal = (bCocktail) ? World.CreatePed(zoo[rnd.Next(0, 25)], GetCoordsFromCam(5)) : World.CreatePed(selectedped, GetCoordsFromCam(5) );

        Vector3 direction = GetDirectionFromCam();
        float dx = 0;
        float dy = 100 + (Math.Abs(direction.Y))  ;
        float dz = direction.Z * 130;
        Function.Call(Hash.SET_ENTITY_HEADING, pedAnimal , player.Heading );
        Function.Call(Hash.APPLY_FORCE_TO_ENTITY , pedAnimal ,1 , dx , dy , dz , 0 , 0 , 0 , false , true,  true , true , false , true);
        
        //Function.Call(GTA.Native.Hash.SET_ENTITY_HEALTH, pedAnimal, 0);
        pedAnimal.Task.FleeFrom(Game.Player.Character.Position);
        pedAnimal.MarkAsNoLongerNeeded();
        counter++;
        player.Weapons.Current.Ammo += 2;
        Script.Wait(10);
    }
    //Thanks to spearminty!
    Vector3 GetDirectionFromCam()
    {
        Vector3 rot = GameplayCamera.Rotation;
        Vector3 coord = GameplayCamera.Position;

        float tZ = rot.Z * 0.0174532924f;
        float tX = rot.X * 0.0174532924f;
        float num = Math.Abs((float)Math.Cos((double)tX));
        

         coord.X= (float)(-Math.Sin((double)tZ)) * num;
         coord.Y = (float)(Math.Cos((double)tZ)) * num;
         coord.Z = (float)(Math.Sin((double)tX));
            
        return coord;
        
    }
    Vector3 GetCoordsFromCam(int distance)
    {
        Vector3 rot = GameplayCamera.Rotation;
        Vector3 coord = GameplayCamera.Position;

        float tZ = rot.Z * 0.0174532924f;
        float tX = rot.X * 0.0174532924f;
        float num = Math.Abs((float)Math.Cos((double)tX));

        coord.X = coord.X + (float)(-Math.Sin((double)tZ)) * (num + distance);
        coord.Y = coord.Y + (float)(Math.Cos((double)tZ)) * (num + distance) ;
        coord.Z = coord.Z + (float)(Math.Sin((double)tX)) * 8;

        return coord;

        
    }
}
