using UnityEngine;

public class ExaminePoint : GameplayObject
{
    public string examineText;
    override public string ObjectName => examineText;

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
