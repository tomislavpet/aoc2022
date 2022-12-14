points = {
    'X': {
        'A': 3,
        'B': 1,
        'C': 2,
     },
    'Y': {
        'A': 1 + 3,
        'B': 2 + 3,
        'C': 3 + 3,
     },
    'Z': {
        'A': 2 + 6,
        'B': 3 + 6,
        'C': 1 + 6,
     },
}

f = open('input.txt', 'r')

score = 0

for line in f:
    parts = line.strip().split(' ')
    score += points[parts[1]][parts[0]]

f.close()

print(score)