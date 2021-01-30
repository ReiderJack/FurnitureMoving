using UnityEngine;

public enum FurnitureType
{
    Celling,
    Floor,
    Wall
}

public class Furniture : MonoBehaviour
{
    [SerializeField] private FurnitureType type = FurnitureType.Floor;
    public FurnitureType Type => type;

    private Vector3 mOffset;
    private float mZCoord;

    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        mOffset = gameObject.transform.position - GetMouseWorldPos();
        
        FurnitureController.Instance.SelectFurniture(gameObject);
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }
}
