using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float attackSpeed;
    [SerializeField] private Vector3 defaultDetectedArea;
    [SerializeField] private Vector3 detectedArea;
    [SerializeField] private UnitAttack defaultAttackBehaviour;
    [SerializeField] private UnitAttack attackBehaviour;
    
    public List<MobState> mobs {get; private set;} = new();
    private SpriteRenderer sprite;
    private Sprite defaultSprite;

    private float attackTimer;

    public bool HasItem { get; private set; }
    public Item EquippedItem { get; private set; }
    public int damage {get; private set;} = 10;
    public bool isMove {get; set;} = false;

    private void OnEnable()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        defaultSprite = sprite.sprite;
        attackBehaviour = defaultAttackBehaviour;
        attackTimer = attackSpeed;
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackSpeed)
        {
            attackTimer = 0;
            
            if(!isMove)
                attackBehaviour.Attack(this, mobs);
        }

        if(mobs.Count > 0)
        {
            Vector3 targetPos = mobs[0].transform.position;
            float sign = Mathf.Sign(targetPos.x - transform.position.x);

            sign = sign == Mathf.Sign(sprite.transform.localScale.x) ? 1 : -1;

            sprite.transform.localScale = new Vector3(sign * sprite.transform.localScale.x, sprite.transform.localScale.y, sprite.transform.localScale.z);
        }
    }

    public void EquipItem(Item item)
    {
        if (HasItem) return;

        EquippedItem = item;
        HasItem = true;

        attackBehaviour = item.attackData;
        sprite.sprite = item.BirdSprite;

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