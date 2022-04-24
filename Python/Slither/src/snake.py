from direction import DIRECTION_VECTORS


class Snake:
	"""This class represents the blocks that make up the snake's body, head and tail."""
	def __init__(self, size, max_size, position, direction):
		"""Set up the snake."""
		self.reset(size, max_size, position, direction)
	def reset(self, size : int, max_size: int, position : tuple[int, int], direction : int):
		"""Sets the snake at its starting position, size and direction."""
		self.size : int = size
		self.max_size : int = max_size
		self.head_position : tuple[int, int] = position
		self.head_direction: int = direction
		self.direction : int = direction
		self.blocks : list[tuple[int, int], int] = [(position, direction)]
	def move(self, direction):
		"""The snake can move one block in the given direction.  
		If it's already full grown, the tail will follow.  
		Otherwise, it will grow one block as well."""
		# Add a new section for the head in the new location.
		#old_head_position = self.head_position
		direction_vector = DIRECTION_VECTORS[direction]
		new_head_position = (self.head_position[0] + direction_vector[0], self.head_position[1] + direction_vector[1])
		self.head_position = new_head_position
		self.head_direction = self.direction
		self.blocks.append((self.head_position, direction))
		# Unless we're still growing, move the tail by removing the first item in the snake.
		if (self.size >= self.max_size):
			#old_tail_position = self.blocks[0]
			# if (len(self.blocks) >= 2):
			# 	new_tail_position = self.blocks[1]
			# else:
			# 	new_tail_position = old_tail_position
			self.blocks = self.blocks[1:]
			

		self.size = len(self.blocks)
		

	def grow(self, grow_by):
		"""Make the snake grow, but not all at once.  This sets its maximum size, 
		but it will still have to grow one block at a time using the .move() 
		method until it reaches this new maximum size."""
		self.max_size = self.max_size + grow_by