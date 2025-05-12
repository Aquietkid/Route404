using UnityEngine;

public class ConnectionInputDetector : MonoBehaviour
{
    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
            ProcessInput(Input.mousePosition);
#elif UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            ProcessInput(Input.GetTouch(0).position);
#endif
    }

    void ProcessInput(Vector3 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var chooser = hit.collider.GetComponent<ConnectionChooser>();
            if (chooser != null)
            {
                chooser.OnClicked();
                Debug.Log("Clicked on connection: " + hit.collider.name);
            }
            else
            {
                Debug.Log("No ConnectionChooser on " + hit.collider.name);
            }
        }
    }
}
