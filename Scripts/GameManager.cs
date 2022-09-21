using Godot;
using System;

public class GameManager : Spatial
{

	[Export]
	private PackedScene enemy;

	private CollisionShape enemySpawnVolume; // can't export variables to hold nodes in an instance of a scene

	[Export]
	private float enemySpawnRate;
	private float enemySpawnCounter;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("game manager running");

		enemySpawnVolume = GetNode("EnemySpawnVolume") as CollisionShape;

		if (enemySpawnVolume == null) GD.Print("spawn volume not loaded!!");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{

		enemySpawnCounter += GetProcessDeltaTime();
		if(enemySpawnCounter > enemySpawnRate)
		{
			Spatial enemySpatial = enemy.Instance() as Spatial;
			enemySpatial.Translation = GetEnemyPosition();
			AddChild(enemySpatial);
			enemySpawnCounter = 0;				
		}

	}


	private Vector3 GetEnemyPosition()
	{
		BoxShape boxShape = enemySpawnVolume.Shape as BoxShape;
		Vector3 position = new Vector3();

		position.x = (float) GD.RandRange(-boxShape.Extents.x, boxShape.Extents.x);
		position.y = (float) GD.RandRange(-boxShape.Extents.y, boxShape.Extents.y);
		position.z = (float) GD.RandRange(-boxShape.Extents.z, boxShape.Extents.z);

		position += enemySpawnVolume.Translation;

		return position;
	}
}
