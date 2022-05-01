from random import Random
from cooldown import Cooldown
from direction import *
from maze import Maze
from mazemaker import MazeMaker
from pygametimefunction import pygame_time_function
from settings import Settings
from snake import Snake
from sounds import Sounds

class SlitherGame:
	def __init__(self, maze_maker : MazeMaker, snake: Snake, sounds: Sounds):
		"""Set up the Slither game."""
		self.rnd = Random()
		self.maze_maker : MazeMaker = maze_maker
		self.snake : Snake = snake
		self.sounds : Sounds = sounds
		self.game_speed : int = 0
		self.tongue_cooldown = Cooldown(pygame_time_function, self.rnd.randint(500, 1200))
		self.tongue_visible = False
		self.new_game()
	def new_game(self):
		"""This sets our state to the beginning of a new game."""
		self.game_over = False
		self.steak_count = 0
		self.maze : Maze = self.maze_maker.next_maze()
		self.snake.reset(1, Settings.INITIAL_SIZE, self.maze.initial_position, DIRECTION_RIGHT)
		self.steak_pos = self.place_a_steak()
		self.cooldown : Cooldown = Cooldown(pygame_time_function, self.slither_delay())
		self.cooldown.reset()
	def slither_delay(self):
		"""This helps control our game speed.  It converts our *speed* to a *delay*,
		which we can actually use to control speed."""
		result = Settings.PRIMARY_DELAY_MS - self.game_speed
		if result < 0:
			result = 0
		return result
	def in_snake(self, xpos, ypos):
		"""Determines if a block is covered by any part of the snake's body, head or tail."""
		result : bool = False
		for block in self.snake.blocks:
			(position, direction) = block
			(x, y) = position
			if (x == xpos) and (y == ypos):
				result = True
				break
		return result
	def in_snake_body(self, xpos, ypos):
		"""Determines if a block is covered by any part of the snake's
		body only.  It ignores the head and tail."""
		result : bool = False
		for block in self.snake.blocks[1:][:-1]:
			(position, direction) = block
			(x, y) = position

			if (x == xpos) and (y == ypos):
				result = True
				break
		return result
	def place_a_steak(self):
		"""Determine a random position which is onthe floor (not under the snake and not in a wall).
		We will loop and try again over and over until we find a suitable place."""
		while (True):
			xrange : int = self.maze.maze_width
			yrange : int = self.maze.maze_height
			xpos : int = self.rnd.randint(0, xrange)
			ypos : int = self.rnd.randint(0, yrange)
			block : int = self.maze.get_block(xpos, ypos)
			if block == Maze.BLOCKTYPE_FLOOR:
				snake_collision : bool = self.in_snake(xpos, ypos)
				if not snake_collision:
					return (xpos, ypos)
	def increment_level(self):
		"""This will move to the next steak or the next level if it's time to do so."""
		if self.steak_count >= Settings.STEAKS_PER_MAZE:
			self.game_speed = self.game_speed + Settings.SPEEDUP_ON_LEVELUP
			self.new_game()
		else:
			self.steak_pos = self.place_a_steak()
			self.snake.max_size = self.snake.max_size + Settings.GROWTH_ON_LEVELUP
	def check_hit_wall(self):
		"""Determines if the snake's head had hit a wall.  If it did, then game over."""
		(headx, heady) = self.snake.head_position
		block = self.maze.get_block(headx, heady)
		if block == Maze.BLOCKTYPE_WALL:
			self.game_over = True
	def check_bit_self(self):
		"""Determines if the snake's head has hit the snake.  If it did, then game over."""
		(headx, heady) = self.snake.head_position
		if self.in_snake_body(headx, heady):
			self.game_over = True
	def check_ate_steak(self):
		"""Determines if the snake's head has hit a steak.  If it did, kudos!  Go to next steak or next level."""
		(headx, heady) = self.snake.head_position
		(steakx, steaky) = self.steak_pos
		if (headx == steakx) and (heady == steaky):
			self.sounds.eat.play()
			self.steak_count += 1
			self.increment_level()
	def check_tongue_visible(self):
		"""This animates the tongue.  It stays in or out of the mouth for random periods
		of time using a cooldown.  Once the cooldown expires, we move it either in or out, 
		the opposite of where it currently is then start a new random cooldown."""
		if self.tongue_cooldown.expired():
			self.tongue_visible = not self.tongue_visible
			self.tongue_cooldown.cooldown_ticks = self.rnd.randint(500, 1200)
			self.tongue_cooldown.reset()
	def tick(self):
		"""If enough time has passed, we will move the snake.
		We then check to see if he hit a wall, ate the steak or bit himself!"""
		if not self.game_over:
			self.check_tongue_visible()
			if self.cooldown.expired():
				self.snake.move(self.snake.direction)
				self.check_hit_wall()
				self.check_bit_self()
				self.check_ate_steak()
				self.cooldown.reset()
