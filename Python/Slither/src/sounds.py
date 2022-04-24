import pygame
from settings import Paths

class Sounds:
	def __init__(self):
		self.load_sounds()
	def load_sounds(self):
		self.eat = pygame.mixer.Sound(Paths.SOUND_EAT_PATH)