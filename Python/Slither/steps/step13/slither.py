###############################################################################
# Slither Step 13
###############################################################################
# Previous step:
# - Detect steak collisions.
#
# What to do now?
# - Animate the tongue.
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
font = pygame.font.Font(Paths.FONT_PATH, 96)
bgfont = pygame.font.Font(Paths.FONT_PATH, 192)
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
			if event.key == K_UP:
				if snake.head_direction != DIRECTION_DOWN:
					snake.direction = DIRECTION_UP
			if event.key == K_DOWN:
				if snake.head_direction != DIRECTION_UP:
					snake.direction = DIRECTION_DOWN
			if event.key == K_LEFT:
				if snake.head_direction != DIRECTION_RIGHT:
					snake.direction = DIRECTION_LEFT
			if event.key == K_RIGHT:
				if snake.head_direction != DIRECTION_LEFT:
					snake.direction = DIRECTION_RIGHT

	# Draw a black background.
	screen.fill((0, 0, 0))

	# Move the snake.
	# We have to create this method in the Game class!
	game.tick()

	if game.game_over:
		screen.fill((192, 32, 32))
		game_over_text = font.render("GAME OVER", True, (0, 0, 0))
		screen.blit(game_over_text, (
			(screen_size[0] - game_over_text.get_rect().width)/ 2, 
			(screen_size[1] - game_over_text.get_rect().height) /2)
		)
	else:

		# Draw the maze
		maze = game.maze
		for x in range(maze.maze_width):
			for y in range(maze.maze_height):
				block = maze.get_block(x, y)
				if block == maze.BLOCKTYPE_WALL:
					# Draw a wall.  We draw a block every 8 pixels (because blocks 
					# are 8x8), but then we double it because scale is 2x.
					draw_block(x, y, graphics.wall)

		# Draw the steak
		(x, y) = game.steak_pos
		draw_block(x, y, graphics.steak)

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