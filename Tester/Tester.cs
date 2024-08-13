// Provided as part of GetNodeAutoProp under MIT license, (c) 2024 Dominik Kaszewski

using GetNodeAutoProp;

Console.WriteLine("Hello World!");

class Node2D
{
    public T GetNode<T>(string path) => default(T)!;
}

class ExportAttribute : Attribute {}

class AnimatedSprite2D {}

class RayCast2D {}

partial class BouncingEnemy : Node2D
{
    [Export]
    public float Speed { get; private set; } = 50.0f;

    [GetNode("./EnemySprite")]
    private AnimatedSprite2D? _sprite;
    [GetNode]
    private RayCast2D? _rayCastLeft;
    [GetNode]
    private RayCast2D? _rayCastRight;
}

