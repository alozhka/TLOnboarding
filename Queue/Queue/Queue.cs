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
        /* Count++;
        if (Count > Buffer.Length)
        {
            IncreaseBufferCapacity();
        } */

        Buffer[TailIndex++] = element;
/* 
        if (TailIndex > _defaultLength)
        {
            TailIndex = 0;
        } */
    }

    public T Dequeue()
    {
        if (Count() < 1)
        {
            throw new IndexOutOfRangeException("Queue is empty");
        }

        T element = Buffer[HeadIndex++];
        /* Count--;
        if (HeadIndex > _defaultLength)
        {
            HeadIndex = 0;
        } */

        return element;
    }

    public bool IsEmpty() => Count() == 0;

    public void Clear()
    {
        Buffer = [];
        HeadIndex = 0;
        TailIndex = 0;
    }

    private void IncreaseBufferCapacity()
    {
        T[] temp = new T[Buffer.Length + initialSize];

        for (int i = 0; i < Buffer.Length; i++)
        {
            temp[i] = Buffer[i];
        }

        Buffer = temp;
    }
}