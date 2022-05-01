###############################################################################
# Slither Step 3
###############################################################################
# Previous step:
# - Slither configuration and setup.
#
# What to do now?
# - Draw the walls.
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

	# Show our screen on the monitor.
	pygame.display.update()