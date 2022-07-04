using UnityEngine;

public class TreeRandomizer : MonoBehaviour
{
    [SerializeField]
    private float destroyChance;

    [Header("Размер")]
    [SerializeField]
    private float minSize;
    [SerializeField]
    private float maxSize;

    [Header("Положение")]
    [SerializeField]
    private float treeFrontChance;

    [SerializeField]
    private int minFrontLayer;
    [SerializeField]
    private int maxFrontLayer;
    [SerializeField]
    private int minBackLayer;
    [SerializeField]
    private int maxBackLayer;

    private void Start()
    {
        foreach (Transform tree in transform)
        {
            if (Random.value < destroyChance)
            {
                Destroy(tree.gameObject);
                continue;
            }
            tree.transform.localScale = Vector3.one * Random.Range(minSize, maxSize);

            int layer;
            // Спереди
            if (Random.value < treeFrontChance)
                layer = Random.Range(minFrontLayer, maxFrontLayer);
            // Сзади
            else
                layer = Random.Range(minBackLayer, maxBackLayer);
            tree.GetComponent<SpriteRenderer>().sortingOrder = layer;

        }
        
    }
}
