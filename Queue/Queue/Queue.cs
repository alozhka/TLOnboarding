using System.Drawing;

namespace Queue;

// TODO: добавить тест на FIFO
// TODO: добавить расширение по необходимости
public class Queue<T>
{
    private T[] Buffer { get; set; }
    private int HeadIndex { get; set; }
    private int TailIndex { get; set; }
    public int Count;

    private const uint _defaultLength = 20;

    public Queue(uint initialSize = _defaultLength)
    {
        Buffer = new T[_defaultLength];
        HeadIndex = 0;
        TailIndex = 0;
        Count = 0;
    }

    public void Enqueue(T element)
    {
        Buffer[TailIndex++] = element;
        Count++;
        if (TailIndex > _defaultLength)
        {
            TailIndex = 0;
        }
    }

    public T Dequeue()
    {
        if (Count < 1)
        {
            // TODO: Текст исключения на английском
            throw new IndexOutOfRangeException("Очередь пустая");
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
}