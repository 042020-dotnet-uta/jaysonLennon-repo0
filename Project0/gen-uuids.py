#!/usr/bin/env python3

import uuid
import sys

if len(sys.argv) != 2:
    print("usage: ./gen-uuids.py COUNT")
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
    print(uuid.uuid4())