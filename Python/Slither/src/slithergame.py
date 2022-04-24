from cooldown import Cooldown
from mazemaker import MazeMaker
from pygametimefunction import pygame_time_function
from settings import Settings
from snake import Snake

class SlitherGame:
	def __init__(self, maze_maker : MazeMaker, snake: Snake):
		self.maze_maker : MazeMaker = maze_maker
		self.snake : Snake = snake
		self.new_game()
	def new_game(self):
		self.maze : list[list[int]] = self.maze_maker.first_maze()
		self.snake.reset(1, Settings.INITIAL_SIZE, self.maze.initial_position, self.snake.direction)
		self.game_speed = 0
		self.cooldown : Cooldown = Cooldown(pygame_time_function, self.slither_delay())
		self.cooldown.start()
	def next_game(self):
		self.game_speed = self.game_speed + 10
		self.maze = self.maze_maker.next_maze()
	def slither_delay(self):
		return 255 - self.game_speed
	def hit_wall(self):
		(headx, heady) = self.snake.head_position
		if self.maze.get_block(headx, heady):
			raise "You hit a wall!"
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
			self.cooldown.start()
	
