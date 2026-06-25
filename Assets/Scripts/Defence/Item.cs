using UnityEngine;


[CreateAssetMenu(fileName = "InventoryItem",menuName = "SAO/InventoryItem", order = 1)]
    public class Item : ScriptableObject
    {
        public int Key; // 아이템을 찾을 Key
        public string Name; // 아이템의 명칭
        public string Description; // 아이템의 설명
        public Sprite Icon; // 아이템의 Sprite 아이콘
        public int Price; // 아이템의 가격
        public int Count; // 아이템의 가격
    }