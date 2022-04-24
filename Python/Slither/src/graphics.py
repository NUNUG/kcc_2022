import pygame
from settings import Settings
from settings import Paths


class Graphics:
	def __init__(self):
		self.load_graphics()
	def load_graphics(self):
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
		self.tail : pygame.surface = [
			pygame.transform.scale(pygame.image.load(Paths.GRAPHICS_TONGUE_UP_PATH), (Settings.BLOCK_SIZE * Settings.SCALE, Settings.BLOCK_SIZE * Settings.SCALE)),
			pygame.transform.scale(pygame.image.load(Paths.GRAPHICS_TONGUE_DOWN_PATH), (Settings.BLOCK_SIZE * Settings.SCALE, Settings.BLOCK_SIZE * Settings.SCALE)),
			pygame.transform.scale(pygame.image.load(Paths.GRAPHICS_TONGUE_LEFT_PATH), (Settings.BLOCK_SIZE * Settings.SCALE, Settings.BLOCK_SIZE * Settings.SCALE)),
			pygame.transform.scale(pygame.image.load(Paths.GRAPHICS_TONGUE_RIGHT_PATH), (Settings.BLOCK_SIZE * Settings.SCALE, Settings.BLOCK_SIZE * Settings.SCALE))
		]
		self.steak = pygame.transform.scale(pygame.image.load(Paths.GRAPHICS_STEAK_PATH), (Settings.BLOCK_SIZE * Settings.SCALE, Settings.BLOCK_SIZE * Settings.SCALE))
		self.block = pygame.transform.scale(pygame.image.load(Paths.GRAPHICS_BLOCK_PATH), (Settings.BLOCK_SIZE * Settings.SCALE, Settings.BLOCK_SIZE * Settings.SCALE))
		