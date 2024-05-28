using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private float playerOuterRadius = 2f;
    [SerializeField] private float playerInnerRadius = .03f;
    [SerializeField] private GameObject playerTowersParent;

    private Vector3 mousePosition;

    private bool isPlacingTower = false;
    private GameObject placingTower;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerOuterRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, playerInnerRadius);
    }

    void Start()
    {
        
    }

    // Used for testing
    [SerializeField] private List<GameObject> towers;
    void Update()
    {
        // Testing key binds
        if (Input.GetKeyDown(KeyCode.Alpha1) && !isPlacingTower)
        {
            SetPlacingTower(Instantiate(towers[0], mousePosition, Quaternion.identity));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && !isPlacingTower)
        {
            SetPlacingTower(Instantiate(towers[1], mousePosition, Quaternion.identity));
        }

        if (isPlacingTower)
        {
            MoveSelectedTower();
            if (IsTowerPositionValid())
            {
                placingTower.GetComponent<SpriteRenderer>().color = Color.green;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    PlaceSelectedTower();
                }
            }
            else
            {
                placingTower.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }

    private void SetPlacingTower(GameObject tower = null)
    {
        if (tower == null)
        {
            isPlacingTower = false;
            placingTower = null;
        }
        else
        {
            isPlacingTower = true;
            placingTower = tower;
            placingTower.GetComponent<BaseTower>().enabled = false;
        }
    }

    private float CalculateDistanceToTower(Vector3 towerCentrePosition)
    {
        return Vector3.Distance(towerCentrePosition, transform.position);
    }

    public void MoveSelectedTower()
    {
        Vector3 mousePosition = Input.mousePosition;

        mousePosition.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 targetPosition = new Vector2(worldPosition.x, worldPosition.y);
        placingTower.transform.position = targetPosition;
    }

    private void PlaceSelectedTower()
    {
        placingTower.GetComponent<SpriteRenderer>().color = Color.white;
        placingTower.GetComponent<BaseTower>().enabled = true;
        placingTower.transform.SetParent(playerTowersParent.transform);
        SetPlacingTower();
    }

    private bool IsTowerPositionValid()
    {
        float towerRadius = placingTower.GetComponent<BaseTower>().TowerSizeRadius;
        float towerDistanceToCentre = CalculateDistanceToTower(placingTower.transform.position);

        return (towerDistanceToCentre - towerRadius > playerInnerRadius) &&
       (towerDistanceToCentre + towerRadius < playerOuterRadius);
    }
}
