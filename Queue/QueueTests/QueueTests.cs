namespace QueueTests;

using Queue;

public class QueueTests
{
    [Fact]
    public void Enqueue_As_FIFO()
    {
        Queue<int> queue = new();
        queue.Enqueue(5);
        queue.Enqueue(10);

        Assert.Equal(5, queue.Dequeue());
    }

    [Fact]
    public void Empty_ShouldBeEmpty()
    {
        Queue<int> queue = new();

        Assert.True(queue.IsEmpty());
    }
    
    [Fact]
    public void NotEmpty_ShouldBeNotEmpty()
    {
        Queue<int> queue = new();
        queue.Enqueue(5);

        Assert.False(queue.IsEmpty());
    }

    [Fact]
    public void HasChangedCount()
    {
        Queue<int> queue = new();
        queue.Enqueue(5);
        queue.Enqueue(6);

        Assert.Equal(2, queue.Count);

        queue.Enqueue(7);

        Assert.Equal(3, queue.Count);
    }

    [Fact]
    public void CanBeCleared()
    {
        Queue<int> queue = new();
        queue.Enqueue(5);
        queue.Enqueue(4);
        queue.Enqueue(5);

        Assert.Equal(3, queue.Count);

        queue.Clear();

        Assert.Equal(0, queue.Count);
    }

    [Fact]
    public void Empty_TryDequeue_ShouldThrowException()
    {
        Queue<int> queue = new();
        
        bool caughtExpection = false;
        try
        {
            queue.Dequeue();
        }
        catch (IndexOutOfRangeException)
        {
            caughtExpection = true;
        }

        Assert.True(caughtExpection);
    }
}
