f = open('input.txt', 'r')

calories = []
current = 0

for line in f:
    line = line.strip()

    if line == "":
        calories.append(current)
        current = 0

    else:
        current += int(line)

f.close()

calories.sort()

print(calories[-1] + calories[-2] + calories[-3])