using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : MonoBehaviour
{
    private Vector2 _direction = Vector2.down;
    public FoodSpawnerScript _foodSpawner;
    private List<GameObject> _segments;
    public GameObject _bodyPrefab;
    public GameObject _tailPrefab;
    public Sprite _tailSpriteUp;
    public Sprite _tailSpriteDown;
    public Sprite _tailSpriteLeft;
    public Sprite _tailSpriteRight;
    public Sprite _headSpriteUp;
    public Sprite _headSpriteDown;
    public Sprite _headSpriteLeft;
    public Sprite _headSpriteRight;
    public Sprite _bodySpriteHorizontal;
    public Sprite _bodySpriteVertical;
    public Sprite _bodySpriteBottomLeft;
    public Sprite _bodySpriteBottomRight;
    public Sprite _bodySpriteTopLeft;
    public Sprite _bodySpriteTopRight;
    public SpriteRenderer _tailPrefabRenderer;
    public SpriteRenderer _bodyPrefabRenderer;
    private SpriteRenderer _headRenderer;
    public LogicScript _logic;
    private bool _isGameOver;

    private void Start()
    {
        _segments = new List<GameObject>();
        _segments.Add(this.gameObject);
        _tailPrefabRenderer.sprite = _tailSpriteDown;
        GameObject tailSegment = Instantiate(this._tailPrefab);
        tailSegment.transform.position = _segments[_segments.Count - 1].transform.position;
        tailSegment.transform.position = new Vector3(
                tailSegment.transform.position.x,
                tailSegment.transform.position.y +1,
                0.0f
            );
        _segments.Add(tailSegment);
        _headRenderer = gameObject.GetComponent<SpriteRenderer>();
        _isGameOver = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && _direction != Vector2.down)
        {
            _direction = Vector2.up;
        } else if (Input.GetKeyDown(KeyCode.D) && _direction != Vector2.left)
        {
            _direction = Vector2.right;
        } else if (Input.GetKeyDown(KeyCode.A) && _direction != Vector2.right)
        {
            _direction = Vector2.left;
        } else if (Input.GetKeyDown(KeyCode.S) && _direction != Vector2.up)
        {
            _direction = Vector2.down;
        }
    }

    private void FixedUpdate()
    {
        if (!_isGameOver && !_logic._isWelcomeScreen)
        {
            for (int i = _segments.Count - 1; i > 0; i--)
            {
                _segments[i].transform.position = _segments[i - 1].transform.position;
            }
            this.transform.position = new Vector3(
                Mathf.Round(this.transform.position.x) + _direction.x,
                Mathf.Round(this.transform.position.y) + _direction.y,
                0.0f
            );

            UpdateTailSprite(_segments);
            UpdateBodySprite(_segments);
            UpdateHeadSprite(_segments);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            Destroy(collision.gameObject);
            _foodSpawner.spawnRandomFood(_foodSpawner.food);
            Grow();
            _logic.IncrementScore();
        }
        else if ((collision.gameObject.layer == 6 || collision.gameObject.layer == 7 || collision.gameObject.layer == 8) && _logic._isWelcomeScreen == false)
        {
            _isGameOver = true;
            _logic.GameOver();
        }
    }

    private void Grow()
    {
        GameObject bodySegment = Instantiate(this._bodyPrefab);
        bodySegment.transform.position = _segments[_segments.Count - 2].transform.position;
        _segments.Insert(_segments.Count - 1, bodySegment);
    }

    private void UpdateTailSprite(List<GameObject> seg)
    {
        if ((seg[seg.Count-1].transform.position.y - seg[seg.Count - 2].transform.position.y) == -1)
        {
            _tailPrefabRenderer.sprite = _tailSpriteUp;
        }

        else if ((seg[seg.Count - 1].transform.position.y - seg[seg.Count - 2].transform.position.y) == 1)
        {
            _tailPrefabRenderer.sprite = _tailSpriteDown;
        }

        else if ((seg[seg.Count - 1].transform.position.x - seg[seg.Count - 2].transform.position.x) == 1)
            {
            _tailPrefabRenderer.sprite = _tailSpriteLeft;
        }

        else if ((seg[seg.Count - 1].transform.position.x - seg[seg.Count - 2].transform.position.x) == -1)
        {
            _tailPrefabRenderer.sprite = _tailSpriteRight;
        }
        GameObject tailSegment = Instantiate(this._tailPrefab);
        tailSegment.transform.position = _segments[_segments.Count - 1].transform.position;
        Destroy(_segments[_segments.Count - 1]);
        _segments.RemoveAt(_segments.Count - 1);
        _segments.Add(tailSegment);
        
    }

    private void UpdateHeadSprite(List<GameObject> seg)
    {
        if ((seg[1].transform.position.y - seg[0].transform.position.y) == -1)
        {
            _headRenderer.sprite = _headSpriteUp;
        }

        else if ((seg[1].transform.position.y - seg[0].transform.position.y) == 1)
        {
            _headRenderer.sprite = _headSpriteDown;
        }

        else if ((seg[1].transform.position.x - seg[0].transform.position.x) == 1)
        {
            _headRenderer.sprite = _headSpriteLeft;
        }

        else if ((seg[1].transform.position.x - seg[0].transform.position.x) == -1)
        {
            _headRenderer.sprite = _headSpriteRight;
        }
    }

        private void UpdateBodySprite(List<GameObject> seg)
    {
        for (int i = _segments.Count - 2; i > 0; i--)
        {

            if (((seg[i].transform.position.x - seg[i - 1].transform.position.x) == -1 && (seg[i].transform.position.y - seg[i + 1].transform.position.y) == 1) ||
                ((seg[i].transform.position.x - seg[i + 1].transform.position.x) == -1 && (seg[i].transform.position.y - seg[i - 1].transform.position.y) == 1))
            {
                _bodyPrefabRenderer.sprite = _bodySpriteTopLeft;
            }

            else if (((seg[i].transform.position.x - seg[i - 1].transform.position.x) == -1 && (seg[i].transform.position.y - seg[i + 1].transform.position.y) == -1) ||
                ((seg[i].transform.position.x - seg[i + 1].transform.position.x) == -1 && (seg[i].transform.position.y - seg[i - 1].transform.position.y) == -1))
            {
                _bodyPrefabRenderer.sprite = _bodySpriteBottomLeft;
            }

            else if (((seg[i].transform.position.x - seg[i - 1].transform.position.x) == 1 && (seg[i].transform.position.y - seg[i + 1].transform.position.y) == -1) ||
                ((seg[i].transform.position.x - seg[i + 1].transform.position.x) == 1 && (seg[i].transform.position.y - seg[i - 1].transform.position.y) == -1))
            {
                _bodyPrefabRenderer.sprite = _bodySpriteBottomRight;
            }

            else if (((seg[i].transform.position.x - seg[i - 1].transform.position.x) == 1 && (seg[i].transform.position.y - seg[i + 1].transform.position.y) == 1) ||
                ((seg[i].transform.position.x - seg[i + 1].transform.position.x) == 1 && (seg[i].transform.position.y - seg[i - 1].transform.position.y) == 1))
            {
                _bodyPrefabRenderer.sprite = _bodySpriteTopRight;
            }

            else if ((seg[i].transform.position.y - seg[i - 1].transform.position.y) == -1 || (seg[i].transform.position.y - seg[i - 1].transform.position.y) == 1)
            {
                _bodyPrefabRenderer.sprite = _bodySpriteVertical;
            }

            else if ((seg[i].transform.position.x - seg[i - 1].transform.position.x) == -1 || (seg[i].transform.position.x - seg[i - 1].transform.position.x) == 1)
            {
                _bodyPrefabRenderer.sprite = _bodySpriteHorizontal;
            }

            GameObject bodySegment = Instantiate(this._bodyPrefab);
            bodySegment.transform.position = _segments[i].transform.position;
            Destroy(_segments[i]);
            _segments.RemoveAt(i);
            _segments.Insert(i, bodySegment);
        }

    }

}
