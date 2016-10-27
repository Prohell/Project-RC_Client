using UnityEngine;
/// <summary>
/// Mediator
/// by TT
/// 2016-07-06 
/// </summary>
public interface IMediator : IInit, IDestroy { }

public abstract class Mediator<T> : IMediator where T : MonoBehaviour
{
    public T view;

    public abstract void OnDestroy();

    public abstract void OnInit();
}