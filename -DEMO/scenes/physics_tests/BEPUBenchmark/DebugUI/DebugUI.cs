using Godot;
using Deterministic.FixedPoint;
using BEPUphysics.Character;

public partial class DebugUI : Control
{
  RichTextLabel FPS; RichTextLabel Thresholds; RichTextLabel PhysicsBodyCount;

  public override void _Ready()
	{
    PhysicsBodyCount = (RichTextLabel)GetNode("VBoxContainer/PhysicsBodyCount");
    FPS = (RichTextLabel)GetNode("VBoxContainer/FPS");  
	}

	public override void _PhysicsProcess(double delta)
	{
    FPS.Text = "FPS: " + Engine.GetFramesPerSecond().ToString();
  } 

  public void UpdateStats(int entityCount)
  {
    PhysicsBodyCount.Text = "Physics Bodies: " + entityCount.ToString();
  }
} 
