//Author : Rafsan Ratul
//Animal cannon mod for GTA V , lets you shoot with animals! lol

//There are obviously better way to do this...but meh :v It works!

using System;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;

public class AnimalShooter : Script
{

    public AnimalShooter()
    {
        Tick += onTick;
        KeyDown += onKeyDown;
        KeyUp += onKeyUp;
    }
    
    //string[] zoo = { "a_c_boar"   , "a_c_chickenhawk", "a_c_shepherd", "a_c_sharktiger", "a_c_seagull", "a_c_rottweiler",
    //                 "a_c_rhesus" , "a_c_chimp"      , "a_c_chop"    , "a_c_cormorant" , "a_c_cow"    , "a_c_coyote"    ,
    //                 "a_c_crow"   , "a_c_deer"       , "a_c_fish"    , "a_c_hen"       , "a_c_husky"  , "a_c_mtlion"    ,
    //                 "a_c_pig"     , "a_c_rat"       , "a_c_retriever"};
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
    GTA.Menu AmmoMenu;
    int force = 100;
    
        void shootAnimals()
    {   

        Ped pedAnimal = (bCocktail)? World.CreatePed(zoo[rnd.Next(0, 25)], player.GetOffsetInWorldCoords(new GTA.Math.Vector3(0, 3, 0))) : World.CreatePed(selectedped, player.GetOffsetInWorldCoords(new GTA.Math.Vector3(0, 3, 0)));

        Function.Call(Hash.SET_PED_TO_RAGDOLL, pedAnimal, 4000, 4000, 0, 0, 0);

        pedAnimal.Velocity.Equals(10);
        pedAnimal.ApplyForce(player.ForwardVector * 50);


        //Function.Call(GTA.Native.Hash.SET_ENTITY_HEALTH, pedAnimal, 0);
        pedAnimal.Task.FleeFrom(Game.Player.Character.Position);
        pedAnimal.MarkAsNoLongerNeeded();
        counter++;
        player.Weapons.Current.Ammo += 2;

    }

    Vector3 RotationToDirection(Vector3 rotation)
    {
        float radZ = rotation.Z * 0.0174532924f;
        float radX = rotation.X * 0.0174532924f;

        float num = Math.Abs((float)Math.Cos((double)radX));

        return new Vector3
        {
            X = (float)((double)((float)(-(float)Math.Sin((double)radZ))) * (double)num),
            Y = (float)((double)((float)Math.Cos((double)radZ)) * (double)num),
            Z = (float)Math.Sin((double)radX)

        };
    }

    void shootAnimal2()
    {   //get camera roation
        Vector3 Rot = Function.Call<Vector3>(Hash.GET_CAM_ROT, 2);
        //normalize to directional vector
        Vector3 direction = RotationToDirection(Rot);

        //Vector3 Rotation = GameplayCamera.Rotation;

        //directional force vector
        direction.X = force * direction.X;
        direction.Y = force * direction.Y;
        direction.Z = force * direction.Z;

        

        Ped pedAnimal = World.CreatePed(selectedped, player.GetOffsetInWorldCoords(new GTA.Math.Vector3(0, 1, 0)));

        Function.Call(Hash.SET_PED_TO_RAGDOLL, pedAnimal, 4000, 4000, 0, 0, 0);

        Function.Call(Hash.APPLY_FORCE_TO_ENTITY, pedAnimal, direction.X , direction.Y , direction.Z, 0 , 0 , 0 , false, false, true, true, false, true );
        counter++;
        player.Weapons.Current.Ammo += 2;
    }

    private void onTick(object sender, EventArgs e)
    {
        if (counter > 300)
        {
            Function.Call(Hash.CLEAR_AREA, player.Position.X, player.Position.Y, player.Position.Z, 200, false);
            counter = 0;
        }
        
        //if (player.IsShooting && bActivated) shootAnimals();
        if (player.IsShooting && bActivated) shootAnimal2();


    }

    private void onKeyDown(object sender, KeyEventArgs e)
    {

    }

    private void onKeyUp(object sender, KeyEventArgs e)
    {
        if ((e.KeyCode == Keys.F12) && AmmoMenu == null)
        {
            MainMenu();
        }
        else if ((e.KeyCode == Keys.F12) && AmmoMenu != null)
        {
            CloseMenu();
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

    void CloseMenu()
    {
        View.CloseAllMenus();
        AmmoMenu = null;
    }

    void MainMenu()
    {
        CloseMenu();
        AmmoMenu = new GTA.Menu("Animal Cannon!", new GTA.MenuItem[] { 
            new MenuButton("Toggle Animal Cannon"  , "Toggles the mod on or off" ,toggler         ),
            new MenuButton("Cat"            , "Switch to cat ammo"               ,catAmmo         ),
            new MenuButton("Retriever"      , "Switch to retriever ammo type"    ,retrieverAmmo   ),
            new MenuButton("Chop"           , "Switch to Chop(Dog) ammo type"    ,chopAmmo        ),
            new MenuButton("Shepard"        , "Switch to shepard(dog) ammo type" ,shepardAmmo     ),
            new MenuButton("Huskey"         , "Switch to huskey ammo type"       ,huskeyAmmo      ),
            new MenuButton("Coyote"         , "Switch to coyote ammo type"       ,coyoteAmmo      ),
            new MenuButton("Cow"            , "Switch to cow ammo"               ,cowAmmo         ),
            new MenuButton("Rhesus"         , "Switch to rhesus ammo type"       ,rhesusAmmo      ),
            new MenuButton("Cormorant"      , "Switch  to cormorant ammo type"   ,cormorantAmmo   ),
            new MenuButton("Mountain Lion"  , "Switch to mountain lion ammo type",mtlionAmmo      ),
            new MenuButton("Deer"           , "Switch to deer ammo type"         ,deerAmmo        ),
            new MenuButton("Rat"            , "Switch to rat ammo type"          ,ratAmmo         ),
            new MenuButton("Pig "           , "Switch to pig ammo type"          ,pigAmmo         ),
            new MenuButton("Boar"           , "Switch to boar ammo type"         ,boarAmmo        ),

            new MenuButton("Crow"           , "Switch to crow ammo"              ,crowAmmo        ),
            new MenuButton("Hen"            , "Switch to hen ammo type"          ,henAmmo         ),
            new MenuButton("ChickenHawk"    , "Switch to chicken-hawk ammo type" ,chickenhawkAmmo ),
            new MenuButton("Pigeon"         , "Switch to pigeon ammo type"       ,pigeonAmmo      ),
            new MenuButton("Seagull"        , "Switch to seagull ammo type"      ,seagull         ),


            new MenuButton("Fish"           , "Switch to fish ammo type"         ,fishAmmo        ), 
            new MenuButton("Tiger Shark"    , "Switch to tiger shark ammo type"  ,tsAmmo          ),                      
            new MenuButton("Hammer Shark"   , "Switch to hammer-shark ammo type" ,hsAmmo          ),
            new MenuButton("Dolphin"        , "Switch to dolphin ammo type"      ,dolphinAmmo     ),
            new MenuButton("Humpback Whale" , "Switch to humpback ammo type"     ,humpbackAmmo    ),
            new MenuButton("Killer Whale"   , "Switch to killer-whale ammo type" ,killerwhaleAmmo ),
            new MenuButton("TOGGLE COCKTAIL"       , "YOU NEVER KNOW!"                  ,cocktailAmmo)
            });


        AmmoMenu.ItemHeight = 20;
        AmmoMenu.Width = 300;
        AmmoMenu.FooterCentered = true;
        View.AddMenu(AmmoMenu);
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
    void seagull()
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

    
}

