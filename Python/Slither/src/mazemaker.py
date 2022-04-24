from maze import Maze
from mazetemplates import MazeTemplates

class MazeMaker:
	"""This class will provide us a series of Maze objects from our maze templates."""
	MAZE_WIDTH : int = 80
	MAZE_HEIGHT : int = 50
	def __init__(self):
		"""Set up the MazeMaker object."""
		self.maze_templates : MazeTemplates = MazeTemplates()
		self.maze_number : int = self.maze_templates.maze_count() - 1
		self.maze : list[int]  = None
	def next_maze(self):
		"""Move to the next maze until we run out, then start over at 0 again."""
		self.maze_number = (self.maze_number + 1) % self.maze_templates.maze_count()
		template = self.maze_templates.templates[self.maze_number]
		#maze = self.maze_templates.render_maze(template)
		maze = Maze(template)
		return maze
