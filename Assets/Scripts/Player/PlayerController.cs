using UnityEngine;

namespace Game
{
    /// <summary>
    /// ���������� ������.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        public TimeEpoch playerEpoch { get; set; } = TimeEpoch.Present;
    }
}
