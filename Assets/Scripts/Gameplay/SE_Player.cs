using FishNet.Object;
using UnityEngine;
using FishNet.Object.Synchronizing;
using UnityEditor;

public class SE_Player : NetworkBehaviour
{
    public string id { get {
            if (credentials == null)
            {
                if (IsOwner)
                {
                    credentials = Resources.Load<UserCredentials>("UserCredentials");
                    return credentials.id;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return credentials.id;
            }
            
        } }
    private UserCredentials credentials;

    public void TestMovement()
    {

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        movement *= 5 * Time.deltaTime;

        transform.Translate(movement);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;


        TestMovement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TestSpawnPersistentObject();
        }

        if (Input.GetMouseButtonDown(0))
        {
            TestInteraction();
        }
    }

    public void TestSpawnPersistentObject()
    {
        SpawnData data = new SpawnData();
        data.OwnerID = id;
        data.Position = transform.position;
        SE_Resources.PersistentSpawnRequest?.Invoke(data);
    }

    public void TestInteraction()
    {
        //Need to do camera stuff here. this is just for testing
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);   
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log("Hit: " + hit.collider.gameObject.name);
            Selectable selectable = hit.collider.gameObject.GetComponent<Selectable>(); 

            if(selectable != null) {
                selectable.ResolveSelection(id);
            }
            
        }
    }

}
