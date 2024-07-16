using UnityEngine;
using UnityEngine.UI;

// +----------------------------+
// | ---- GoragarX GameDev ---- |
// | goragarxgamedev@gmail.com  |
// +----------------------------+

namespace GoragarXGameDev
{
    [RequireComponent(typeof(RawImage))]
    public class ImageScroll : MonoBehaviour
    {
        [SerializeField] private RawImage image;
        [SerializeField] private float xSpeed, ySpeed;

        private void Update()
        {
            image.uvRect = new Rect(
                image.uvRect.position + new Vector2(xSpeed, ySpeed) * Time.deltaTime,
                image.uvRect.size);
        }
    }
}
