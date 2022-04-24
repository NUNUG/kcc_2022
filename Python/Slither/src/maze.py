class Maze:
	BLOCKTYPE_STARTING_POSITION : int = 2
	BLOCKTYPE_WALL : int = 1
	BLOCKTYPE_FLOOR : int = 0

	def __init__(self, maze_template_data : str):
		"""A maze which has been constructed from a template."""
		self.template : str = maze_template_data
		self.render_maze(maze_template_data)
	def render_maze(self, template : str):
		"""This converts a maze template from the text structure we have defined 
		in this class to a usable 2D array of integer values, each representing 
		a wall (1) or floor (0)."""
		in_list = template
		out_list = []

		for line in in_list:
			chars = list(line)
			out_line = []
			for c in chars:
				if (c == "*"):
					out_line.append(Maze.BLOCKTYPE_WALL)
				elif (c == "x"):
					out_line.append(Maze.BLOCKTYPE_STARTING_POSITION)
				else:
					out_line.append(Maze.BLOCKTYPE_FLOOR)
			out_list.append(out_line)
		self.maze_data : list[list[int]] = out_list
		self.determine_maze_dimensions()
		self.determine_initial_position()
	def determine_maze_dimensions(self):
		"""Determines the width and height of the maze from the template."""
		greatest_width : int = 0
		lowest_width : int = 999999
		height : int = len(self.template)
		
		for row_data in self.template:
			row_width = len(row_data)
			if (row_width > greatest_width):
				greatest_width = row_width
			if (row_width < lowest_width):
				lowest_width = row_width
		if (lowest_width < greatest_width):
			raise "There is an error in the maze template.  All lines must be the same length."
		
		self.maze_height : int = height
		self.maze_width : int = greatest_width

	def determine_initial_position(self):
		"""Determines the position of the snake's head at the beginning of a maze."""
		self.initial_position = (1, 1)
		for row_num in range(self.maze_height):
			for col_num in range(self.maze_width):
				if (self.maze_data[row_num][col_num] == Maze.BLOCKTYPE_STARTING_POSITION):
					self.initial_position = (col_num, row_num)

	def get_block(self, x : int, y : int):
		# Maze data is stored as [row][col].  
		if y <= self.maze_height - 1:
			if x <= self.maze_width - 1:
				return self.maze_data[y][x]
		return ((x, y), Maze.BLOCKTYPE_FLOOR)


