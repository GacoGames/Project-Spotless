using UnityEngine;

public class NPC : GameplayObject
{
    public string npcName;
    public override string ObjectName => npcName;

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
    }
}
