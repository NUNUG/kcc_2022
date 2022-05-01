###############################################################################
# Slither Step 2
###############################################################################
# Previous step:
# - PyGame imports and main game loop.
#
# What to do now?
# - Add the Slither configuration.
###############################################################################

# PyGame imports
import pygame
from pygame.locals import *
import sys

pygame.init()

###############################################################################
# PyGame Setup
###############################################################################
# Create a screen that we can draw on.
screen_rows = 25
screen_columns = 40
screen_size = (screen_columns * 8 , screen_rows * 8)
screen = pygame.display.set_mode([screen_size[0], screen_size[1]])
pygame.display.set_caption("Slither!")

# Game speed
fps = 60

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