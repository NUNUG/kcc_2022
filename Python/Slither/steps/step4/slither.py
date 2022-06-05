###############################################################################
# Slither Step 4
###############################################################################
# Previous step:
# - Draw the walls.
#
# What to do now?
# - Draw the snake tail.
###############################################################################

# PyGame imports
import pygame
from pygame.locals import *
import sys

# Slither game imports
from slithergame import SlitherGame
from snake import *
from mazemaker import *
from settings import *
from direction import *
from graphics import *
from sounds import *


pygame.init()

###############################################################################
# PyGame Setup
###############################################################################
# Create a screen that we can draw on.
screen_rows = 25
screen_columns = 40
screen_size = (screen_columns * Settings.BLOCK_SIZE * Settings.SCALE, screen_rows * Settings.BLOCK_SIZE * Settings.SCALE)
screen = pygame.display.set_mode([screen_size[0], screen_size[1]])
pygame.display.set_caption("Slither!")

# Game speed
fps = 60

###############################################################################
# Slither Game Setup
###############################################################################

maze_maker = MazeMaker()
snake = Snake(1, Settings.INITIAL_SIZE, (0, 0), DIRECTION_RIGHT)
graphics = Graphics()
sounds = Sounds()
game = SlitherGame(maze_maker, snake, sounds)

def draw_block(x: int, y: int, block : pygame.surface):
	screen.blit(block, (
		x * Settings.BLOCK_SIZE * Settings.SCALE, 
		y * Settings.BLOCK_SIZE * Settings.SCALE
	))

###############################################################################
# Main Game Loop
###############################################################################
while True:
	# Wait until time has passed before drawing the screen again.
	pygame.time.Clock().tick(fps)

	# Look for user input in case they want to quit the game.
	for event in pygame.event.get():
		# Pay attention if the user clicks the X to quit.
		if event.type == pygame.QUIT:
			sys.exit()
		
		# Check the keyboard for keypresses. 
		if event.type == pygame.KEYDOWN:
			if event.key == K_ESCAPE:
				sys.exit()

	# Draw a black background.
	screen.fill((0, 0, 0))

	# Draw the maze
	maze = game.maze
	for x in range(maze.maze_width):
		for y in range(maze.maze_height):
			block = maze.get_block(x, y)
			if block == maze.BLOCKTYPE_WALL:
				# Draw a wall.  We draw a block every 8 pixels (because blocks 
				# are 8x8), but then we double it because scale is 2x.
				draw_block(x, y, graphics.wall)

	# Show our screen on the monitor.
	pygame.display.update()