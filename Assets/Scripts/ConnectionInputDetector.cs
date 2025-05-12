using UnityEngine;

public class ConnectionInputDetector : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // or Touch if mobile
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                var chooser = hit.collider.GetComponent<ConnectionChooser>();
                if (chooser != null)
                {
                    chooser.OnClicked(); // This must call SetNextDestination()
                    Debug.Log("Clicked on connection: " + hit.collider.name);
                }
                else
                {
                    Debug.Log("No ConnectionChooser on " + hit.collider.name);
                }
            }
        }
    }
}
