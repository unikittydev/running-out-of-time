using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ParallaxWallpaper : MonoBehaviour
    {
        [SerializeField]
        private Image image;

        private Image duplicate;

        [SerializeField]
        private float scrollSpeed;

        private Transform player;
        private float oldPlayerPos;
        
        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag(Tags.player).transform;
            duplicate = Instantiate(image, image.transform.parent);
            SetPositions(image.rectTransform.anchoredPosition3D);
        }

        private void Update()
        {
            float deltaX = player.position.x - oldPlayerPos;

            Vector3 pos = image.rectTransform.anchoredPosition3D;
            pos.x += -deltaX * scrollSpeed * Time.deltaTime;

            if (pos.x >= 0f)
                pos -= new Vector3(image.rectTransform.rect.width, 0f, 0f);
            else if (pos.x <= -image.rectTransform.rect.width)
                pos += new Vector3(image.rectTransform.rect.width, 0f, 0f);
            SetPositions(pos);

            oldPlayerPos = player.position.x;
        }

        private void SetPositions(Vector3 pos)
        {
            image.rectTransform.anchoredPosition3D = pos;
            duplicate.rectTransform.anchoredPosition3D = pos + new Vector3(image.rectTransform.rect.width - 2f, 0f, 0f);
        }
    }
}
