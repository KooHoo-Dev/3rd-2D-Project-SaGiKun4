using UnityEngine;

public class DashSkill : BirdSkill
{
    private bool isDash = false;
    private bool isCharge = false;
    private Vector2 target;

    private float chargeTime = 3f;
    private float chargeTimer;
    private float dashSpeed = 8f;
    private Vector2 dashDirection;

    private float coolTime = 8f;
    private float coolTimeTimer;


    public override void CurrentSkillInit(BirdMovement bird)
    {
        base.CurrentSkillInit(bird);
        isDash = false;
        isCharge = false;
    }

    public override void Execute(BirdMovement bird)
    {
        if (IsFinish)
            return;

        if (!isCharge && !isDash)
        {
            bird.Rigidbody2D.linearVelocity = Vector2.zero;

            target = bird.Player.position;

            Vector2 dir = (target - (Vector2)transform.position).normalized;

            bird.FocusOnDirection(dir);

            dashDirection = (target - (Vector2)transform.position).normalized;

            chargeTimer = chargeTime;

            isCharge = true;
        }

        if (isCharge)
        {
            chargeTimer -= Time.deltaTime;

            if (chargeTimer <= 0f)
            {
                isCharge = false;
                isDash = true;
            }
        }

        if (isDash)
        {
            bird.Rigidbody2D.linearVelocity = dashDirection * dashSpeed;

            if (Vector2.Distance(transform.position, target) < 0.3f)
            {
                bird.Rigidbody2D.linearVelocity = Vector2.zero;

                isDash = false;

                coolTimeTimer = coolTime;

                FinishSkill();
            }
        }
    }

    public override bool CanUse(BirdMovement bird)
    {
        coolTimeTimer -= Time.deltaTime;

        if (coolTimeTimer > 0f)
            return false;

        return true;
    }
}
