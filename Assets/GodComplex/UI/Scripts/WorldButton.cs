using UnityEngine;

namespace Unity.Template.CompetitiveActionMultiplayer
{
    public class WorldButton : MonoBehaviour
    {
        RoomManager roomManager;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            roomManager = FindFirstObjectByType<RoomManager>();
            if(roomManager == null)
            {
                print("RoomManager not found in scene");
                return;
            }
        }

        public void SendPositionOfNewRoom()
        {
            if(roomManager == null)
            {
                print("RoomManager not found in scene");
                return;
            }
            roomManager.SetSpawnOfNextRoom(transform.position);
            gameObject.SetActive(false);
        }
    }
}
