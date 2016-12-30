using UnityEngine;
using System.Collections;
using WindowsInput;
using System.Windows.Forms;
using System;

public class TestScript : MonoBehaviour {

    public int left = 5;
    public int right = 700;
    public int top = 120;
    public int buttom = 670;
    public float MouseSpeed = 5f;

    [System.Runtime.InteropServices.DllImport("user32")]
    private static extern bool SetCursorPos(int x, int y);

    [System.Runtime.InteropServices.DllImport("user32")]
    private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo); 

    const int MOUSEEVENTF_MOVE = 0x0001;
    const int MOUSEEVENTF_LEFTDOWN = 0x0002;
    const int MOUSEEVENTF_LEFTUP = 0x0004;
    const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
    const int MOUSEEVENTF_RIGHTUP = 0x0010;
    const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
    const int MOUSEEVENTF_MIDDLEUP = 0x0040;
    const int MOUSEEVENTF_ABSOLUTE = 0x8000;  
	// Use this for initialization

    NativeRECT boundry;

    int posx, posy;
    /// <summary>
    /// boundry of the moving area
    /// </summary>
    public struct NativeRECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    } ;
    void Awake()
    {
        boundry = new NativeRECT();
        boundry.left = left;
        boundry.right = right;
        boundry.top = top;
        boundry.bottom = buttom;
    }

    void Start()
    {
        StartCoroutine(MouseMove());
        StartCoroutine(MovePlayer());
        StartCoroutine(MouseClick());
    }
    /// <summary>
    ///  Move the mouse point
    /// </summary>
    /// <returns></returns>
    IEnumerator MouseMove()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            posx = UnityEngine.Random.Range(boundry.left, boundry.right);
            posy = UnityEngine.Random.Range(boundry.top, boundry.bottom);

            SetCursorPos(posx, posy);
            yield return new WaitForSeconds(0.3f);
        }
    }
    /// <summary>
    /// Virtual key input
    /// </summary>
    /// <returns></returns>
    IEnumerator MovePlayer()
    {
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            // Randomly generate the keyboard input
            float pressTime = UnityEngine.Random.Range(0.5f, 1.5f);
            int direction = UnityEngine.Random.Range(1, 9);
            switch (direction)
            {
                case 1:
                    InputSimulator.SimulateKeyDown(VirtualKeyCode.LEFT);
                    yield return new WaitForSeconds(pressTime);
                    InputSimulator.SimulateKeyUp(VirtualKeyCode.LEFT);
                    break;
                case 2:
                    InputSimulator.SimulateKeyDown(VirtualKeyCode.RIGHT);
                    yield return new WaitForSeconds(pressTime);
                    InputSimulator.SimulateKeyUp(VirtualKeyCode.RIGHT);
                    break;
                case 3:
                    InputSimulator.SimulateKeyDown(VirtualKeyCode.UP);
                    yield return new WaitForSeconds(pressTime);
                    InputSimulator.SimulateKeyUp(VirtualKeyCode.UP);
                    break;
                case 4:
                    InputSimulator.SimulateKeyDown(VirtualKeyCode.DOWN);
                    yield return new WaitForSeconds(pressTime);
                    InputSimulator.SimulateKeyUp(VirtualKeyCode.DOWN);
                    break;
                case 5:
                    InputSimulator.SimulateKeyDown(VirtualKeyCode.LEFT);
                    InputSimulator.SimulateKeyDown(VirtualKeyCode.UP);
                    yield return new WaitForSeconds(pressTime);
                    InputSimulator.SimulateKeyUp(VirtualKeyCode.LEFT);
                    InputSimulator.SimulateKeyUp(VirtualKeyCode.UP);
                    break;
                case 6:
                    InputSimulator.SimulateKeyDown(VirtualKeyCode.LEFT);
                    InputSimulator.SimulateKeyDown(VirtualKeyCode.DOWN);
                    yield return new WaitForSeconds(pressTime);
                    InputSimulator.SimulateKeyUp(VirtualKeyCode.LEFT);
                    InputSimulator.SimulateKeyUp(VirtualKeyCode.DOWN);
                    break;
                case 7:
                    InputSimulator.SimulateKeyDown(VirtualKeyCode.RIGHT);
                    InputSimulator.SimulateKeyDown(VirtualKeyCode.UP);
                    yield return new WaitForSeconds(pressTime);
                    InputSimulator.SimulateKeyUp(VirtualKeyCode.RIGHT);
                    InputSimulator.SimulateKeyUp(VirtualKeyCode.UP);
                    break;
                case 8:
                    InputSimulator.SimulateKeyDown(VirtualKeyCode.RIGHT);
                    InputSimulator.SimulateKeyDown(VirtualKeyCode.DOWN);
                    yield return new WaitForSeconds(pressTime);
                    InputSimulator.SimulateKeyUp(VirtualKeyCode.RIGHT);
                    InputSimulator.SimulateKeyUp(VirtualKeyCode.DOWN);
                    break;
                default:
                    break;
            }
        }
    }
    /// <summary>
    /// Virtual mouse click
    /// </summary>
    /// <returns></returns>
    IEnumerator MouseClick()
    {
        yield return new WaitForSeconds(1.4f);
        while (true)
        {
            int WillClick = UnityEngine.Random.Range(0, 2);
            if (WillClick == 1)
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
