using UnityEngine;

public abstract class BirdSkill : MonoBehaviour
{
    public bool IsFinish { get; protected set; } = false;

    public virtual void CurrentSkillInit(BirdMovement bird)
    { 
        IsFinish = false;
    }

    public virtual void CancelSkill()
    {
        IsFinish = true;
    }

    public abstract void Execute(BirdMovement bird);

    public abstract bool CanUse(BirdMovement bird);

    protected void FinishSkill()
    {
        IsFinish = true;
    }
}
