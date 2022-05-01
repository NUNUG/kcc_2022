###############################################################################
# Slither Step 8
###############################################################################
# Previous step:
# - Draw the snake body.
#
# What to do now?
# - Draw the steak
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

	# Move the snake.
	# We have to create this method in the Game class!
	game.tick()

	# Draw the maze
	maze = game.maze
	for x in range(maze.maze_width):
		for y in range(maze.maze_height):
			block = maze.get_block(x, y)
			if block == maze.BLOCKTYPE_WALL:
				# Draw a wall.  We draw a block every 8 pixels (because blocks 
				# are 8x8), but then we double it because scale is 2x.
				draw_block(x, y, graphics.wall)

	# # Draw the snake
	# ... the body
	first_block_num = 0
	last_block_num = len(snake.blocks) - 1
	for block_num in range(last_block_num):
		block = snake.blocks[block_num]
		if (block_num != first_block_num) and (block_num != last_block_num):
			(position, direction) = block
			(x, y) = position
			image = graphics.body[direction]
			draw_block(x, y, image)
	# ...the tail
	(position, direction) = snake.blocks[0]
	(x, y) = position
	draw_block(x, y, graphics.tail[direction])

	# ...the head
	(position, direction) = snake.blocks[len(snake.blocks) - 1]
	(x, y) = position
	draw_block(x, y, graphics.head[direction])

	# Show our screen on the monitor.
	pygame.display.update()