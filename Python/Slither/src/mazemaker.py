from maze import Maze
from mazetemplates import MazeTemplates

class MazeMaker:
	MAZE_WIDTH : int = 80
	MAZE_HEIGHT : int = 50
	def __init__(self):
		self.maze_templates : MazeTemplates = MazeTemplates()
		self.maze_number : int = -1
		self.maze : list[int]  = self.first_maze()
	def first_maze(self):
		"""Set the current maze to the first maze."""
		self.maze_number = 0
		template = self.maze_templates.templates[self.maze_number]
		maze = Maze(template)
		#maze = self.maze_templates.render_maze(template)
		return maze
	def next_maze(self):
		"""Move to the next maze until we run out, then start over at 0 again."""
		self.maze_number = (self.maze_number + 1 % self.maze_templates.maze_count())
		template = self.maze_templates.templates[self.maze_number]
		#maze = self.maze_templates.render_maze(template)
		maze = Maze(template)
		return maze
	# def initial_position(self):
	# 	"""Returns the position of the snake's head at the beginning of a maze."""
	# 	for row_num in range(MazeMaker.MAZE_HEIGHT):
	# 		for col_num in range(MazeMaker.MAZE_WIDTH):
	# 			if (self.maze[row_num][col_num] == BLOCKTYPE_STARTING_POSITION):
	# 				return (col_num, row_num)
		raise "There is an error in the maze template.  It has no starting position."
