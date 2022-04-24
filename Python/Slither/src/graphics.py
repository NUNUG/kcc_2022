import pygame
from settings import Settings
from settings import Paths


class Graphics:
	"""A class which contains all of the graphics we will display in the game."""
	def __init__(self):
		"""Set up the Graphics class."""
		self.load_graphics()
	def load_graphics(self):
		"""Create, load and scale each of the graphics.  Most of these graphics are snake body parts which exist 
		in 4 orientations, one for each direction.  To make the game easier, we load the images for all for 
		directions into a single array for each kind of body part.  The order that we load them tells us 
		their direction.  The order is UP, DOWN, LEFT, RIGHT.  It's easy to remember as if it was the Konami code.
		That way, we can find the head-up graphic just by saying: 
		graphics.head[DIRECTION_UP]"""
		self.head : pygame.surface = [
			pygame.transform.scale(pygame.image.load(Paths.GRAPHICS_HEAD_UP_PATH), (Settings.BLOCK_SIZE * Settings.SCALE, Settings.BLOCK_SIZE * Settings.SCALE)),
			pygame.transform.scale(pygame.image.load(Paths.GRAPHICS_HEAD_DOWN_PATH), (Settings.BLOCK_SIZE * Settings.SCALE, Settings.BLOCK_SIZE * Settings.SCALE)),
			pygame.transform.scale(pygame.image.load(Paths.GRAPHICS_HEAD_LEFT_PATH), (Settings.BLOCK_SIZE * Settings.SCALE, Settings.BLOCK_SIZE * Settings.SCALE)),
			pygame.transform.scale(pygame.image.load(Paths.GRAPHICS_HEAD_RIGHT_PATH), (Settings.BLOCK_SIZE * Settings.SCALE, Settings.BLOCK_SIZE * Settings.SCALE))
		]
		self.body : pygame.surface = [
			pygame.transform.scale(pygame.image.load(Paths.GRAPHICS_BODY_UP_PATH), (Settings.BLOCK_SIZE * Settings.SCALE, Settings.BLOCK_SIZE * Settings.SCALE)),
			pygame.transform.scale(pygame.image.load(Paths.GRAPHICS_BODY_DOWN_PATH), (Settings.BLOCK_SIZE * Settings.SCALE, Settings.BLOCK_SIZE * Settings.SCALE)),
			pygame.transform.scale(pygame.image.load(Paths.GRAPHICS_BODY_LEFT_PATH), (Settings.BLOCK_SIZE * Settings.SCALE, Settings.BLOCK_SIZE * Settings.SCALE)),
			pygame.transform.scale(pygame.image.load(Paths.GRAPHICS_BODY_RIGHT_PATH), (Settings.BLOCK_SIZE * Settings.SCALE, Settings.BLOCK_SIZE * Settings.SCALE))
		]
		self.tail : pygame.surface = [
			pygame.transform.scale(pygame.image.load(Paths.GRAPHICS_TAIL_UP_PATH), (Settings.BLOCK_SIZE * Settings.SCALE, Settings.BLOCK_SIZE * Settings.SCALE)),
			pygame.transform.scale(pygame.image.load(Paths.GRAPHICS_TAIL_DOWN_PATH), (Settings.BLOCK_SIZE * Settings.SCALE, Settings.BLOCK_SIZE * Settings.SCALE)),
			pygame.transform.scale(pygame.image.load(Paths.GRAPHICS_TAIL_LEFT_PATH), (Settings.BLOCK_SIZE * Settings.SCALE, Settings.BLOCK_SIZE * Settings.SCALE)),
			pygame.transform.scale(pygame.image.load(Paths.GRAPHICS_TAIL_RIGHT_PATH), (Settings.BLOCK_SIZE * Settings.SCALE, Settings.BLOCK_SIZE * Settings.SCALE))
		]
		self.tongue : pygame.surface = [
			pygame.transform.scale(pygame.image.load(Paths.GRAPHICS_TONGUE_UP_PATH), (Settings.BLOCK_SIZE * Settings.SCALE, Settings.BLOCK_SIZE * Settings.SCALE)),
			pygame.transform.scale(pygame.image.load(Paths.GRAPHICS_TONGUE_DOWN_PATH), (Settings.BLOCK_SIZE * Settings.SCALE, Settings.BLOCK_SIZE * Settings.SCALE)),
			pygame.transform.scale(pygame.image.load(Paths.GRAPHICS_TONGUE_LEFT_PATH), (Settings.BLOCK_SIZE * Settings.SCALE, Settings.BLOCK_SIZE * Settings.SCALE)),
			pygame.transform.scale(pygame.image.load(Paths.GRAPHICS_TONGUE_RIGHT_PATH), (Settings.BLOCK_SIZE * Settings.SCALE, Settings.BLOCK_SIZE * Settings.SCALE))
		]
		self.steak = pygame.transform.scale(pygame.image.load(Paths.GRAPHICS_STEAK_PATH), (Settings.BLOCK_SIZE * Settings.SCALE, Settings.BLOCK_SIZE * Settings.SCALE))
		self.block = pygame.transform.scale(pygame.image.load(Paths.GRAPHICS_BLOCK_PATH), (Settings.BLOCK_SIZE * Settings.SCALE, Settings.BLOCK_SIZE * Settings.SCALE))
		