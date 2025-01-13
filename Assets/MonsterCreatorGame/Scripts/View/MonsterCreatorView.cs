using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterCreatorView : MonoBehaviour
{
    [SerializeField] private List<Slot> craftingSlots;
    [SerializeField] private Image customCursor;
    [SerializeField] private MonsterCreatorController monsterController;
    [SerializeField] private Button createButton;

    private Item _currentItem;
    private List<Item> _currentRecipe;

    private void Start()
    {
        _currentRecipe = new List<Item>(new Item[craftingSlots.Count]);
        InitializeSlots();
    }
    private void InitializeSlots()
    {
        for (int i = 0; i < craftingSlots.Count; i++)
        {
            Slot slot = craftingSlots[i];
            slot.Initialize(i, slot.GetComponent<Image>());
        }
    }

    private void Update()
    {
        if (_currentItem != null)
        {
            customCursor.transform.position = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0) && _currentItem != null)
        {
            OnMouseReleased();
        }
    }

    public void OnMouseDownItem(Item item)
    {
        _currentItem = item;
        customCursor.gameObject.SetActive(true);
        customCursor.sprite = item.icon.sprite;
    }

    private void OnMouseReleased()
    {
        Slot nearestSlot = FindNearestSlot(Input.mousePosition);
        if (nearestSlot != null)
        {
            nearestSlot.SetItem(_currentItem);
            _currentRecipe[nearestSlot.Index] = _currentItem;
        }

        _currentItem = null;
        customCursor.gameObject.SetActive(false);
        UpdateCreateButtonState();
    }

    private Slot FindNearestSlot(Vector2 position)
    {
        Slot nearest = null;
        float minDistance = float.MaxValue;

        foreach (var slot in craftingSlots)
        {
            float distance = Vector2.Distance(position, slot.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = slot;
            }
        }

        return nearest;
    }
    private void UpdateCreateButtonState()
    {
        foreach (var item in _currentRecipe)
        {
            if (item == null)
            {
                createButton.gameObject.SetActive(false);
                return;
            }
        }
        createButton.gameObject.SetActive(true);
    }
    public void CheckRecipe()
    {
        string recipe = "";

        foreach (var item in _currentRecipe)
        {
            recipe += item?.id ?? "null";
        }

        monsterController.CreateMonster(recipe, ResetSystem);
    }
    private void ResetSystem()
    {
        foreach (var slot in craftingSlots)
        {
            slot.ClearItem();
        }
        _currentRecipe = new List<Item>(new Item[craftingSlots.Count]);
    }
}
