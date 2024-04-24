//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.InputSystem;

//public class DrawScript : MonoBehaviour
//{
//    [SerializeField] private DrawingActions drawingActions; // Reference to the generated input actions
//    private Texture2D texture;
//    private bool isDrawing = false;

//    private void Awake()
//    {
//        drawingActions = new DrawingActions();
//        drawingActions.Drawing.Draw.performed += ctx => isDrawing = true;
//        drawingActions.Drawing.Draw.canceled += ctx => isDrawing = false;
//        drawingActions.Drawing.Position.performed += ctx => Draw(ctx.ReadValue<Vector2>());

//        drawingActions.Drawing.Draw.started += ctx => Debug.Log("Touch started: ");
//        drawingActions.Drawing.Draw.performed += ctx => Debug.Log("Touch performed: ");
//        drawingActions.Drawing.Draw.canceled += ctx => Debug.Log("Touch canceled");
//    }

//    void Start()
//    {
//        var rectTransform = GetComponent<RectTransform>();
//        texture = new Texture2D((int)rectTransform.rect.width, (int)rectTransform.rect.height);
//        GetComponent<Image>().material.mainTexture = texture;
//    }

//    private void OnEnable()
//    {
//        drawingActions.Enable();
//    }

//    private void OnDisable()
//    {
//        drawingActions.Disable();
//    }

//    private void Draw(Vector2 position)
//    {
//        if (!isDrawing) return;

//        Vector2 localPoint;
//        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), position, null, out localPoint);
//        int x = (int)(localPoint.x + texture.width / 2);
//        int y = (int)(localPoint.y + texture.height / 2);
//        if (x >= 0 && y >= 0 && x < texture.width && y < texture.height)
//        {
//            texture.SetPixel(x, y, Color.black);
//            texture.Apply();
//        }
//    }
//}


//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.InputSystem;

//public class DrawScript : MonoBehaviour
//{
//    [SerializeField] private DrawingActions drawingActions; // Reference to the generated input actions
//    private Texture2D texture;
//    private bool isDrawing = false;

//    // Set the thickness of the stroke
//    public int strokeThickness = 5;

//    private void Awake()
//    {
//        drawingActions = new DrawingActions();
//        drawingActions.Drawing.Draw.performed += ctx => isDrawing = true;
//        drawingActions.Drawing.Draw.canceled += ctx => isDrawing = false;
//        drawingActions.Drawing.Position.performed += ctx => Draw(ctx.ReadValue<Vector2>());

//        drawingActions.Drawing.Draw.started += ctx => Debug.Log("Touch started: ");
//        drawingActions.Drawing.Draw.performed += ctx => Debug.Log("Touch performed: ");
//        drawingActions.Drawing.Draw.canceled += ctx => Debug.Log("Touch canceled");
//    }

//    void Start()
//    {
//        var rectTransform = GetComponent<RectTransform>();
//        texture = new Texture2D((int)rectTransform.rect.width, (int)rectTransform.rect.height);
//        GetComponent<Image>().material.mainTexture = texture;
//    }

//    private void OnEnable()
//    {
//        drawingActions.Enable();
//    }

//    private void OnDisable()
//    {
//        drawingActions.Disable();
//    }

//    private void Draw(Vector2 position)
//    {
//        if (!isDrawing) return;

//        Vector2 localPoint;
//        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), position, null, out localPoint);
//        DrawVector(localPoint, strokeThickness);
//    }

//    // Draw a circle of pixels around the point to create a thicker line
//    void DrawVector(Vector2 point, int thickness)
//    {
//        int centerX = (int)(point.x + texture.width / 2);
//        int centerY = (int)(point.y + texture.height / 2);
//        int radius = thickness / 2; // Adjust radius for thickness

//        for (int x = -radius; x <= radius; x++)
//        {
//            for (int y = -radius; y <= radius; y++)
//            {
//                if (x * x + y * y <= radius * radius) // Circle equation
//                {
//                    int drawX = centerX + x;
//                    int drawY = centerY + y;
//                    if (drawX >= 0 && drawY >= 0 && drawX < texture.width && drawY < texture.height)
//                    {
//                        texture.SetPixel(drawX, drawY, Color.black);
//                    }
//                }
//            }
//        }
//        texture.Apply();
//    }
//}

//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.InputSystem;

//public class DrawScript : MonoBehaviour
//{
//    [SerializeField] private DrawingActions drawingActions; // Reference to the generated input actions
//    private Texture2D texture;
//    private Vector2 lastPosition; // To keep track of the last position drawn
//    private bool isDrawing = false;
//    public int strokeThickness = 5; // The thickness of the stroke

//    private void Awake()
//    {
//        if (drawingActions == null)
//        {
//            //drawingActions = new DrawingActions();
//            //drawingActions.Drawing.Draw.performed += ctx => StartDrawing(ctx.ReadValue<Vector2>());
//            //drawingActions.Drawing.Draw.canceled += ctx => EndDrawing();
//            //drawingActions.Drawing.Position.performed += ctx => Draw(ctx.ReadValue<Vector2>());
//            drawingActions = new DrawingActions();

//            // Setup the callback for when a drawing starts (e.g., mouse button pressed)
//            drawingActions.Drawing.Draw.performed += ctx => isDrawing = true;
//            drawingActions.Drawing.Draw.canceled += ctx => isDrawing = false;

//            // Separate callback for position change (e.g., mouse moved)
//            drawingActions.Drawing.Position.performed += ctx =>
//            {
//                if (isDrawing)
//                {
//                    Draw(ctx.ReadValue<Vector2>());
//                }
//            };
//        }
//    }

//    void Start()
//    {
//        var rectTransform = GetComponent<RectTransform>();
//        texture = new Texture2D((int)rectTransform.rect.width, (int)rectTransform.rect.height);
//        GetComponent<Image>().material.mainTexture = texture;
//    }

//    private void OnEnable()
//    {
//        if (drawingActions != null)
//        {
//            drawingActions.Enable();
//        }

//    }

//    private void OnDisable()
//    {
//        if (drawingActions != null)
//        {
//            drawingActions.Disable();
//        }
//    }

//    private void StartDrawing(Vector2 position)
//    {
//        isDrawing = true;
//        // Convert the position to local space
//        Vector2 localPosition = ScreenToLocal(position);
//        // Start the line at the current position
//        lastPosition = localPosition;
//    }

//    private void EndDrawing()
//    {
//        isDrawing = false;
//    }

//    private void Draw(Vector2 position)
//    {
//        if (isDrawing)
//        {
//            // Convert the position to local space
//            Vector2 localPosition = ScreenToLocal(position);
//            // Draw a line from the last position to the current position
//            DrawLine(lastPosition, localPosition, strokeThickness);
//            // Update the last position
//            lastPosition = localPosition;
//        }
//    }

//    private Vector2 ScreenToLocal(Vector2 screenPosition)
//    {
//        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), screenPosition, null, out Vector2 localPoint);
//        return new Vector2(localPoint.x + texture.width / 2, localPoint.y + texture.height / 2);
//    }

//    private void DrawLine(Vector2 start, Vector2 end, int thickness)
//    {
//        Vector2 difference = end - start;
//        float steps = difference.magnitude / (thickness / 2f); // Ensure we fill in all gaps by basing it on thickness
//        Vector2 increment = difference / steps;

//        for (float i = 0; i <= steps; i++)
//        {
//            DrawVector(start + increment * i, thickness);
//        }
//    }

//    void DrawVector(Vector2 point, int thickness)
//    {
//        int centerX = (int)point.x;
//        int centerY = (int)point.y;
//        int radius = thickness / 2;

//        for (int x = -radius; x <= radius; x++)
//        {
//            for (int y = -radius; y <= radius; y++)
//            {
//                if (x * x + y * y <= radius * radius)
//                {
//                    int drawX = centerX + x;
//                    int drawY = centerY + y;
//                    if (drawX >= 0 && drawY >= 0 && drawX < texture.width && drawY < texture.height)
//                    {
//                        texture.SetPixel(drawX, drawY, Color.black);
//                    }
//                }
//            }
//        }
//        // Apply all SetPixel calls
//        texture.Apply();
//    }
//}



using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class DrawScript : MonoBehaviour
{
    [SerializeField] private DrawingActions drawingActions; // Reference to the generated input actions
    private Texture2D texture;
    private Vector2 lastPosition = Vector2.negativeInfinity; // Initialized to negative infinity
    private bool isDrawing = false;
    public int strokeThickness = 5; // The thickness of the stroke
    private List<Vector2> pointsToDraw = new List<Vector2>(); // List to accumulate points

    private void Awake()
    {
        InitializeDrawingActions();
    }

    private void InitializeDrawingActions()
    {
        drawingActions = new DrawingActions();

        drawingActions.Drawing.Draw.performed += ctx => isDrawing = true;
        drawingActions.Drawing.Draw.canceled += ctx => EndDrawing();

        drawingActions.Drawing.Position.performed += ctx =>
        {
            if (isDrawing)
            {
                Vector2 position = ctx.ReadValue<Vector2>();
                pointsToDraw.Add(position);
            }
        };
    }

    void Start()
    {
        var rectTransform = GetComponent<RectTransform>();
        texture = new Texture2D((int)rectTransform.rect.width, (int)rectTransform.rect.height);
        ClearTexture(); // Clears the texture to start with a blank state
        GetComponent<Image>().material.mainTexture = texture;
    }

    private void OnEnable()
    {
        drawingActions.Enable();
    }

    private void OnDisable()
    {
        drawingActions.Disable();
    }

    private void EndDrawing()
    {
        isDrawing = false;
        lastPosition = Vector2.negativeInfinity; // Reset last position when drawing ends
    }

    void Update()
    {
        if (isDrawing && pointsToDraw.Count > 0)
        {
            foreach (var point in pointsToDraw)
            {
                Draw(point);
            }
            texture.Apply(); // Apply all SetPixel calls once per frame
            pointsToDraw.Clear(); // Clear the list after drawing
        }
    }

    private void Draw(Vector2 screenPosition)
    {
        Vector2 localPosition = ScreenToLocal(screenPosition);
        if (lastPosition != Vector2.negativeInfinity)
        {
            DrawLine(lastPosition, localPosition, strokeThickness);
        }
        lastPosition = localPosition;
    }

    private Vector2 ScreenToLocal(Vector2 screenPosition)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), screenPosition, null, out Vector2 localPoint);
        return new Vector2(localPoint.x + texture.width / 2, localPoint.y + texture.height / 2);
    }

    private void DrawLine(Vector2 start, Vector2 end, int thickness)
    {
        Vector2 difference = end - start;
        float steps = difference.magnitude / (thickness / 2f);
        Vector2 increment = difference / steps;

        for (float i = 0; i <= steps; i++)
        {
            DrawVector(start + increment * i, thickness);
        }
    }

    void DrawVector(Vector2 point, int thickness)
    {
        int centerX = (int)point.x;
        int centerY = (int)point.y;
        int radius = thickness / 2;

        for (int x = -radius; x <= radius; x++)
        {
            for (int y = -radius; y <= radius; y++)
            {
                if (x * x + y * y <= radius * radius)
                {
                    int drawX = centerX + x;
                    int drawY = centerY + y;
                    if (drawX >= 0 && drawY >= 0 && drawX < texture.width && drawY < texture.height)
                    {
                        texture.SetPixel(drawX, drawY, Color.black);
                    }
                }
            }
        }
        // Note: texture.Apply() is now correctly inside the Update method
    }

    private void ClearTexture()
    {
        Color[] clearColorArray = new Color[texture.width * texture.height];
        for (int i = 0; i < clearColorArray.Length; i++)
        {
            clearColorArray[i] = Color.clear;
        }
        texture.SetPixels(clearColorArray);
        texture.Apply();
    }
}

