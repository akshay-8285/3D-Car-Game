using UnityEngine;

public class EventCall : MonoBehaviour
{
    public void OnAcceleratePressed()
    {
        CarInputEvent.OnAccleratePressed?.Invoke();
        Debug.Log("Accelerate Pressed");
    }

    public void OnAccelerateReleased()
    {
        CarInputEvent.OnAcclerateReleased?.Invoke();
    }

    public void OnBrakePressed()
    {
        CarInputEvent.OnBrakePressed?.Invoke();
        Debug.Log("Brake Pressed");
    }

    public void OnBrakeReleased()
    {
        CarInputEvent.OnBrakeReleased?.Invoke();
    }

    public void OnLeftPressed()
    {
        CarInputEvent.OnLeftPressed?.Invoke();
    }

    public void OnLeftReleased()
    {
        CarInputEvent.OnLeftReleased?.Invoke();
    }

    public void OnRightPressed()
    {
        CarInputEvent.OnRightPressed?.Invoke();
    }

    public void OnRightReleased()
    {
        CarInputEvent.OnRightReleased?.Invoke();
    }
}
