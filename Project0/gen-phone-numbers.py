#!/usr/bin/env python3

import random
import sys

if len(sys.argv) != 2:
    print("usage: ./gen-phone-numbers.py COUNT")
    sys.exit(1)

amount_to_print = 0

try:
    amount_to_print = int(sys.argv[1])
except:
    print("Please provide an integer")
    sys.exit(1)

if amount_to_print <= 0:
    print("Please provide a positive integer")
    sys.exit(1)

for i in range(1, amount_to_print + 1):
    part1 = random.randrange(0,999)
    part2 = random.randrange(0,999)
    part3 = random.randrange(0,9999)
    print("{:03d}-{:03d}-{:04d}".format(part1, part2, part3))