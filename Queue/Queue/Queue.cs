using System.Drawing;

namespace Queue;

public class Queue<T>
{
    private T[] Buffer { get; set; }
    private int HeadIndex { get; set; }
    private int TailIndex { get; set; }
    public int Count;

    private const uint _defaultLength = 20;

    public Queue(uint initialSize = _defaultLength)
    {
        Buffer = new T[initialSize];
        HeadIndex = 0;
        TailIndex = 0;
        Count = 0;
    }

    public void Enqueue(T element)
    {
        Count++;
        if (Count > Buffer.Length)
        {
            IncreaseBufferCapacity();
        }

        Buffer[TailIndex++] = element;

        if (TailIndex > _defaultLength)
        {
            TailIndex = 0;
        }
    }

    public T Dequeue()
    {
        if (Count < 1)
        {
            throw new IndexOutOfRangeException("Queue is empty");
        }

        T element = Buffer[HeadIndex++];
        Count++;
        if (HeadIndex > _defaultLength)
        {
            HeadIndex = 0;
        }

        return element;
    }

    public bool IsEmpty() => Count == 0;

    public void Clear()
    {
        Buffer = [];
        HeadIndex = 0;
        TailIndex = 0;
        Count = 0;
    }

    private void IncreaseBufferCapacity()
    {
        T[] temp = new T[Buffer.Length];

        Buffer.Select((el, index) => temp[index] = el);

        Buffer = new T[temp.Length + _defaultLength];
        
        temp.Select((el, index) => Buffer[index] = el);
   }
}