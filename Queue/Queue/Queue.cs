using System.Diagnostics;

namespace Queue;

public class Queue<T>(int initialSize = 20)
{
    private T[] Buffer = new T[initialSize];
    private int HeadIndex = 0;
    private int TailIndex = 0;
    public int Capacity => Buffer.Length;
    public int Count() => (TailIndex - HeadIndex + Capacity) % Capacity;


    public void Enqueue(T element)
    {
        Buffer[TailIndex] = element;
        TailIndex = (TailIndex + 1) % Capacity;

        if (TailIndex == HeadIndex)
        {
            IncreaseBufferCapacity();
        }
    }

    public T Dequeue()
    {
        if (Count() < 1)
        {
            throw new IndexOutOfRangeException("Queue is empty");
        }

        T element = Buffer[HeadIndex];
        HeadIndex = (HeadIndex + 1) % Capacity;

        return element;
    }

    public bool IsEmpty() => Count() == 0;

    public void Clear()
    {
        Buffer = new T[Capacity];
        HeadIndex = 0;
        TailIndex = 0;
    }

    private void IncreaseBufferCapacity()
    {
        TailIndex = (TailIndex - 1 + Capacity) % Capacity;
        T[] temp = new T[Buffer.Length + initialSize];

        int bufferIndex = HeadIndex, tempIndex = 0;
        
        for (; bufferIndex != TailIndex;
        bufferIndex = (bufferIndex + 1) % Capacity, tempIndex++)
        {
            temp[tempIndex] = Buffer[bufferIndex];
        }
        temp[tempIndex] = Buffer[bufferIndex];

        HeadIndex = 0;
        TailIndex = Capacity;
        Buffer = temp;
    }
}