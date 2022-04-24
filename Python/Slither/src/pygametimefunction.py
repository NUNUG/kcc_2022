import pygame

def pygame_time_function():
	"""This is a function used by the Cooldown class.  It uses the PyGame timer.  
	When we do it this way, the Cooldown class doesn't have to know about PyGame.  
	Keeping things separate like this is an important programming concept."""
	return pygame.time.get_ticks()
	
