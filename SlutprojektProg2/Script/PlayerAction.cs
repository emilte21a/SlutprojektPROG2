
using System.Runtime.CompilerServices;

public class PlayerAction
{
    public Rectangle handRectangle;
    Vector2 position;
    float timer = 1;

    public Vector2 origin;

    public PlayerAction()
    {
        position = new Vector2(0, 0);
        handRectangle = new Rectangle(0, 0, 20, 20);
    }

    public void OnClick(Vector2 playerPosition, Item currentItem)
    {
        origin = playerPosition + new Vector2(40, 40);
        System.Console.WriteLine(handRectangle);
        position = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), Game.camera);
        handRectangle.X = position.X;
        handRectangle.Y = position.Y;
        if (Vector2.Distance(origin, position) <= 240 && currentItem.usable)
        {
            /*
                skapa funktion som kollar om handrektangeln kolliderar med någon rektangel i världen t.ex träd eller stenblock
                eller så kollar jag checkcollisionpointrec 

                Varje prefab och tile ska ha HP som bestämmer om det ska finnas kvar eller inte. 
                om musen kolliderar med objektet och avståndet mellan spelaren och objektet är mindre än ett visst avstånd

                minska objektets hp om det finns med korresponderande currentItem om det itemet är usable

                om spelaren tar sönder prefaben eller tilen så ska spelaren 

                currentItem.itemDamage;
            */
        }

        timer -= Raylib.GetFrameTime();

        if (timer <= 0)
            timer = 1;
    }
}