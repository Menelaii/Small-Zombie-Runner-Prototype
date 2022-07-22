using System;

public class Catcher
{
    private static int _catchCount = 0;

    private bool _iCatched;

    public event Action TargetCatched;
    public event Action TargetReleased;

    public void Release(Human target)
    {
        if (_iCatched == false)
            return;

        _catchCount--;
        if (_catchCount <= 0)
        {
            target?.EnableMovement();
        }

        TargetReleased?.Invoke();
    }

    public void Catch(Human target)
    {
        target.DisableMovement();

        _iCatched = true;
        _catchCount++;

        TargetCatched?.Invoke();
    }
}
