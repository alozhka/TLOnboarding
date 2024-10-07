using System.Drawing;

namespace Queue;

public class Queue<T>
{
    private T[] Buffer { get; set; }
    private int Head { get; set; }
    private int Tail { get; set; }
    public int Count;

    private const uint _defaultLength = 20;

    public Queue()
    {
        Buffer = new T[_defaultLength];
        Head = 0;
        Tail = 0;
        Count = 0;
    }

    public void Enqueue(T element)
    {
        Buffer[Tail] = element;
        Tail++;
    }

    public T Dequeue()
    {
        return Buffer[Head++]; 
    }

    public bool IsEmpty() => Count == 0; 

    public void Clear()
    {
        Buffer = [];
        Head = 0;
        Tail = 0;
        Count = 0;
    }
}