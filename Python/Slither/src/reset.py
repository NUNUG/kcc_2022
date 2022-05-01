from shutil import copyfile

step_count = 14
file_names = [
	"slither.py",
	"slithergame.py",
]

def copy_file(step_number, file_name):
	src = "../steps/step" + str(step_number) + "/" + file_name
	dest = "./" + file_name

	copyfile(src, dest)
	print("Copied ", src, " to ", dest)

print("This will clean up and reset your work to any step throughout the session.")
print("If you want to play the finished game, go to step 0.")
print("Enter the number for the step you want to work on (Example: 1):")
desiredStep = input()

try:
	step_num = int(desiredStep)
except:
	step_num = -1

if (step_num < 0) or (step_num > step_count):
	print(desiredStep + " is not a valid step number.  Valid steps numbers are 1 through "+str(step_count)+".")
else:
	for file_name in file_names:
		copy_file(step_num, file_name)
