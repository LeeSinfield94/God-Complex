using UnityEngine;

namespace Unity.Template.CompetitiveActionMultiplayer
{
    public class Room : MonoBehaviour
    {
        private Vector3 spawnPos;
        public Vector3 SpawnPos => spawnPos;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            this.transform.position = spawnPos;
        }
    }
}
