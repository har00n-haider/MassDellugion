using Godot;
using System;

public class Bullet : RigidBody
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	[Export]
	private float speed;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	public override void _PhysicsProcess(float delta)
	{
		TranslateObjectLocal(new Vector3(speed * delta, 0, 0));
	}
}
