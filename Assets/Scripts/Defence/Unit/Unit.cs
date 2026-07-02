using Study.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float attackSpeed;

    private Vector3 defaultDetectedArea;
    [field: SerializeField] public Vector3 detectedArea { get; private set; }
    [field: SerializeField] public SpriteRenderer detectedAreaSprite { get; private set; }

    [field: SerializeField] public UnitAttack defaultAttackBehaviour { get; private set; }
    [field:SerializeField] public UnitAttack attackBehaviour { get; private set; }

    public List<MobState> mobs {get; private set;} = new();
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;
    private Sprite defaultSprite;

    public bool HasItem { get; private set; }
    public Item EquippedItem { get; private set; }
    public int damage {get; private set;} = 10;
    public bool isMove {get; set;} = false;

    private void OnEnable()
    {
        animator = GetComponentInChildren<Animator>();
        defaultSprite = sprite.sprite;
        defaultDetectedArea = detectedArea;
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
        Debug.Log("공격 2");
    }

    public void EquipItem(Item item)
    {
        if (HasItem) return;

        EquippedItem = item;
        HasItem = true;

        attackBehaviour = item.attackData;
        sprite.sprite = item.BirdSprite;
        animator.runtimeAnimatorController = item.overrideController;

        InventoryManager.Instance.Inventory.RemoveItem(EquippedItem);

        Debug.Log($"{name} 아이템 장착: {item.Name}");
    }

    public void RemoveItem()
    {
        if (!HasItem) return;

        InventoryManager.Instance.Inventory.AddItem(EquippedItem);
        ContentController.Instance.Refresh();

        HasItem = false;
        EquippedItem = null;
        sprite.sprite = defaultSprite;

        attackBehaviour = defaultAttackBehaviour;
    }
}