using InControl;

public class PaddleActions : PlayerActionSet
{
    public PlayerAction Left;
    public PlayerAction Right;
    public PlayerOneAxisAction Move;

    public PaddleActions()
    {
        Left = CreatePlayerAction("Move Left");
        Right = CreatePlayerAction("Move Right");
        Move = CreateOneAxisPlayerAction(Left, Right);
    }
}
