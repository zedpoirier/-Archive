using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This camera script came from a tutorial from a guy named Stephen Barr. It is pretty basic, but pretty neat,
/// especially with the obstruction portion.
/// </summary>
public class CameraControler : MonoBehaviour
{
    // Variables for the camera position.
    public float rotationSpeed = 1f;
    public Transform target, player; // Target is the point on which the camera will focus. It is the empty child of the player.
    float mouseX, mouseY;

    // variables to handle obstructions, like walls and other objects. In those cases, the camera will zoom in on the player.
    public Transform obstruction; // this object is filled automatically whenever the raycasthit detects a game object with the tag "object"
    float zoomSpeed = 2f;

    void Start()
    {        
        obstruction = target;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        CamControl();
        ViewObstructed();
    }

    // Basic camera movement.
    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(target);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            // when holding left shift, you engage free camera., The camera rotates around the target point.
            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
        else
        {
            // Camera rotates together with the player ALWAYS facing from behind.
            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            player.rotation = Quaternion.Euler(0, mouseX, 0);
        }

        
    }

    void ViewObstructed()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, target.position - transform.position, out hit, 4.5f)) // Detects if there are obstructions within set distance.
        {
            if(hit.collider.gameObject.tag != "Player")
            {
                obstruction = hit.transform;
                obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly; // Neat little property I found on the tutorial that helped make this script. It "hides" the meshrenderer but leaves its shadow.

                if (Vector3.Distance(obstruction.position, transform.position) >= 3f && Vector3.Distance(transform.position, target.position) >= 1.5f)
                {
                    transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                }
            }
            else
            {
                obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On; // "Unhides" the obstruction when it is outside of the range of the raycasthit.
                if(Vector3.Distance(transform.position, target.position) < 4.5f)
                {
                    transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
                }
            }
            
        }
    }
}
