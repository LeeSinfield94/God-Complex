using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace Unity.Template.CompetitiveActionMultiplayer
{
    public class RoomManager : MonoBehaviour
    {
        private Vector3 nextRoomsPosition = Vector3.zero;
        public int roomsToSpawn = 5;
        public Room roomPrefab;
        public List<Room> worldRooms = new List<Room>();
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            for (int i = 0; i < roomsToSpawn; i++)
            {
                Room room = Instantiate(roomPrefab, Vector3.zero, Quaternion.identity);
                room.gameObject.SetActive(false);
            }
            worldRooms.AddRange(FindObjectsByType<Room>(FindObjectsSortMode.None).ToList());
        }

        public void SetSpawnOfNextRoom(Vector3 pos)
        {
            nextRoomsPosition = pos;
            print(nextRoomsPosition);
        }
        public void SpawnRoom()
        {
            if (nextRoomsPosition != Vector3.zero)
            {
                foreach (var room in worldRooms)
                {
                    if (!room.gameObject.activeInHierarchy)
                    {
                        room.SpawnPos = nextRoomsPosition;
                        room.gameObject.SetActive(true);
                        return;
                    }
                }
            }
            else
            {
                Debug.LogError("Next room position not set");
            }
        }
    }
}
