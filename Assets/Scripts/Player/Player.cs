using UnityEngine;

namespace Game
{
    /// <summary>
    /// Игрок.
    /// </summary>
    public class Player : MonoBehaviour
    {
        public TimeEpoch playerEpoch { get; set; } = TimeEpoch.Present;

        private void Awake()
        {
            tag = Tags.PLAYER;
        }
    }
}
