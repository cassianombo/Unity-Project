using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = " Scriptable object/Item")]
public class Item : ScriptableObject
{

    [Header("Only gameplay")]
    public TileBase Tile;
    public ItemType Type;
    public ActionType ActionType;
    public Vector2Int range = new Vector2Int(5, 4);

    [Header("Only UI")]
    public int maxStack = 1;

    [Header("Both")]
    public Sprite sprite;

    public bool stackable => maxStack > 1;
}

public enum ItemType
{
    Tool,
    Food
}

public enum ActionType
{
    Cut,
    Fish
}
