using UnityEngine;

public class CarryItem : MonoBehaviour
{
    private GameObject fish;
    public Transform pickUpPosition;

    private bool canHold = true;
    
    private void Pickup()
    {
        if (!fish) return;
        
        fish.transform.SetParent(pickUpPosition);

        fish.GetComponent<Rigidbody>().isKinematic = true;

        fish.transform.rotation = pickUpPosition.rotation;
        fish.transform.position = pickUpPosition.position;

        canHold = false;
    }

    private void Drop()
    {
        if (!fish) return;

        fish.GetComponent<Rigidbody>().isKinematic = false;
        fish = null;

        pickUpPosition.GetChild(0).parent = null;
        canHold = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown("e"))
        {
            if (!canHold)
            {
                Drop();
            }
            else
            {
                Pickup();
            }
        }

        if (!canHold && fish)
        {
            fish.transform.position = pickUpPosition.position;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Fish")) return;
        
        if (!fish)
        {
            fish = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Fish")) return;
        
        if (canHold)
        {
            fish = null;
        }
    }
}
