var fs = require('fs');

const points = { X: { A: 3, B: 1, C: 2 }, Y: { A: 4, B: 5, C: 6 }, Z: { A: 8, B: 9, C: 7 } };

const score = fs.readFileSync('input.txt').toString().split('\n')
                .reduce((acc, line) => acc + points[line.split(' ')[1]][line.split(' ')[0]], 0);

console.log(score);