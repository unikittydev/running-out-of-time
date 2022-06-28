using UnityEngine;

namespace Game
{
    /// <summary>
    /// Контроллер игрока.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        public TimeEpoch playerEpoch { get; set; } = TimeEpoch.Present;
    }
}
