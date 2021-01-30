using UnityEngine;

public class FurnitureController : MonoBehaviour
{
    public static FurnitureController Instance;
    private GameObject SelectedObject { get; set; }
    private Furniture SelectedFurniture { get; set; }
    private bool IsStickMode { get; set; }
    private bool IsFurnitureSelected { get; set; }
    
    [SerializeField] private LayerMask layerToIgnore = 8;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        ChangeMode();

        Deselect();
        
        if (IsFurnitureSelected == false) return;
        
        StickFurniture();
            
        RotateFurniture();
    }

    private void ChangeMode()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            IsStickMode = !IsStickMode;
        }
    }

    public void SelectFurniture(GameObject furniture)
    {
        SelectedObject = furniture;
        SelectedFurniture = furniture.GetComponent<Furniture>();
        IsFurnitureSelected = true;
    }

    private void Deselect()
    {
        if (Input.GetKey(KeyCode.X))
        {
            SelectedObject = null;
            SelectedFurniture = null;
            IsFurnitureSelected = false;
        }
    }

    private void StickFurniture()
    {
        if (Instance.IsStickMode == false) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity, ~layerToIgnore);
        
        if (LayerMask.NameToLayer(SelectedFurniture.Type.ToString()) != hit.transform.gameObject.layer) return;

        var furnitureTransform = SelectedFurniture.transform;
        furnitureTransform.position = hit.point;
        
        var vector = GetVector(furnitureTransform, hit);

        furnitureTransform.rotation = Quaternion.FromToRotation (vector, hit.normal) * furnitureTransform.rotation;
    }

    private static Vector3 GetVector(Transform furnitureTransform, RaycastHit hit)
    {
        var vector = -furnitureTransform.forward;

        var floor = LayerMask.NameToLayer("Celling");
        if (floor == hit.transform.gameObject.layer)
        {
            vector = furnitureTransform.forward;
        }

        var celling = LayerMask.NameToLayer("Floor");
        if (celling == hit.transform.gameObject.layer)
        {
            vector = furnitureTransform.up;
        }

        return vector;
    }

    private void RotateFurniture()
    {
        RotationZ();

        RotationX();

        RotationY();
    }

    private void RotationY()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SelectedObject.transform.Rotate(new Vector3(0, 15, 0));
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            SelectedObject.transform.Rotate(new Vector3(0, -15, 0));
        }
    }

    private void RotationX()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            SelectedObject.transform.Rotate(new Vector3(15, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SelectedObject.transform.Rotate(new Vector3(-15, 0, 0));
        }
    }

    private void RotationZ()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SelectedObject.transform.Rotate(new Vector3(0, 0, 15));
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SelectedObject.transform.Rotate(new Vector3(0, 0, -15));
        }
    }
}
