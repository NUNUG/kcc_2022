class Cooldown:
	"""This is a time keeper.  It has a start time and a time to elapse.  
	It doesn't take action when time is up like a timer would.  It only keeps track of the time."""
	def __init__(self, time_function, cooldown_ticks: int):
		"""Set up the cooldown and set the timer."""
		self.get_current_time = time_function
		self.cooldown_ticks : int = cooldown_ticks
		self.reset()
	def reset(self):
		"""Restart the timer, starting... NOW!"""
		self.start_ticks = self.get_current_time()
	def elapsed(self):
		"""How much time has elapsed since we started the timer?"""
		now_ticks = self.get_current_time()
		elapsed_this_time = now_ticks - self.start_ticks
		return elapsed_this_time
	def expired(self):
		"""If enough time has elapsed, the timer has expired and some action can be taken."""
		result : bool = self.elapsed() >= self.cooldown_ticks
		return result
	