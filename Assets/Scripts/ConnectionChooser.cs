using UnityEngine;

public class ConnectionChooser : MonoBehaviour
{
    [SerializeField] private Transform destinationRouter;

    public Transform GetDestination() => destinationRouter;
    public void SetDestination(Transform dest) => destinationRouter = dest;

    public void OnClicked()
    {
        if (destinationRouter != null && PacketController.Instance != null)
        {
            var current = PacketController.Instance.currentRouter;

            if (PacketController.Instance.IsPossibleNextDestination(current, destinationRouter))
            {
                PacketController.Instance.SetNextDestination(destinationRouter);
                Debug.Log($"Valid destination: {destinationRouter.name}");
            }
            else
            {
                Debug.Log($"Invalid destination from {current?.name} to {destinationRouter.name}");
            }
        }
    }
}
