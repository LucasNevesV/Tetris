using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    
    public GameObject shape;

    public ShapeSpawner spawner;
    public GridController gridController;
    public AudioController audioController;

    Vector3 LastPosition;
    Vector3 LastRotation;

    const int right = 1;
    const int left = -1;

    float elapsedTime = 1f;

    // Update is called once per frame
    void Update()
    {
        Fall();
        CheckMovement();
        CurrentPositionValidations();
        gridController.UpdateGrid(shape.transform);
    }

    void Fall()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= 1f)
        {
            LastPosition = shape.transform.position;
            shape.transform.position += Vector3.down;
            elapsedTime = 0;

            if (gridController.CheckShapeCollision(shape.transform))
            {
                shape.transform.position = LastPosition;
                gridController.UpdateGrid(shape.transform);
                
                ChangeShape();
            }

        }
    }

    void CheckMovement()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(right);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(left);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            float yPositionAfterMove = shape.transform.position.y + -1;

            if (yPositionAfterMove >= 0.5f)
                Move(0, true);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            Rotate();
        }
    }

    void Move(int horizontalMove, bool down = false)
    {
        LastPosition = shape.transform.position;
        LastRotation = shape.transform.eulerAngles;

        int verticalMove = down ? -1 : 0;
        shape.transform.position += new Vector3(horizontalMove, verticalMove);

        audioController.PlayerMove();
    }

    void Rotate()
    {
        LastPosition = shape.transform.position;
        LastRotation = shape.transform.eulerAngles;

        shape.transform.Rotate(new Vector3(0, 0, -90));

        audioController.PlayerRotate();
    }

    void ChangeShape()
    {
        CheckGameOver();
        gridController.CheckForTetris();

        spawner.Instanciar();
    }

    #region Validations

    void CurrentPositionValidations()
    {
        CheckHorizontalBoundaries();

        CheckIfHitBottom();
    }

    void CheckHorizontalBoundaries()
    {
        foreach (Transform unitBlock in shape.transform)
        {
            var currentPosition = unitBlock.position;

            bool shapeOverBoundaries = currentPosition.x < GameConfig.screenLeftBoundarie
                || currentPosition.x > GameConfig.screenLRightBoundarie;

            if (shapeOverBoundaries || gridController.CheckCollision(unitBlock))
            {
                shape.transform.position = LastPosition;
                shape.transform.eulerAngles = LastRotation;
                break;
            }
        }
    }

    void CheckIfHitBottom()
    {
        foreach (Transform unitBlock in shape.transform)
        {
            var currentPosition = unitBlock.position;

            if (currentPosition.y <= GameConfig.screenBottomBoundarie + 0.5f)
            {
                var position = shape.transform.position;

                shape.transform.position = new Vector3(position.x, position.y);

                gridController.UpdateGrid(shape.transform);

                ChangeShape();

                break;
            }
        }
    }

    void CheckGameOver()
    {
        bool gameOver = gridController.CheckForGameOver(shape.transform);

        if (gameOver)
        {
            gameObject.SetActive(false);
        }
    }

    #endregion
}
