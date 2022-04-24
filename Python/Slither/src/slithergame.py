from random import Random
from cooldown import Cooldown
from maze import Maze
from mazemaker import MazeMaker
from pygametimefunction import pygame_time_function
from settings import Settings
from snake import Snake

class SlitherGame:
	def __init__(self, maze_maker : MazeMaker, snake: Snake):
		self.rnd = Random()
		self.maze_maker : MazeMaker = maze_maker
		self.snake : Snake = snake
		self.game_speed : int = 0
		self.new_game()
	def new_game(self):
		self.game_over = False
		self.steak_count = 0
		self.maze : Maze = self.maze_maker.next_maze()
		self.snake.reset(1, Settings.INITIAL_SIZE, self.maze.initial_position, self.snake.direction)
		self.steak_pos = self.place_a_steak()		
		self.cooldown : Cooldown = Cooldown(pygame_time_function, self.slither_delay())
		self.cooldown.reset()
	def next_game(self):
		self.game_speed = self.game_speed + 10
		self.new_game()
	def slither_delay(self):
		result = Settings.PRIMARY_DELAY_MS - self.game_speed
		if result < 0:
			result = 0
		return result
	def in_snake(self, xpos, ypos):
		result : bool = False
		for block in self.snake.blocks:
			(position, direction) = block
			(x, y) = position
			if (x == xpos) and (y == ypos):
				result = True
				break
		return result
	def place_a_steak(self):
		# Determine a random position which is on the floor (not on the snake and not in a wall).
		# Loop until we find one like this.  It might be nice to make sure it's not in a corner or 
		# anything, but that's more work than we want to do for now.
		while (True):
			xrange : int = self.maze.maze_width
			yrange : int = self.maze.maze_height
			xpos : int = int(self.rnd.random() * xrange)
			ypos : int = int(self.rnd.random() * yrange)
			block: int = self.maze.get_block(xpos, ypos)
			if block == Maze.BLOCKTYPE_FLOOR:
				snake_collision : bool = self.in_snake(xpos, ypos)
				if not snake_collision:
					return (xpos, ypos)
	def hit_wall(self):
		(headx, heady) = self.snake.head_position
		block = self.maze.get_block(headx, heady)
		if block == Maze.BLOCKTYPE_WALL:
			self.game_over = True
			print("You hit a wall!")
	def bit_self(self):
		(headx, heady) = self.snake.head_position
		for block in self.snake.blocks:
			(blockx, blocky) = block
			if (blockx == headx) and (blocky == heady):
				self.game_over = True
				print("You bit yourself!")
	def ate_steak(self):
		(headx, heady) = self.snake.head_position
		(steakx, steaky) = self.steak_pos
		if (headx == steakx) and (heady == steaky):
			self.increment_level()
			self.steak_count += 1
			print("You ate the steak!  Level up!")
	def increment_level(self):
		if self.steak_count >= Settings.STEAKS_PER_MAZE:
			self.game_speed = self.game_speed + 10
			self.new_game()
		else:
			self.steak_pos = self.place_a_steak()
			self.snake.max_size = self.snake.max_size + Settings.GROWTH_ON_LEVELUP
	def tick(self):
		"""If enough time has passed, we will move the snake.
		We then check to see if he hit a wall, ate the steak or bit himself!"""
		if self.cooldown.expired():
			self.snake.move(self.snake.direction)
			if self.hit_wall():
				pass
			if self.bit_self():
				pass
			if self.ate_steak():
				pass
			self.cooldown.reset()
	
