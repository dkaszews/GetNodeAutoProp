# GetNodeAutoProp
Incremental C# source code generator for Godot's GetNode fields, inspired by [Godot Proposal #2425](https://github.com/godotengine/godot-proposals/issues/2425).

## Quick start
```cs
using GetNodeAutoProp;

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

    public override void _Process(double delta)
    {
        if (RayCastLeft.IsColliding())
        {
            Sprite.FlipH = false;
        }
        else if (RayCastRight.IsColliding())
        {
            Sprite.FlipH = true;
        }

        int direction = Sprite.FlipH ? -1 : 1;
        Position += new Vector2((float)(delta * Speed * direction), 0);
    }
}
```


## Usage
1. Include `GetNodeAutoProp` namespace
1. Declare your dependencies as nullable fields with `[GetNodeAttribute("./path/to/node")]`
1. Upon compilation, each fields will generate a property with same name, but capitalized and stripped of underscores
1. The path can be omitted, defaults to the same name as generated property

The example code from quick start will generate the following file:

```cs
partial class BouncingEnemy
{
    private AnimatedSprite2D Sprite => _sprite ??= GetNode<AnimatedSprite2D>("./EnemySprite");
    private RayCast2D RayCastLeft => _rayCastLeft ??= GetNode<RayCast2D>("RayCastLeft");
    private RayCast2D RayCastRight => _rayCastRight ??= GetNode<RayCast2D>("RayCastRight");
}

```


## Installation
1. Download the `Generator` folder and put it inside your project, e.g. in `plugins`
1. Open your game's `.csproj` file, in Godot requires changing "All recognized" to "All" in "Open file" dialog
1. Change the `<TargetFramework>` to `net8.0` or higher
1. Add the following at the end of `<Project>` section:
```xml
  <ItemGroup>
    <ProjectReference
      Include="plugins/Generator/Generator.csproj"
      OutputItemType="Analyzer"
      ReferenceOutputAssembly="true"
    />
    <Compile Remove="plugins/Generator/**/*.cs" />
  </ItemGroup>
1. Recompile the project
```


## FAQ
**Q:** How can I see the generated code? <br />
**A:** Add the following to the `<PropertyGroup>` section of your game's `.csproj` file and recompile the project:
```xml
<EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>
```

**Q:** Why my IDE does not recognize generated properties? <br />
**A:** Code completion engines don't always play nicely with generated code.
You may need to reload the completion engine or restart your IDE completely.
This may be required just once per project, after a new file is added, or every time you add new `[GetNode]` field.

* For (Neo)vim [YCM](https://github.com/ycm-core/YouCompleteMe) use `:YcmRestartServer`

