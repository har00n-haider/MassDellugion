using Godot;
using System;

public class Player : KinematicBody
{

	[Export]
	private float speed;

	[Export]
	private PackedScene bomb;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		GD.Print("hello bitch");        
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{

	}

	public override void _PhysicsProcess(float delta)
	{
		Vector3 direction = new Vector3();	

		if(Input.IsActionPressed("ui_right")) direction.z += 1;
		if(Input.IsActionPressed("ui_left")) direction.z += -1;
		if(Input.IsActionPressed("ui_up")) direction.x += 1;
		if(Input.IsActionPressed("ui_down")) direction.x += -1;

		MoveAndSlide(direction.Normalized() * speed);

		if (Input.IsActionJustPressed("fire")) DropBomb();
		
	}

	private void DropBomb()
	{
		Spatial bombSpatial = bomb.Instance() as Spatial;
		bombSpatial.Translation = Translation;
		GetNode("/root").AddChild(bombSpatial);
	}


	private void _on_HitBox_body_entered(Node body)
	{
		GD.Print(body.Name);
	}

}






