using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Marker : MonoBehaviour
{
    [SerializeField] public Transform _tip;
    [SerializeField] public int _pensize = 2;
    [SerializeField] public Color[] _colors;


    private Renderer _renderer;
    private float _tipHeight;

    //private RaycastHit _touch;
    private WhiteBoard _whiteboard;
    private Vector2 _touchPos, _lastTouchPos;
    private bool _touchLastFrame;
    private Quaternion _lastTouchRot;

    // Start is called before the first frame update
    void Start()
    {
        _colors = Enumerable.Repeat(Color.blue, _pensize * _pensize).ToArray();

    }

    // Update is called once per frame
    void Update()
    {
        //Draw();
    }

    public void Draw(RaycastHit _touch)
    {
            Debug.Log(_touch.transform.tag);
        if (_touch.transform.CompareTag("WhiteBoard"))
        {
            if (_whiteboard == null)
            {
                _whiteboard = _touch.transform.GetComponent<WhiteBoard>();

            }

            _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

            var x = (int)(_touchPos.x * _whiteboard.texture.width);
            var y = (int)(_touchPos.y * _whiteboard.texture.height);

            print("x=" + x + "\ny=" + y);
            print("height=" + _whiteboard.texture.height + "\nwidth=" + _whiteboard.texture.width);

            if (y <= 0 || y >= _whiteboard.texture.height || x <= 0 || x >= _whiteboard.texture.width)
            {
                print("error out of board");
                return;
            }

            if (_touchLastFrame)
            {
                _whiteboard.texture.SetPixels(x, y, _pensize, _pensize, _colors);

                for (float f = 0.01f; f < 1.00; f += 0.03f)
                {
                    var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                    var lerpy = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                    _whiteboard.texture.SetPixels(lerpX, lerpy, _pensize, _pensize, _colors);
                }

                transform.rotation = _lastTouchRot;

                _whiteboard.texture.Apply();
            }

            _lastTouchPos = new Vector2(x, y);
            _lastTouchRot = transform.rotation;
            _touchLastFrame = true;
            return;
        }


        _whiteboard = null;
        _touchLastFrame = false;
    }
}
