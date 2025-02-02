using UnityEngine;

public class Portal : GameplayObject
{
    public Room targetRoom;

    public override string ObjectName => targetRoom.Title;

    public override void OnHover()
    {
        base.OnHover();
    }
    public override void OnHoverEnd()
    {
        base.OnHoverEnd();
    }

    public override void OnClick()
    {
        base.OnClick();
        Location.Instance.ChangeRoom(targetRoom);
    }
}
