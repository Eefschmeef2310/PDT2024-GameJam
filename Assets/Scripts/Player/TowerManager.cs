using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private float playerOuterRadius = 2.8f;
    [SerializeField] private float playerInnerRadius = .5f;
    [SerializeField] private float towerRotationSpeed = 60f;
    [SerializeField] public GameObject playerTowersParent;

    private Vector3 mousePosition;
    private GameObject placingTower;
    private bool isPlacingTower = false;
    public Collider2D[] colliders;

    public float detectionRadius = 0.5f;
    public LayerMask towerLayerMask;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerOuterRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, playerInnerRadius);

        if (colliders != null)
        {
            Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(cursorPosition, detectionRadius);
        }
    }

    void Start()
    {
        CreateGenericTower();
    }

    // Used for testing
    [SerializeField] private List<GameObject> towers;
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && !isPlacingTower)
        {
            RotatePlayerTowersClockwise();
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

    public void CreateGenericTower()
    {
        SetPlacingTower(Instantiate(towers[0], mousePosition, Quaternion.identity));
    }

    public void CreateShieldTower()
    {
        SetPlacingTower(Instantiate(towers[1], mousePosition, Quaternion.identity));
    }

    private void SetPlacingTower(GameObject tower = null)
    {
        Time.timeScale = 0;
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
        placingTower.layer = 6;
        placingTower.transform.SetParent(playerTowersParent.transform);
        SetPlacingTower();
        Time.timeScale = 1;
    }

    private void RotatePlayerTowersClockwise()
    {
        float rotationAmount = towerRotationSpeed * Time.deltaTime;
        playerTowersParent.transform.Rotate(0f, 0f, rotationAmount);
    }

    private bool IsTowerPositionValid()
    {
        float towerRadius = placingTower.GetComponent<BaseTower>().TowerSizeRadius;
        float towerDistanceToCentre = CalculateDistanceToTower(placingTower.transform.position);
        

        return (towerDistanceToCentre - towerRadius > playerInnerRadius) &&
       (towerDistanceToCentre + towerRadius < playerOuterRadius) && !CheckForTowers();
    }

    private bool CheckForTowers()
    {
        detectionRadius = placingTower.GetComponent<BaseTower>().TowerSizeRadius;
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        colliders = Physics2D.OverlapCircleAll(cursorPosition, detectionRadius, towerLayerMask);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Tower"))
            {
                return true;
            }
        }
        return false;
    }
}
