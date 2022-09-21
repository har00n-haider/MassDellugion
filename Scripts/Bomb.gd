extends RigidBody


export var explosion_force : float
export var upward_force : float


export var explosion_particles : Resource

var items_in_radius : Array


func _ready():
	randomize() # WTF is this ?

		

# Need to connect the _on_XXX handler methods to the relevant nodes via the GUI > Node > Signals menu


# Adding the rigidbodies to an array that entered into the explosion area.
func _on_Area_body_entered(body):

	if body.name != self.name:
		if body is RigidBody:
			items_in_radius.append(body)

# Deleting the rigidbodies from the array that exited the explosion area.
func _on_Area_body_exited(body):

	if body is RigidBody:
		items_in_radius.erase(body)

func explosion():
	var force_dir : Vector3
	var random_vector : Vector3
	
	#Applying the explosion force for every Rigidbody in the array.
	for j in items_in_radius:
		#Getting a direction vector between the bomb and all nearby RigidBodies. This line of code later helps to calculate a trajectory for the Rigidbodies.
		force_dir = self.translation.direction_to(j.translation)
		
		force_dir = force_dir + Vector3(0,1,0) * upward_force
		
		#Generating a position on the object where the force will be applied. This line of code makes the Rigidbodies randomly rotate after the explosion.
		random_vector = Vector3(rand_range(0, 1), rand_range(0, 1), rand_range(0, 1)) * force_dir
		j.apply_impulse(random_vector, force_dir * explosion_force)

# Time to explode! 
func _on_Timer_timeout():

	explosion()
	
	# stop showing the bomb model	
	remove_child($MeshInstance)
	remove_child($CollisionShape)
	
	#Explosion particles made by drcd1. Here is the link: https://github.com/drcd1/GodotSimpleExplosionVFX
	var particles = explosion_particles.instance()
	self.get_parent().add_child(particles)
	particles.translation = self.translation
	particles.emitting = true
	
	$Audio.play()


func _on_Audio_finished():
	queue_free()

