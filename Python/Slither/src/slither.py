###############################################################################
# Slither Step 2 - PyGame and Game Loop
###############################################################################
# We add Pygame, create a window and an empty game loop
###############################################################################

# PyGame imports
import pygame
from pygame.locals import *
import sys
from direction import DIRECTION_RIGHT
from graphics import Graphics

# Slither game imports
from slithergame import SlitherGame
from snake import Snake
from mazemaker import MazeMaker
from settings import *

pygame.init()

###############################################################################
# PyGame Setup
###############################################################################
# Create a screen that we can draw on.
screen_rows = 50
screen_columns = 80
screen_size = (screen_columns * Settings.BLOCK_SIZE * Settings.SCALE, screen_rows * Settings.BLOCK_SIZE * Settings.SCALE)
screen = pygame.display.set_mode([screen_size[0], screen_size[1]])

# Game speed
fps = 60

###############################################################################
# Slither Game Setup
###############################################################################

maze_maker = MazeMaker()
maze = maze_maker.first_maze()
snake = Snake(1, Settings.INITIAL_SIZE, maze.initial_position, DIRECTION_RIGHT)
game = SlitherGame(maze_maker, snake)
graphics = Graphics()

maze = maze_maker.first_maze()

# Main Game Loop
while True:
	# Wait until time has passed before drawing the screen again.
	pygame.time.Clock().tick(fps)

	# Look for user input in case they want to quit the game.
	for event in pygame.event.get():
		# Pay attention if the user clicks the X to quit.
		if event.type == pygame.QUIT:
			sys.exit()
		# Check the keyboard for keypresses. 
		if event.type ==pygame.KEYDOWN:
			if event.key == K_ESCAPE:
				sys.exit()

	for x in range(maze.maze_width):
		for y in range(maze.maze_height):
			block = maze.get_block(x, y)
			if block == maze.BLOCKTYPE_WALL:
				# Draw a wall.
				screen.blit(graphics.block, (
					x * Settings.BLOCK_SIZE * Settings.SCALE, 
					y * Settings.BLOCK_SIZE * Settings.SCALE,
					x * Settings.BLOCK_SIZE * Settings.SCALE + Settings.BLOCK_SIZE * Settings.SCALE, 
					y * Settings.BLOCK_SIZE * Settings.SCALE + Settings.BLOCK_SIZE * Settings.SCALE
				))

	# Show our screen on the monitor.
	pygame.display.update()
