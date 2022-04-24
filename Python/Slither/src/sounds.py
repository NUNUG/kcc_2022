import pygame
from settings import Paths

class Sounds:
	"""A class which contains all the sounds that we can plan in the game."""
	def __init__(self):
		self.load_sounds()
	def load_sounds(self):
		self.eat = pygame.mixer.Sound(Paths.SOUND_EAT_PATH)