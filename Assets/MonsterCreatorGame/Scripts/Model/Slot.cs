using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    //public Item item;
    //public int index;
    public int Index { get; private set; }
    private Image _icon;
    private Item _item;

    public void Initialize(int index, Image icon)
    {
        Index = index;
        _icon = icon;
    }

    public void SetItem(Item item)
    {
        _item = item;
        _icon.sprite = item.icon.sprite;
        _icon.gameObject.SetActive(true);
    }

    public void ClearItem()
    {
        _item = null;
        _icon.sprite = null;
        _icon.gameObject.SetActive(false);
    }
}
