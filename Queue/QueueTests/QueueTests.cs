namespace QueueTests;

using Queue;

public class QueueTests
{
    [Fact]
    public void Enqueue_ShoulBeDequeued()
    {
        Queue<int> queue = new();
        queue.Enqueue(5);

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
}
