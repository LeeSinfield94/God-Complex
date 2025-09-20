using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.Template.CompetitiveActionMultiplayer
{
    public class RoomManager : MonoBehaviour
    {
        public List<Room> worldRooms = new List<Room>();
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            worldRooms.AddRange(FindObjectsByType<Room>(FindObjectsSortMode.None).ToList());
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
