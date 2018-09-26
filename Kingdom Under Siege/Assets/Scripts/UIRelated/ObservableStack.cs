using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//a delegate for creating an event
public delegate void UpdateStackEvent();

public class ObservableStack<T> : Stack<T>
{
    //event for when we push something
    public event UpdateStackEvent OnPush;
    //event for when we pop something
    public event UpdateStackEvent OnPop;
    //event for when we clear the stack
    public event UpdateStackEvent OnClear;

    public ObservableStack(Stack<T> items) : base(items)
    {

    }

    public ObservableStack()
    {

    }

    public new void Push(T item)
    {
        base.Push(item);

        if (OnPush != null)//makes sure something is listening to the event
        {
            OnPush();//calls the event
        }
    }

    public new T Pop()
    {
        T item = base.Pop();

        if (OnPop != null)//makes sure something is listening to the event
        {
            OnPop();//calls the event
        }

        return item;
    }

    public new void Clear()
    {
        base.Clear();

        if (OnClear != null)//makes sure something is listening to the event
        {
            OnClear();//calls the event
        }
    }
}
