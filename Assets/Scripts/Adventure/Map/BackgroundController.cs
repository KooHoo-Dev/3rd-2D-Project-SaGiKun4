using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private const float SunsetPlayerMinPos = 80f;
    private const float SunsetPlayerMaxPos = 95f;

    [SerializeField] Transform backgroundGroup2;

    SpriteRenderer[] sprites;

    public Transform player;

    private void Awake()
    {
        sprites = backgroundGroup2.GetComponentsInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        float alpha = Mathf.InverseLerp(SunsetPlayerMinPos, SunsetPlayerMaxPos, player.position.y);

        SetAlpha(alpha);
    }

    public void SetAlpha(float alpha)
    {
        foreach (SpriteRenderer sprite in sprites)
        {
            Color color = sprite.color;

            color.a = alpha;

            sprite.color = color;
        }
    }
}