using Study.Utilities;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float attackSpeed;

    private Vector3 defaultDetectedArea;
    [field: SerializeField] public Transform detectedObject { get; private set; }
    [field: SerializeField] public SpriteRenderer detectedAreaSprite { get; private set; }

    [field: SerializeField] public UnitAttack defaultAttackBehaviour { get; private set; }
    public UnitAttack attackBehaviour { get; private set; }

    public List<MobState> mobs {get; private set;} = new();
    [SerializeField] private SpriteRenderer sprite;
    private Animator animator;
    private RuntimeAnimatorController defaultAnimator;

    public bool HasItem { get; private set; }
    public Item EquippedItem { get; private set; }
    public int damage {get; private set;}
    private int defaultDamage = 5;
    public bool isMove {get; set;} = false;
    private int mobType;

    private void OnEnable()
    {
        animator = GetComponentInChildren<Animator>();
        mobType = 0;
        defaultAnimator = animator.runtimeAnimatorController;
        defaultDetectedArea = detectedObject.localScale;
        damage = defaultDamage;
        defaultDetectedArea = detectedObject.localScale;
        attackBehaviour = defaultAttackBehaviour;
    }

    private void Update()
    {
        if(mobs.Count > 0)
        {
            if (isMove) return;

            Vector3 targetPos = mobs[0].transform.position;
            float sign = Mathf.Sign(targetPos.x - transform.position.x);

            sign = sign == Mathf.Sign(sprite.transform.localScale.x) ? -1 : 1;

            sprite.transform.localScale = new Vector3(sign * sprite.transform.localScale.x, sprite.transform.localScale.y, sprite.transform.localScale.z);

            animator.SetTrigger("Attack");
        }

        // 테스트 코드
        if (SimpleInput.GetKeyDown(UnityEngine.InputSystem.Key.Space) && UnitSpawner.Instance.currentUnit == this)
        { RemoveItem(); }
    }

    public void Attack()
    {
        attackBehaviour.Attack(this, mobs);
        PlaySound();
    }

    public void EquipItem(Item item)
    {
        if (item.Tier == ItemTier.Infinite)
        {
            RemoveItem();
            return;
        }

        if (HasItem) return;

        EquippedItem = item;
        HasItem = true;

        attackBehaviour = item.attackData;
        sprite.sprite = item.BirdSprite;
        animator.runtimeAnimatorController = item.overrideController;
        detectedObject.localScale = item.detectedArea;
        damage = item.damage;
        mobType = item.Key;

        InventoryManager.Instance.Inventory.RemoveItem(EquippedItem);

        Debug.Log($"{name} 아이템 장착: {item.Name}");
    }

    public void RemoveItem()
    {
        if (!HasItem) return;

        animator.runtimeAnimatorController = defaultAnimator;
        attackBehaviour = defaultAttackBehaviour;
        damage = defaultDamage;
        detectedObject.localScale = defaultDetectedArea;

        InventoryManager.Instance.Inventory.AddItem(EquippedItem);
        ContentController.Instance.Refresh();

        HasItem = false;
        EquippedItem = null;
    }

    private void PlaySound(float pitch = 1)
    {
        if (mobType == 0) return; // 아기새
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Whip);
    }
}