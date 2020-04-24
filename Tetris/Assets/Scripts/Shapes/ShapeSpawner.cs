using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ShapeSpawner : MonoBehaviour
{
    
    public BlockController shapeMaster;
    public Image shapePreview;

    GameObject nextShape;

    GameObject[] shapes;
    Sprite[] blockStyles;

    // Start is called before the first frame update
    void Start()
    {

        shapes = Resources.LoadAll<GameObject>("Prefabs");
        blockStyles = Resources.LoadAll<Sprite>("Blocks");

        CreateNextShape();
        Instanciar();
    }

    public void Instanciar()
    {
        shapeMaster.shape =
            Instantiate(nextShape, transform.position + nextShape.transform.position, Quaternion.identity);

        CreateNextShape();
    }

    void CreateNextShape()
    {
        GameObject shape = shapes[Random.Range(0, shapes.Length)];

        ChangeBlockStyle(shape);

        nextShape = shape;

        CreateShapePreview();
    }

    void CreateShapePreview()
    {
        Texture2D previewTexture = GenerateTExture();

        Sprite previewSprite = 
            Sprite.Create(previewTexture, new Rect(0, 0, previewTexture.width, previewTexture.height), Vector2.zero);

        shapePreview.sprite = previewSprite;
    }

    Texture2D GenerateTExture()
    {
        RuntimePreviewGenerator.OrthographicMode = true;
        RuntimePreviewGenerator.BackgroundColor = new Color(0,0,0,0);
        RuntimePreviewGenerator.PreviewDirection = new Vector3(0, 0, 90);

        return RuntimePreviewGenerator.GenerateModelPreview(nextShape.transform);
    }

    void ChangeBlockStyle(GameObject shape)
    {
        Sprite style = blockStyles[Random.Range(0, blockStyles.Length)];

        foreach (Transform block in shape.transform)
        {
            block.GetComponent<SpriteRenderer>().sprite = style;
        }
    }
}
